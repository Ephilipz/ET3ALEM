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
        /// <summary>
        /// upload file and returns download link
        /// </summary>
        Task<string> UploadFile(FileUpload fileUpload);
    }
}
