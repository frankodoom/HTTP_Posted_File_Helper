HTTP Posted File Helper V.1.0.2

This is a light weight library that helps in the posting of files to IIS Webserver by providing a helper class FileHElper.cs which contains overloaded methods ProcessFile() which reduces the boilerplate in posting files.

Installing..

    PM> Install-Package HttpPostedFileHelper

Usage

 //Reference the Library
  using HttpPostedFileHelper;
Processing Single Files (Basic Usage)

     [HttpPost]  
     [ValidateAntiForgeryToken] 
      public ActionResult UploadFile(HttpPostedFileBase file)
      {
       //Instanciate the Filehelper class to create a Filehelper Object
       FileHelper filehelper = new FileHelper();
       filehelper.ProcessFile(file, "{path to Existing or New Directory}");
       return view();
      }
Processing Multiple Files
This makes provision for multiple files being uploaded to the server with an overridden method for Processing an IEnumerable of files (HttpPostedFileBase Collection)

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UploadFile(Model model, IEnumerable<HttpPostedFileBase> file)
    {
        FileHelper filehelper = new FileHelper();
        // ProcessFileAsync returns count of files processed           
        int postedfiles = filehelper.ProcessFile(file, "~/MyTargetLocation");
        if (postedfiles > 0)
        {
            //files were written successfully
        }           
        return View("Home");
    }
Asynchronous File Processing
Processing files can be done Asynchronously, this allows large files to be processed in the background thread freeing the main thread

       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<ActionResult> UploadFile(Model model, IEnumerable<HttpPostedFileBase> file)
       {
          FileHelper filehelper = new FileHelper();          
          await filehelper.ProcessFileAsync(file, "~/MyTargetLocation");
          //you can do some other work while awaiting          
           return View("Home");
        }
Reject File Extensions During Upload
You can specify the file types to be rejected during an upload by supplying a string of the file extensions

         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<ActionResult> UploadFile(Model model, IEnumerable<HttpPostedFileBase> file)
          {
            FileHelper filehelper = new FileHelper();
            string reject = ".jpeg,.png,.svg";
            int postedfiles = await filehelper.ProcessFileAsync(file, "~/MyTargetLocation",reject);
            //you can do some other work while awaiting   
            if (postedfiles > 0)
             {
                //files were written successfully
              }   
             return View("Home");
          }

 
