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

namespace DataServiceLayer
{
    public class GoogleDriveDsl : IStorageDsl
    {
        private readonly IConfiguration _IConfiguration;

        public GoogleDriveDsl(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
        }

        public async Task<string> UploadFile(FileUpload fileUpload)
        {
            var googleDriveSettings = _IConfiguration.GetSection("Google").Get<GoogleDriveSettings>();

            // Load the Service account credentials and define the scope of its access.
            var credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(googleDriveSettings))
                .CreateScoped(DriveService.ScopeConstants.Drive);
            // Create the  Drive service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            // Upload file Metadata
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileUpload.FileName,
                Parents = new List<string>() {googleDriveSettings.Upload_folder_id},
            };

            // Create a new file on Google Drive
            if (fileUpload.File == null || fileUpload.File.Length == 0)
            {
                throw new CustomExceptionBase("Corrupt file");
            }

            await using var ms = new MemoryStream();
            await fileUpload.File.CopyToAsync(ms);
            
            // Create a new file, with metadata and stream.                                                                                                                                
            var request = service.Files.Create(fileMetadata, ms, string.Empty);
            request.Fields = "*";
            var results = await request.UploadAsync(CancellationToken.None);

            if (results.Status == UploadStatus.Failed)
            {
                Console.WriteLine($"Error uploading file: {results.Exception.Message}");
            }

            var permissionRequest = service.Permissions.Create(new Permission {Type = "anyone", Role = "reader"}, request.ResponseBody?.Id);
            await permissionRequest.ExecuteAsync();
            
            return $"https://drive.google.com/uc?id={request.ResponseBody?.Id}";
        }
    }
}