using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;
using MVC5Course.Models.ViewModels;

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

            db.OrderLine.RemoveRange(product.OrderLine);  //先刪除關連資料
            db.Product.Remove(product);  //再刪除主要資料

            #region 錯誤示範, 不可以使用
            //foreach (var item in product.OrderLine.ToList()) {
            //    db.OrderLine.Remove(item);
            //    db.SaveChanges();
            //}
            #endregion
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
            try {
                db.SaveChanges();  //整個實體更新, 不會只更新某一欄位
            }
            catch (DbEntityValidationException ex) {
                foreach (var entityErrors in ex.EntityValidationErrors) {
                    foreach (var vErrors in entityErrors.ValidationErrors) {
                        throw new DbEntityValidationException(vErrors.PropertyName + " 發生錯誤 : " + vErrors.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        //public ActionResult Add20Percent() {
        //    var data = db.Product.Where(p => p.ProductName.Contains("White"));

        //    foreach (var item in data) {
        //        if (item.Price.HasValue) {
        //            item.Price = item.Price * 1.2m;
        //        }
        //    }

        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Add20Percent() {
            string str = "%White%";
            db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price = Price * 1.2 WHERE ProductName LIKE @p0", str);

            return RedirectToAction("Index");
        }

        //顯示檢視表內容
        public ActionResult ClientContribution() {
            var data = db.vw_ClientOrderTotal.Take(10);

            return View(data);
        }

        public ActionResult ClientContribution2(string keyword = "Mary") {
            var data = db.Database.SqlQuery<ClientContributionViewModel>(@"
              SELECT
		                c.FirstName,
		                c.LastName,
		                (SELECT SUM(o.OrderTotal) 
		                FROM [dbo].[Order] o 
		                WHERE o.ClientId = c.ClientId) as OrderTotal
	            FROM [dbo].[Client] as c
                WHERE
                        c.FirstName LIKE @p0", "%" + keyword + "%");
            return View(data);
        }

        public ActionResult ClientContribution3(string keyword = "Vicky") {
            return View(db.usp_GetClientContribution(keyword));
        }


    }
}