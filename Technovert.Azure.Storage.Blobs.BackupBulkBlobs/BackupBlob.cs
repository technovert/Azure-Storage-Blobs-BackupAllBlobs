using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.Azure.Storage.Blobs.BackupBulkBlobs.Extensions;

namespace Technovert.Azure.Storage.Blobs.BackupBulkBlobs
{
    internal class BackupBlob
    {
        private string azCopyToolExePath { get; set; }

        internal async Task BackupAllContainers(string storageAccountConnectionString, string targetCopyLocation, string azCopyToolExePath)
        {
            // Suffix Target Copy location with "<storageAccountName>\Containers"
            targetCopyLocation += $@"\{storageAccountConnectionString.GetStorageAccountName()}\Containers";

            this.azCopyToolExePath = azCopyToolExePath;

            await this.BackupContainers(storageAccountConnectionString, targetCopyLocation);
        }

        private async Task BackupContainers(string storageAccountConnectionString, string targetCopyLocation)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageAccountConnectionString);

            var containers = blobServiceClient.GetBlobContainers();

            foreach (var container in containers)
            {
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container.Name);

                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = containerClient.Name,
                    Resource = "c"
                };

                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(2);

                sasBuilder.SetPermissions(BlobContainerSasPermissions.All);
                Uri sasUri = containerClient.GenerateSasUri(sasBuilder);

                await BackupContainer(sasUri.ToString(), targetCopyLocation);
            }
        }

        private async Task BackupContainer(string sasToken, string targetCopyLocation)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            process.StartInfo.FileName = this.azCopyToolExePath;

            process.StartInfo.Arguments = @$"copy {sasToken} {targetCopyLocation} --recursive=true";

            Console.WriteLine("Copy started!");

            process.Start();

            await process.WaitForExitAsync();

            Console.WriteLine("Copy done!");
        }
    }
}
