using HttpPostedFileHelper;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {

            AzureStorageWriter az = new AzureStorageWriter();
            az.ConnectionKey = "Emulator";
            az.Container = "cloud991";
            az.WriteToAzure(files);
            
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}