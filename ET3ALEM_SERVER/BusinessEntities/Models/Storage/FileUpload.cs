using BusinessEntities.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public class FileUpload
    {
        public StorageType StorageType { get; set; }
        public string File { get; set; }
        public string FileName { get; set; }
     }
}
