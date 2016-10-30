using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVC5Course.ActionFilters.ShareDataAttribute;

namespace MVC5Course.Controllers
{
    public class MVController : BaseController
    {
   
        [Share頁面上常用的ViewBag變數資料] //AOP
        // GET: MV
        public ActionResult Index()
        {
            //弱型別傳值，使用 Dictionary
           // ViewData["Temp1"] = "暫存資料1";

            var b = new ClientLoginViewModel();
            {
                 b.FirstName = "Will";
                 b.LastName = "Huang";
            };
            ViewData["Temp2"] = b;

            ViewBag.Temp3 = b;

            return View();
        }

        public ActionResult MyForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyForm(ClientLoginViewModel c)
        {
            //必需要進行ModelState.IsValid 檢核
            if (ModelState.IsValid)
            {
                TempData["MyFormData"] = c;
                return RedirectToAction("MyFormResult");
            }
            return View();
        }

        public ActionResult MyFormResult()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var data = db.Product.OrderByDescending(p => p.ProductId).Take(10);

            return View(data);
        }

        public ActionResult BatchUpdate(ProductBatchUpdateViewModel[] items)
        {

            /*
           * VIEW:
           * 預設輸出的欄位名稱格式：item.Productid
           * 要改成以下欄位格式：
           * item[0].Productid
           * item[1].Productid
           * 
           * */

            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var product = db.Product.Find(item.ProductId);
                    product.ProductName = item.ProductName;
                    product.Price = item.Price;
                    product.Active = item.Active;
                    product.Stock = item.Stock;

                }
                db.SaveChanges();
                return RedirectToAction("ProductList");
            }

           
            return RedirectToAction("ProductList");

            return View();
        }

       
    }
}