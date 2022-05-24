using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BusinessEntities.Models;
using ExceptionHandling.CustomExceptions;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.AspNetCore.Http;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
namespace DataServiceLayer
{
    public class GoogleDriveDsl : IStorageDsl
    {
        private readonly IConfiguration _IConfiguration;

        public GoogleDriveDsl(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
        }

        public async Task<bool> DeleteFile(FileDelete fileDelete, string userId)
        {
            var googleDriveSettings = GetGoogleDriveSettings(_IConfiguration);
            var service = GetDriveService(googleDriveSettings);
            var deleteRequest = service.Files.Delete(fileDelete.Id);
            await deleteRequest.ExecuteAsync();
            return true;
        }

        public async Task<FileUploadResult> UploadFile(FileUpload fileUpload, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new CustomExceptionBase("userId can't be null or empty string");
            }
            if (fileUpload.File == null || fileUpload.File.Length == 0)
            {
                throw new CustomExceptionBase("Corrupt file");
            }
            var googleDriveSettings = GetGoogleDriveSettings(_IConfiguration);
            var service = GetDriveService(googleDriveSettings);
            string parentFolderId = await GetUserFolderIfExistsOrCreateNew(service, userId, googleDriveSettings.Upload_folder_id);
            var file = await UploadFile(service, fileUpload.File, fileUpload.File.FileName, new List<string>() { parentFolderId });
            return new FileUploadResult { StorageLink = $"https://drive.google.com/uc?id={file?.Id}", StorageId = file?.Id };
        }

        private async Task<string> GetUserFolderIfExistsOrCreateNew(DriveService service, string userId, string storageParentFolderId)
        {
            string folderId = string.Empty;
            if (!await CheckFolderExists(service, userId))
            {
                var file = await CreateGoogleDriveFile(service, userId, storageParentFolderId);
                folderId = file.Id;
            }
            return folderId;
        }

        private GoogleDriveSettings GetGoogleDriveSettings(IConfiguration _IConfiguration)
        {
            return _IConfiguration.GetSection("Google").Get<GoogleDriveSettings>();
        }

        private DriveService GetDriveService(GoogleDriveSettings googleDriveSettings)
        {
            // Load the Service account credentials and define the scope of its access.
            var credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(googleDriveSettings))
                .CreateScoped(DriveService.ScopeConstants.Drive);
            // Create the  Drive service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }

        private async Task<GoogleFile> CreateGoogleDriveFile(DriveService service, string folderName, string parentFolderId)
        {
            var fileMetadata = new GoogleFile()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentFolderId }
            };
            // Create a new folder on drive.
            var createFolderRequest = service.Files.Create(fileMetadata);
            createFolderRequest.Fields = "id";
            var file = await createFolderRequest.ExecuteAsync();

            var permissionRequest = service.Permissions.Create(new Permission { Type = "anyone", Role = "reader" }, file?.Id);
            await permissionRequest.ExecuteAsync();
            return file;
        }

        private async Task<bool> CheckFolderExists(DriveService service, string folderName)
        {
            string folderId = string.Empty;
            string pageToken = null;
            FilesResource.ListRequest request = service.Files.List();
            request.Q = $"mimeType = 'application/vnd.google-apps.folder' and  name = '{folderName}'";
            request.Fields = "nextPageToken, files(id, name)";
            request.Spaces = "drive";
            do
            {
                request.PageToken = pageToken;
                FileList result = await request.ExecuteAsync();
                var folder = result.Files.FirstOrDefault(file => string.Equals(file.Name, folderName, StringComparison.InvariantCultureIgnoreCase));
                if (folder != null)
                {
                    folderId = folder.Id;
                    break;
                }
                pageToken = result.NextPageToken;
            } while (pageToken != null);
            return !string.IsNullOrEmpty(folderId);
        }

        private async Task<GoogleFile> UploadFile(DriveService service, IFormFile file, string fileName, List<string> parentFolders)
        {
            var fileMetadata = new GoogleFile()
            {
                Name = fileName,
                Parents = parentFolders,
            };
            await using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            // Create a new file, with metadata and stream.
            var request = service.Files.Create(fileMetadata, ms, string.Empty);
            request.Fields = "*";
            var results = await request.UploadAsync(CancellationToken.None);
            if (results.Status == UploadStatus.Failed)
            {
                throw new CustomExceptionBase($"Error uploading file: {results.Exception.Message}");
            }
            var permissionRequest = service.Permissions.Create(new Permission { Type = "anyone", Role = "reader" }, request.ResponseBody?.Id);
            await permissionRequest.ExecuteAsync();
            return request.ResponseBody;
        }
    }
}