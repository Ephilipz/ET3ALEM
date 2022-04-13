using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using BusinessEntities.Models;
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
                Parents = new List<string>() { googleDriveSettings.Upload_folder_id },

            };
            string uploadedFileId;
            // Create a new file on Google Drive
            var bytes = Convert.FromBase64String(fileUpload.File);
            var contents = new MemoryStream(bytes);

            // Create a new file, with metadata and stream.
            var request = service.Files.Create(fileMetadata, contents, String.Empty);
            request.Fields = "*";
            var results = await request.UploadAsync(CancellationToken.None);

            if (results.Status == UploadStatus.Failed)
            {
                Console.WriteLine($"Error uploading file: {results.Exception.Message}");
            }
            var _perimissionRequest = service.Permissions.Create(new Permission { Type = "anyone", Role = "reader" }, request.ResponseBody?.Id);
            await _perimissionRequest.ExecuteAsync();
            // the file id of the new file we created
            uploadedFileId = request.ResponseBody?.Id;
            return request.ResponseBody.WebContentLink;
        }
    }
}
