using System;
using System.Linq;
using System.Threading.Tasks;
using Technovert.Azure.Storage.Blobs.BackupBulkBlobs.Extensions;

namespace Technovert.Azure.Storage.Blobs.BackupBulkBlobs
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // argument validation can be improved

            if (args.Length != 3)
            {
                throw new ArgumentException("Please pass all required arguments..!!!");
            }

            string storageAccountConnectionString = args[0];
            string targetCopyLocation = args[1];
            string azCopyToolExePath = args[2];

            if (storageAccountConnectionString.IsNullOrEmpty() || targetCopyLocation.IsNullOrEmpty() || azCopyToolExePath.IsNullOrEmpty())
            {
                throw new ArgumentException("Please pass all required arguments..!!!");
            }

            await new BackupBlob().BackupAllContainers(storageAccountConnectionString, targetCopyLocation, azCopyToolExePath);
        }
    }
}
