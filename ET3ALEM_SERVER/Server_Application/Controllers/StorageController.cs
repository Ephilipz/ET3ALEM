using BusinessEntities.Models;
using DataServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Server_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly StorageFactory _StorageFactory;
        public StorageController(StorageFactory StorageFactory)
        {
            _StorageFactory = StorageFactory;
        }
        [HttpPost("UploadPdf")]
        public async Task<IActionResult> UploadPdf(FileUpload fileUpload)
        {
            return Ok(await UploadFile(fileUpload));
        }
        
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] FileUpload fileUpload)
        {
            return Ok(await UploadFile(fileUpload));
        }

        private async Task<string> UploadFile(FileUpload fileUpload)
        {
            var storageDsl = _StorageFactory.GetStorageDsl(fileUpload.StorageType);

            return await storageDsl.UploadFile(fileUpload);

        }
    }
}
