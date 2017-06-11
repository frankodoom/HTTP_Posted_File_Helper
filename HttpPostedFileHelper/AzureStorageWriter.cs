using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpPostedFileHelper
{

   public  class AzureStorageWriter: IDisposable
    {
        //FileHelper Properties
        /// <summary>
        /// Set Azure Storage Connection Key
        /// </summary>
        public string ConnectionKey { get; set; }

        /// <summary>
        /// Set Azure Blob Container Name
        /// </summary>
        public string Container { get; set; }

       


        /// <summary>
        /// Process Multiple Posted Files Asynchronously
        /// </summary>
        /// <param name = "file" > IEnumerable < HttpPostedFileBase > Collection of files to be posted</param>
        ///  <returns> returns integer count of files written to server</returns>
        public void WriteToAzure(IEnumerable<HttpPostedFileBase> files)
        {

            foreach (var file in files)
            {
                try
                {
                    var clientPath = System.IO.Path.GetDirectoryName(file.FileName);
                    var contentType = file.ContentType;
                    var streamContents = file.InputStream;
                    var blobName = file.FileName;
                    var connectionString = CloudConfigurationManager.GetSetting(ConnectionKey);
                    var storageAccount = CloudStorageAccount.Parse(connectionString);
                    var blobClient = storageAccount.CreateCloudBlobClient();
                    var container = blobClient.GetContainerReference(Container);
                    container.CreateIfNotExists();
                    container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                    // write blob with file name  
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);
                    //var blob = container.GetBlobReference(blobName);
                    blockBlob.Properties.ContentType = contentType;
                    blockBlob.UploadFromStream(streamContents);
                }
                catch
                {
                    // ViewBag.Info = "Error in writing image to storage";
                }
            }
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }


}
