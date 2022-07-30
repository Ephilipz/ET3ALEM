using BusinessEntities.Models;
using DataServiceLayer;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Server_Application.Controllers
{
    
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly StorageFactory _StorageFactory;
        private readonly IAccountHelper _accountHelper;
        private string UserId;
        public StorageController(StorageFactory StorageFactory, IAccountHelper AccountHelper)
        {
            _StorageFactory = StorageFactory;
            _accountHelper = AccountHelper;
        }
        [HttpPost("UploadPdf")]
        public async Task<IActionResult> UploadPdf([FromForm] FileUpload fileUpload)
        {
            return Ok(await UploadFile(fileUpload));
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] FileUpload fileUpload)
        {
            UserId = _accountHelper.GetUserId(HttpContext, User);
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
