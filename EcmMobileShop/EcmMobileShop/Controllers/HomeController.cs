using EcmMobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace EcmMobileShop.Controllers
{
   
    public class HomeController : Controller
    {
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();

        [ChildActionOnly]
        public ActionResult _Header()
        {

            return PartialView("_Header", ecmMobile.tb_LOAISP.ToList());
        }

        [ChildActionOnly]
        public ActionResult _Footer()
        {
            var loaiSP = ecmMobile.tb_LOAISP.ToList();
            var hangSP = ecmMobile.tb_HANGSP.ToList();
            var modelCollection = new ModelCollection();
            modelCollection.AddModel(loaiSP);
            modelCollection.AddModel(hangSP);

            return PartialView("_Footer", modelCollection);
        }

        public ActionResult Index()
        {
            var loaiSP = ecmMobile.tb_LOAISP.ToList();
            return View(loaiSP);
        }

        public ActionResult Shop(int? page, int? idLoai, int? idHang)
        {
           
            if (page == null) page = 1;

            if (idLoai != null)
            {
                var mobile = ecmMobile.tb_SANPHAM.OrderBy(b => b.IdSP).Where(p => p.IdLoaiSP == idLoai);

                int pageSize = 15;
                int pageNumber = (page ?? 1);


                return View(mobile.ToPagedList(pageNumber, pageSize));
            } if(idHang != null)
            {
                var mobile = ecmMobile.tb_SANPHAM.OrderBy(b => b.IdSP).Where(p => p.IdHangSP == idHang);

                int pageSize = 15;
                int pageNumber = (page ?? 1);


                return View(mobile.ToPagedList(pageNumber, pageSize));
            }    
            else {
                var mobile = ecmMobile.tb_SANPHAM.OrderBy(b => b.IdSP);
                int a = mobile.Count();

                int pageSize = 15;
                int pageNumber = (page ?? 1);


                return View(mobile.ToPagedList(pageNumber, pageSize));
            }

            
        }
        public ActionResult Product(int id)
        {
            var product = ecmMobile.tb_SANPHAM.Single(p => p.IdSP == id);
            List<tb_SANPHAM> tb_SANPHAMs = new List<tb_SANPHAM>();
            tb_SANPHAMs.Add(product);
            var listLoai = ecmMobile.tb_LOAISP.ToList();
            var listHang = ecmMobile.tb_HANGSP.ToList();
            var listCTSP = ecmMobile.tb_CT_SANPHAM.Where(p => p.IdSP == id).ToList();

            var modelCollection = new ModelCollection();
            modelCollection.AddModel(tb_SANPHAMs);
            modelCollection.AddModel(listLoai);
            modelCollection.AddModel(listHang);
            modelCollection.AddModel(listCTSP);

            return View(modelCollection);
        }

        public ActionResult Cart()
        {
            List<CartItem> ShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if(ShoppingCart == null)
            {
                ShoppingCart = new List<CartItem>();
            }    
            var modelCollection = new ModelCollection();
            modelCollection.AddModel(ShoppingCart);
            return View(modelCollection);       
        }
        public RedirectToRouteResult RemoveItem(int ProductId)
        {
            List<CartItem> shoppingCart = Session["ShoppingCart"] as List<CartItem>;
            CartItem DelItem = shoppingCart.FirstOrDefault(m => m.IdSP == ProductId);
            if (DelItem != null)
            {
                shoppingCart.Remove(DelItem);
            }
            return RedirectToAction("Cart", "Home");
        }

        public RedirectToRouteResult AddToCart(int ProductId,string mausac)
        {
            Cart cart = new Cart(); 
            if (Session["ShoppingCart"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["ShoppingCart"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> ShoppingCart = Session["ShoppingCart"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (ShoppingCart.FirstOrDefault(m => m.IdSP == ProductId && m.MauSac == mausac) == null) // ko co sp nay trong gio hang
            {
                CartItem prodouct = cart.FindProduct(ProductId);  // tim sp theo sanPhamID

                

                ShoppingCart.Add(prodouct);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem prodouct = ShoppingCart.FirstOrDefault(m => m.IdSP == ProductId);
                prodouct.SoLuong++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("Cart", "Home");
            //return RedirectToRoute(Request.UrlReferrer.ToString());
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}