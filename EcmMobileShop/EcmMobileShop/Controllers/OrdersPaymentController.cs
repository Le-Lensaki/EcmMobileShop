﻿using EcmMobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcmMobileShop.Controllers
{
    public class OrdersPaymentController : Controller
    {
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();
        // GET: OrdersPayment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(tb_KHACHHANG model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Orders(model.TenKH, model.SDT, model.DiaChi);
                    return RedirectToAction("Cart", "Home");
                
                }    
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
        public void Orders(string hoten,string sdt,string diachi)
        {
            List<CartItem> shoppingCart = Session["ShoppingCart"] as List<CartItem>;
            tb_KHACHHANG kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == hoten && k.SDT == sdt);
            if(kh == null)
            {
                kh = new tb_KHACHHANG();
                ecmMobile.tb_KHACHHANG.Add(new tb_KHACHHANG { TenKH = hoten,SDT = sdt, DiaChi = diachi });
                ecmMobile.SaveChanges();
                kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == hoten && k.SDT == sdt);
            }
            tb_HOADON hd = new tb_HOADON();
            hd.tb_KHACHHANG = kh;
            hd.NgayLap = DateTime.Now;
            hd.IdTinhTrangDH = 1;
            hd.DiaChiGiao = diachi;
            ecmMobile.tb_HOADON.Add(hd);
            ecmMobile.SaveChanges();
            



            foreach (CartItem item in shoppingCart)
            {
                tb_CT_SANPHAM ctsp = ecmMobile.tb_CT_SANPHAM.Single(ct => ct.MauSac == item.MauSac && ct.IdctSP == item.IdctSP);
                

                    tb_CHITIETHOADON cthd = new tb_CHITIETHOADON();
                cthd.tb_HOADON = hd;
                cthd.tb_CT_SANPHAM = ctsp;
                cthd.TenSP = item.TenSP;
                cthd.SoLuongBan = item.SoLuong;
                cthd.GiaBan = item.Gia;
                ecmMobile.tb_CHITIETHOADON.Add(cthd);

            }
            ecmMobile.SaveChanges();


            Session["ShoppingCart"] = null;
           
        }

        public ActionResult FormHistory()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> FormHistory(tb_KHACHHANG model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tb_KHACHHANG kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == model.TenKH && k.SDT == model.SDT);
                    return RedirectToAction("HistoryOrders", "OrdersPayment",new { idkh = kh.IdKH});

                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        public ActionResult HistoryOrders(int idkh)
        {
            if(idkh == 0)
            {
                return RedirectToAction("Index", "Home");
            }    
            List<tb_HOADON> hd = ecmMobile.tb_HOADON.Where(p => p.IdKH == idkh).ToList();

            return View(hd);
        }

        public ActionResult Details(int ordersID)
        {
            List<tb_CHITIETHOADON> cthd = ecmMobile.tb_CHITIETHOADON.Where(p => p.IdHD == ordersID).ToList();


            return View(cthd);
        }
    }
}