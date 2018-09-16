using gov_moderator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Services
{
    public class StorageClient : IStorageClient
    {
        private CloudBlobClient blobClient;
        private GMConfig config;

        public StorageClient(CloudBlobClient blobClient, GMConfig config)
        {
            this.blobClient = blobClient;
            this.config = config;
        }

        public async Task<string> AddNewBlob(string blobName, IFormFile blob)
        {
            var container = blobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.Properties.ContentType = blob.ContentType;

            using (var stream = blob.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(stream);
            }
            return blockBlob.Uri.ToString();
        }

        public async Task DeleteBlob(string blobName)
        {
            var container = this.blobClient.GetContainerReference("images");
            var blockBlob = container.GetBlockBlobReference(blobName);
            await blockBlob.DeleteAsync();
        }
    }

    public interface IStorageClient
    {
        Task<string> AddNewBlob(string blobName, IFormFile blob);
        Task DeleteBlob(string blobName);
    }

    public class BlobInfo
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}
