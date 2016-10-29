using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVC5Course.Controllers {
    public class Share頁面上常用的ViewBag變數資料 : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewData["Temp1"] = "暫存資料 Temp1";

            var b = new ClientLoginViewModel() {
                FirstName = "Vicky",
                LastName = "Kao"
            };

            filterContext.Controller.ViewData["Temp2"] = b;
            filterContext.Controller.ViewBag.Temp3 = b;

            //base.OnActionExecuting(filterContext);
        }


    }
}
