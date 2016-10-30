using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVC5Course.Controllers {
    public class Share頁面上常用的ViewBag變數資料 : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            //開始時間
            filterContext.Controller.ViewData["StrTime"] = DateTime.Now;
            filterContext.Controller.ViewData["Temp1"] = "暫存資料 Temp1";

            var b = new ClientLoginViewModel() {
                FirstName = "Vicky",
                LastName = "Kao"
            };

            filterContext.Controller.ViewData["Temp2"] = b;
            filterContext.Controller.ViewBag.Temp3 = b;

            //base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            //結束時間
            filterContext.Controller.ViewData["EndTime"] = DateTime.Now;

            //base.OnActionExecuted(filterContext);
        }

    }
}
