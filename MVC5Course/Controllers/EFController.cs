﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White"))
                                                .OrderByDescending(p=>p.ProductId) ;

            return View(data);
        }

        public ActionResult Create() {

            var client = new Product() {
                ProductName = "White cat",
                Active = true,
                Price = 199,
                Stock = 5
            };

            db.Product.Add(client);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {
            var product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id) {
            var product = db.Product.Find(id);

            return View(product);
        }

        public ActionResult Update(int id) {
            var product = db.Product.Find(id);

            product.ProductName += "!";
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Add20Percent() {
            var data = db.Product.Where(p => p.ProductName.Contains("White"));

            foreach (var item in data) {
                if (item.Price.HasValue) {
                    item.Price = item.Price * 1.2m;
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}