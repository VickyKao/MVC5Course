using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVC5Course.Controllers {
    class LocalDebugOnlyAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            //如果是從遠端登入就轉址
            if (!filterContext.HttpContext.Request.IsLocal) {
                filterContext.Result = new RedirectResult("/");
            }


            //base.OnActionExecuting(filterContext);
        }

    }
}
