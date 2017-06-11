
HttpPosted File Helper V 1.2.0
 __        __   _                          _   _____      __     __  _   ____    ___  
 \ \      / /__| | ___ ___  _ __ ___   ___| | |_   _|__   \ \   / / / | |___ \  / _ \ 
  \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \ / _ \ |   | |/ _ \   \ \ / /  | |   __) || | | |
   \ V  V /  __/ | (_| (_) | | | | | |  __/_|   | | (_) |   \ V /   | |_ / __/ | |_| |
    \_/\_/ \___|_|\___\___/|_| |_| |_|\___(_)   |_|\___/     \_/    |_(_)_____(_)___/
                                                           

HttpPosted File Helper is a light-weight library for Posting Files,
currently supports Asp.Net MVC (Asp.Net Core Coming Soon).


This Revision (V.1.2.0)
    *Rewritten FileHelper
    *AzureStorageWriter
	*Implemented Garbage Collection for good memory management


####### THE NEW FileHelper Class  #########################################

   The new FileHelper Class implements IDisposable for good memory management, also 
   changes from passing property values as arguments to setitng values through accessors.

   * The Old Implementation (Basic Usage)

        '[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFiles(Enumerable<HttpPostedFileBase> files)
	{
	Filehelper filehelper = new Filehelper();
        filehelper.ProcessFile(files,"MyPath");
	}'


  * The New Implementation (Wraps in a using statement)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
		{

            using(var filehelper = new FileHelper())
			}
			   filehelper.FilePath = "/PostedFiles";
               filehelper.ProcessFile(files);
			{
		}
			

############ AZURE STORAGE WRITER FEATURE###########################################.

1.......Setting Up The Connection

   a)For testing with a storage emulator in Development, add this to your Web.config

		    <appSettings>
             <add key="[MyConnectionName]" value="UseDevelopmentStorage=true" />
               </appSettings>


   b)In Production add your Azure Storage Key to your appSettings in Web.config
		  
		  <appSettings>
		     <add key = "[MyConnectionName]" value = "{Your Azure Storage Connection Key Value}" />			                 
			 </appSettings>
       
2........Sending the files
             
	    [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
		{

           using (var storagewriter = new AzureStorageWriter())
           {
              storagewriter.ConnectionKey = "StorageConnectionString";
              storagewriter.Container = "cloud991";
              storagewriter.WriteToAzure(files);
           } 
		   
		   return View("Index);  
       }


3........Sending the files Asyncronuously

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<int> ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
		{

           using (var storagewriter = new AzureStorageWriter())
           {
              storagewriter.ConnectionKey = "StorageConnectionString";
              storagewriter.Container = "cloud991";
              int num = await  storagewriter.WriteToAzure(files);
			  //Do Somethingelse While Awaiting
			  if(num>0)
			  }
			     //Files Written Successfully.
			  {
			  
			  }
           } 		   
		   return View("Index);  
     
	 
	 !!!!!!!!!!!!!!!!!!!!!! THANK YOU FOR DOWNLOADING HOPE THIS HELPS YOU DEVELOP RAPIDLY :-) !!!!!!!!!!!!!!!!!
	   Follow me @mr_odoom
	   Contributions are Welcome
	   www.github.com/frankodoom/Http_Posted_File_Helper
