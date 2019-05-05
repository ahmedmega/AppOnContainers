using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreMongo
{
    public class AppSettings
    {
        public string StorageConnectionString { get; set; }
        public string AzureStorageAccountContainer { get; set; }

        public string MongoConnectionString { get; set; }

        public string WebAppUrl { get; set; }
    }
}
