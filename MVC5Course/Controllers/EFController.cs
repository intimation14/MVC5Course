using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            //var db = new FabricsEntities();
            //where :
            var data = db.Product.Where(p=> p.ProductName.Contains("White"));


            return View(data);
        }

        public ActionResult Create()
        {
            //var db = new FabricsEntities();
            var client = new Product()
            {
                ProductName = "White Cat",
                Active = true,
                Price = 199,
                Stock = 5
            };

            //集合物件
            db.Product.Add(client);

            db.SaveChanges();

            return  RedirectToAction("Index");
            //return View();
        }

        //
        public ActionResult Delete(int id)
        {
           var delData =  db.Product.Find(id);

            db.Product.Remove(delData);

            db.SaveChanges();
            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult Details(int id)
        {
           var data= db.Product.Find(id);

            return View(data);
        }

       
    }
}