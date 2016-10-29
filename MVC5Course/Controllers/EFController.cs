using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            //一個SaveChanges，含一個交易(trans)
            //在SaveChanges前的所有"變更追蹤"，會一次產生出所有要異動的T-SQL
          
            var delData =  db.Product.Find(id);

            //批次刪除:由關聯table datas(導覽屬性)先刪除,
            db.OrderLine.RemoveRange(delData.OrderLine);
            db.Product.Remove(delData);
            
            db.SaveChanges();   //
            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult Details(int id)
        {
           var data= db.Product.Find(id);

            return View(data);
        }

        public ActionResult Update(int id)
        {
            var UpdData = db.Product.Find(id);
            //變更追蹤
            UpdData.ProductName += "!";

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex) //Entity Framework 發生驗證例外時的處裡方法
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var item in entityErrors.ValidationErrors)
                    {
                        //
                        throw new DbEntityValidationException(item.PropertyName + "發生錯誤" + item.PropertyName);
                    }
                    
                }
              
            }

            return RedirectToAction("Index");
            //return View();
        }



        public ActionResult Edit(int id)
        {
            var EditData = db.Product.Find(id);
            return View(EditData);
        }

        //傳入參數對應submit edit.cshtml的model
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var updData = db.Product.Find(product);
            updData.ProductName = product.ProductName;
            updData.Price = product.Price;
            updData.Stock = product.Stock;
            updData.IsDeleted = product.IsDeleted;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex) //Entity Framework 發生驗證例外時的處裡方法
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var item in entityErrors.ValidationErrors)
                    {
                        //
                        throw new DbEntityValidationException(item.PropertyName + "發生錯誤" + item.PropertyName);
                    }

                }

            }

            return View();
        }

        //批次更新
        //public ActionResult Add20Percent()
        //{
        //    var data = db.Product.Where(p => p.ProductName.Contains("White"));


        //    foreach (var item in data)
        //    {
        //        if (item.Price.HasValue) {
        //            item.Price = item.Price.Value * 1.2m;
        //        }

        //    }


        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //    //return View();
        //}

        //效能調整:利用db.Database.ExecuteSqlCommand()
        public ActionResult Add20Percent()
        {
            string str = "%White%";
            db.Database.ExecuteSqlCommand("update dbo.Product SET Price =Price * 1.2 WHERE ProductName like @p0 ", str);

            db.SaveChanges();

            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult ClientContribution()
        {
            var data = db.vw_ClientContribution.Take(10);
            return View(data);
        }

        public ActionResult ClientContribution2(string FirstName="Mary")
        {
            var data = db.Database.SqlQuery<ClientContributionViewModel>(@"
                        SELECT

                             c.ClientId,
                             c.FirstName,
                             c.LastName,
                             (SELECT SUM(o.OrderTotal)

                              FROM[dbo].[Order] o

                              WHERE o.ClientId = c.ClientId) as OrderTotal

                        FROM  [dbo].[Client] as c 
                        where FirstName like @p0", "%" + FirstName + "%");


            return View(data);
        }

        public ActionResult ClientContribution3(string FirstName)
        {

            return View(db.usp_GetClientContribution(FirstName));
        }
    }
}