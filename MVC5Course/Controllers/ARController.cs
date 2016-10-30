using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();  
        }

        public ActionResult FileTest()
        {
            var filePath = Server.MapPath("~/Content/A.jpg");
            return File(filePath, "image/jpeg");
        }


        public ActionResult Action()
        {
            return View();
        }

        public ActionResult JsonTest()
        {
            db.Configuration.LazyLoadingEnabled = false;
            //var data = db.Product.OrderBy(p => p.ProductId).Take(10);
            var data = db.Client.OrderBy(p => p.FirstName).Take(10);

            return Json(data, JsonRequestBehavior.AllowGet);


        }
    }
}