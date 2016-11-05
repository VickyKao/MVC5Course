using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    [Authorize]
    public class ClientsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        // GET: Clients
        public ActionResult Index(string search, int? CreditRating, string Gender)
        {
            var client = db.Client.Include(c => c.Occupation);
            //搜尋
            if (!string.IsNullOrEmpty(search)) {
                client = client.Where(p => p.FirstName.Contains(search));
            }
            if (CreditRating != null) {
                client = client.Where(p => p.CreditRating == CreditRating);
            }
            if (!string.IsNullOrEmpty(Gender)) {
                client = client.Where(p => p.Gender.Contains(Gender));
            }
            client =  client.OrderByDescending(p => p.ClientId).Take(10);

            //CreditRating
            var options = (from p in db.Client
                           select p.CreditRating).Distinct().OrderBy(p=>p).ToList();
            ViewBag.CreditRating = new SelectList(options);

            //Gender
            ViewBag.Gender = new SelectList(new string[] { "M", "F"});

            return View(client);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        [ChildActionOnly]
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName");

            //給預設值
            var client = new Client() {
                Gender = "M"
            };
            return View(client);
        }

        // POST: Clients/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form) {
            //form傳入參數沒有使用，只是為了與同名方法做區隔

            var client = db.Client.Find(id);  //找到要更新的資料
            //TryUpdateModel取代ModelState.IsValid，同時binding及驗證
            if (TryUpdateModel(client, null, null, new string[] { "IsAdmin" })) {
                //排除掉IsAdmin
                //db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            db.Client.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login() {

            return View();
        }

        //mvcpostaction4 (HttpPost mvc)
        [HttpPost]
        public ActionResult Login(ClientLoginViewModel client) {

            return View("LoginResult", client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
