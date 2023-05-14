using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcmMobileShop.Models;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class FeedbackController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();

        // GET: OneTechAdmin/Feedback
        public ActionResult Index()
        {
            var tb_FEEDBACK = db.tb_FEEDBACK.Include(t => t.tb_CHITIETHOADON);
            return View(tb_FEEDBACK.ToList());
        }

        // GET: OneTechAdmin/Feedback/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_FEEDBACK tb_FEEDBACK = db.tb_FEEDBACK.Find(id);
            if (tb_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            return View(tb_FEEDBACK);
        }

        // GET: OneTechAdmin/Feedback/Create
        public ActionResult Create()
        {
            ViewBag.IdctHD = new SelectList(db.tb_CHITIETHOADON, "IdctHD", "TenSP");
            return View();
        }

        // POST: OneTechAdmin/Feedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFB,IdctHD,Noidung,Img,NgayFB")] tb_FEEDBACK tb_FEEDBACK)
        {
            if (ModelState.IsValid)
            {
                db.tb_FEEDBACK.Add(tb_FEEDBACK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdctHD = new SelectList(db.tb_CHITIETHOADON, "IdctHD", "TenSP", tb_FEEDBACK.IdctHD);
            return View(tb_FEEDBACK);
        }

        // GET: OneTechAdmin/Feedback/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_FEEDBACK tb_FEEDBACK = db.tb_FEEDBACK.Find(id);
            if (tb_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdctHD = new SelectList(db.tb_CHITIETHOADON, "IdctHD", "TenSP", tb_FEEDBACK.IdctHD);
            return View(tb_FEEDBACK);
        }

        // POST: OneTechAdmin/Feedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFB,IdctHD,Noidung,Img,NgayFB")] tb_FEEDBACK tb_FEEDBACK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_FEEDBACK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdctHD = new SelectList(db.tb_CHITIETHOADON, "IdctHD", "TenSP", tb_FEEDBACK.IdctHD);
            return View(tb_FEEDBACK);
        }

        // GET: OneTechAdmin/Feedback/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_FEEDBACK tb_FEEDBACK = db.tb_FEEDBACK.Find(id);
            if (tb_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            return View(tb_FEEDBACK);
        }

        // POST: OneTechAdmin/Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_FEEDBACK tb_FEEDBACK = db.tb_FEEDBACK.Find(id);
            db.tb_FEEDBACK.Remove(tb_FEEDBACK);
            db.SaveChanges();
            return RedirectToAction("Index");
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
