using BusinessEntities.Enumerators;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class StorageFactory

    {
        private readonly IConfiguration _IConfiguration;
        public StorageFactory(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
        }
        public IStorageDsl GetStorageDsl(StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.GoogleDrive:
                    return new GoogleDriveDsl(_IConfiguration);
                default:
                    return new GoogleDriveDsl(_IConfiguration);
            }
        }
    }
}
