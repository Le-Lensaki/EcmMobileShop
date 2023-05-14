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
    public class ProductsAdminController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();

        // GET: OneTechAdmin/ProductsAdmin
        public ActionResult Index()
        {
            var tb_SANPHAM = db.tb_SANPHAM.Include(t => t.tb_HANGSP).Include(t => t.tb_LOAISP);
            return View(tb_SANPHAM.ToList());
        }

        // GET: OneTechAdmin/ProductsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM);
        }

        // GET: OneTechAdmin/ProductsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.IdHangSP = new SelectList(db.tb_HANGSP, "IdHangSP", "TenHangSP");
            ViewBag.IdLoaiSP = new SelectList(db.tb_LOAISP, "IdLoaiSP", "TenLoaiSP");
            return View();
        }

        // POST: OneTechAdmin/ProductsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSP,IdHangSP,IdLoaiSP,TenSP,MoTaSP,Gia,ImagePathMain,NgayNhap,TrangThai")] tb_SANPHAM tb_SANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.tb_SANPHAM.Add(tb_SANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHangSP = new SelectList(db.tb_HANGSP, "IdHangSP", "TenHangSP", tb_SANPHAM.IdHangSP);
            ViewBag.IdLoaiSP = new SelectList(db.tb_LOAISP, "IdLoaiSP", "TenLoaiSP", tb_SANPHAM.IdLoaiSP);
            return View(tb_SANPHAM);
        }

        // GET: OneTechAdmin/ProductsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHangSP = new SelectList(db.tb_HANGSP, "IdHangSP", "TenHangSP", tb_SANPHAM.IdHangSP);
            ViewBag.IdLoaiSP = new SelectList(db.tb_LOAISP, "IdLoaiSP", "TenLoaiSP", tb_SANPHAM.IdLoaiSP);
            return View(tb_SANPHAM);
        }

        // POST: OneTechAdmin/ProductsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSP,IdHangSP,IdLoaiSP,TenSP,MoTaSP,Gia,ImagePathMain,NgayNhap,TrangThai")] tb_SANPHAM tb_SANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_SANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHangSP = new SelectList(db.tb_HANGSP, "IdHangSP", "TenHangSP", tb_SANPHAM.IdHangSP);
            ViewBag.IdLoaiSP = new SelectList(db.tb_LOAISP, "IdLoaiSP", "TenLoaiSP", tb_SANPHAM.IdLoaiSP);
            return View(tb_SANPHAM);
        }

        // GET: OneTechAdmin/ProductsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM);
        }

        // POST: OneTechAdmin/ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            db.tb_SANPHAM.Remove(tb_SANPHAM);
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
