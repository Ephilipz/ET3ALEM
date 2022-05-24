using BusinessEntities.Models;
using DataServiceLayer;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Server_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StorageController : ControllerBase
    {
        private readonly StorageFactory _StorageFactory;
        private readonly string UserId;
        public StorageController(StorageFactory StorageFactory, IAccountHelper AccountHelper)
        {
            _StorageFactory = StorageFactory;
            UserId = AccountHelper.GetUserId(HttpContext, User);
        }
        [HttpPost("UploadPdf")]
        public async Task<IActionResult> UploadPdf([FromForm] FileUpload fileUpload)
        {
            return Ok(await UploadFile(fileUpload));
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] FileUpload fileUpload)
        {
            return Ok(await UploadFile(fileUpload));
        }

        private async Task<FileUploadResult> UploadFile(FileUpload fileUpload)
        {
            var storageDsl = _StorageFactory.GetStorageDsl(fileUpload.StorageType);
            return await storageDsl.UploadFile(fileUpload, UserId);
        }
        [HttpPost("DeleteFile")]
        public async Task<IActionResult> DeleteFile(FileDelete fileDelete)
        {
            var storageDsl = _StorageFactory.GetStorageDsl(fileDelete.StorageType);
            return Ok(await storageDsl.DeleteFile(fileDelete, UserId));
        }
    }
}
