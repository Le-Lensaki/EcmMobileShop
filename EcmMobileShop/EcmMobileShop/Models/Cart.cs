using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcmMobileShop.Models;

namespace EcmMobileShop.Models
{
    public class Cart
    {
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();

        private List<CartItem> products;

        public Cart()
        {
            products = new List<CartItem>();
            List<tb_SANPHAM> listSP = ecmMobile.tb_SANPHAM.ToList();
            List<tb_CT_SANPHAM> listctSP = ecmMobile.tb_CT_SANPHAM.ToList();          
            foreach (var item in listSP)
            {
                foreach (var ctsp in listctSP)
                {
                    if (ctsp.IdSP == item.IdSP)
                    {
                        CartItem cartItem = new CartItem();

                        cartItem.IdSP = item.IdSP;
                        cartItem.IdctSP = ctsp.IdctSP;
                        cartItem.TenSP = item.TenSP;
                        cartItem.MauSac = ctsp.MauSac;
                        cartItem.IdHangSP = item.IdHangSP;
                        cartItem.IdLoaiSP = item.IdLoaiSP;
                        cartItem.MoTaSP = item.MoTaSP;
                        cartItem.Gia = item.Gia;
                        cartItem.ImagePathMain = item.ImagePathMain;
                        cartItem.NgayNhap = item.NgayNhap;
                        cartItem.TrangThai = item.TrangThai;
                        cartItem.SoLuong = 1;
                        products.Add(cartItem);
                    }
                }
                           
            }
        }
        public List<CartItem> FindAll()
        {
            return this.products;
        }
        public CartItem FindProduct(int id)
        {
            return this.products.Single(p => p.IdSP.Equals(id));
        }

    }
}