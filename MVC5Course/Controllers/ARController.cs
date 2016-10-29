﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ActionResult NotFound() {
            return View();
        }

        public ActionResult PartialViewTest() {
            return PartialView();
        }

        public ActionResult ContentTest() {

            return Content("開發實戰", "text/plain", Encoding.GetEncoding("utf-8"));
        }

        public ActionResult FileTest() {
            var filePath = Server.MapPath("~/Content/PPAP.jpg");
            return File(filePath, "image/jpeg");
        }

        public ActionResult FileTest2() {
            var filePath = Server.MapPath("~/Content/PPAP.jpg");
            return File(filePath, "image/jpeg", "NewPPAP.jpg");  //下載檔案
        }

        public ActionResult JsonTest() {
            db.Configuration.LazyLoadingEnabled = false;  //停用延遲載入
            var data = db.Product.OrderByDescending(p => p.ProductId).Take(10);

            return Json(data, JsonRequestBehavior.AllowGet);
        }




    }
}