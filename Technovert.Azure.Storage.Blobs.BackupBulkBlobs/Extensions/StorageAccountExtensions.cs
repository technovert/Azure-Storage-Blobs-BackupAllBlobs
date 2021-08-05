using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.Azure.Storage.Blobs.BackupBulkBlobs.Extensions
{
    internal static class StorageAccountExtensions
    {
        internal static string GetStorageAccountName(this string storageAccountConnectionString)
        {
            string accountNameKey = "AccountName=";
            return storageAccountConnectionString.Split(';').FirstOrDefault(m => m.StartsWith(accountNameKey)).Split(accountNameKey)[1];
        }
    }
}
