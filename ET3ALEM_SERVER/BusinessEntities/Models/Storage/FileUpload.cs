using BusinessEntities.Enumerators;
using Microsoft.AspNetCore.Http;

namespace BusinessEntities.Models
{
    public class FileUpload
    {
        public StorageType StorageType { get; set; }
        public IFormFile  File { get; set; }
     }
}
