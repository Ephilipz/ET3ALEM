using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public interface IStorageDsl
    {
        Task<FileUploadResult> UploadFile(FileUpload fileUpload, string userId);
        Task<bool> DeleteFile(FileDelete fileDelete, string userId);
    }
}
