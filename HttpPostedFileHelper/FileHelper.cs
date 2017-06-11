using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HttpPostedFileHelper;
using System.Web.Hosting;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;

namespace HttpPostedFileHelper
{

    //Handles writing posted files to WebServer
    public class FileHelper : IDisposable
    {

        private static string fileName { get; set; }

        /// <summary>
        ///  /// Set Existing File Path  
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///  /// Set File Extensions to Reject During Upload
        /// </summary>
        public string FileExtension { get; set; }
        private int fileCount { get; set; }
        private IEnumerable<HttpPostedFileBase> pfiles { get; set; }

  

        //Quick Save automatically detects multiple or single files
        /// <summary>
        /// Post Single File 
        /// </summary>
        /// /// <param name="file"> The HttpPostedBase file Object</param>
        public int ProcessFile(HttpPostedFileBase file)
        {
            try
            {
                //When Just a Single File is uploaded
                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    string serverPath = Path.Combine(HostingEnvironment.MapPath(FilePath), fileName);
                    file.SaveAs(serverPath);
                    //Increment successful written file
                    fileCount = fileCount + 1;

                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return fileCount;
        }


        //Synchronous writing of Multiple Files
        /// <summary>
        /// Basic Bulk File Processing 
        /// </summary>
        /// /// <param name="file"> The HttpPostedBase file Object</param>
        public int ProcessFile(IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                foreach (var files in file)
                {
                    if (file != null && files.ContentLength > 0)
                    {
                        //get filename
                        fileName = Path.GetFileName(files.FileName);
                        //get file path
                        string serverPath = Path.Combine(HttpContext.Current.Server.MapPath(FilePath), fileName);
                        //get file extensions
                        var FileExtension = Path.GetExtension(FilePath);

                        files.SaveAs(serverPath);
                        //Increment successful written file
                        fileCount = fileCount + 1;
                    }
                }
            }
            catch (Exception PotedFileException)
            {
                PotedFileException.ToString();
            }
            //return number of files written  successfully
            return fileCount;

        }


        //Asynchronuous posting with File Extension Blacklisting
        /// <summary>
        /// Process Multiple Posted Files with Specified File Extensions Asynchronously
        /// </summary>
        /// <param name="file"> Collection of files
        /// </param>
        /// <param name="extension"> 
        /// file extensions to be rejected during upload eg .docx, .jpeg, .xlsx
        /// </param>
        public async Task<int> ProcessFilesAsync(IEnumerable<HttpPostedFileBase> file)
        {

            try
            {
                foreach (var files in file)
                {
                    if (file != null && files.ContentLength > 0)
                    {
                        //get filename
                        fileName = Path.GetFileName(files.FileName);
                        //get file path
                        string serverPath = Path.Combine(HostingEnvironment.MapPath(FilePath), fileName);
                        //get file extensions
                        var FileExt = Path.GetExtension(serverPath);
                        //reject extensions
                        if (FileExtension.Contains(FileExt))
                        {
                            //blacklist.Add(FileName); //Add the blacklisted file

                            continue; //skip the current iteration
                        }
                        files.SaveAs(serverPath);

                        //Increment successful written file
                        fileCount = fileCount + 1;
                    }
                }
                // get number of fles written 
                //filesWritten =  file.Count();         
            }
            catch (Exception e)
            {
                e.ToString();
            }
            //return number of files written  successfully
            int result = await Task.FromResult<int>(fileCount);
            return result;
        }
        public void Dispose()
        {
           
        }

    }
}




