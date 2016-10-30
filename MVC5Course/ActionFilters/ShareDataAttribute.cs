using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
    public class ShareDataAttribute
    {
        public class Share頁面上常用的ViewBag變數資料Attribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                //取得目前filter有多少參數
                //filterContext.ActionParameters
                 filterContext.Controller.ViewData["Temp1"] = "暫存資料1";
            }
        }
    }
}