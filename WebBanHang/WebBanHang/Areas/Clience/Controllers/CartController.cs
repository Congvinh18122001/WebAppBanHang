using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Areas.Clience.Models;
using WebBanHang.Models;

namespace WebBanHang.Areas.Clience.Controllers
{
    public class CartController : Controller
    {
        DBBanHangEntities1 DB = new DatabaseEntity().GetData();
        public List<ItemCartViewModel> GetCart()
        {
            List<ItemCartViewModel> itemCarts = Session["Cart"] as List<ItemCartViewModel>;
            if (itemCarts == null)
            {
                itemCarts = new List<ItemCartViewModel>();
                Session["Cart"] = itemCarts;
            }
            return itemCarts;
        }
        public ActionResult AddToCart(int? MaSP , string strUrl)
        {
            SanPham findSP = DB.SanPhams.SingleOrDefault(p=>p.MaSP==MaSP);
            if (findSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemCartViewModel> itemCarts = GetCart();
            ItemCartViewModel findItemCart = itemCarts.SingleOrDefault(p=>p.MaSP==MaSP);
            
            if (findItemCart != null)
            {

                    if (findSP.SoLuongTon <= findItemCart.SoLuong)
                    {
                        return Content("Sản phẩm tồn Không đủ !");
                    }
                findItemCart.SoLuong++;
                findItemCart.ThanhTien = findItemCart.DonGia * findItemCart.SoLuong;
                return Redirect(strUrl);
            }

                ItemCartViewModel itemCart = new ItemCartViewModel(MaSP.Value);
                itemCarts.Add(itemCart);
               return Redirect(strUrl);

        }
        public int TongSoLuong()
        {
            List<ItemCartViewModel> itemCarts = Session["Cart"] as List<ItemCartViewModel>;
            if (itemCarts == null)
            {
                return 0;
            }
            return itemCarts.Sum(p=>p.SoLuong);

        }
        public decimal TongTien()
        {
            List<ItemCartViewModel> itemCarts = Session["Cart"] as List<ItemCartViewModel>;
            if (itemCarts == null)
            {
                return 0;
            }
            return itemCarts.Sum(p => p.ThanhTien);
        }
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        // GET: Clience/Cart
        public ActionResult ShowCart()
        {
            List<ItemCartViewModel> itemCarts = Session["Cart"] as List<ItemCartViewModel>;
            ViewBag.SoLuongs = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(itemCarts);
        }
        public ActionResult CartPartial()
        {
            ViewBag.SoLuongs = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View();
        }
        public ActionResult EditCart(int? MaSP)
        {
            SanPham findSP = DB.SanPhams.SingleOrDefault(p => p.MaSP == MaSP);
            if (findSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (Session["Cart"] == null )
            {
                return RedirectToAction("ShowCart");
            }
            List<ItemCartViewModel> itemCarts = Session["Cart"] as List<ItemCartViewModel>;
            ItemCartViewModel itemCart = itemCarts.SingleOrDefault(p => p.MaSP == MaSP);
            if (itemCart == null)
            {
                return RedirectToAction("ShowCart");
            }
            ViewBag.Cart = itemCarts;
            return View(itemCart);
        }
        public ActionResult UpdateCart(ItemCartViewModel itemCart)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowCart");
            }

            SanPham sanPham = DB.SanPhams.SingleOrDefault(p => p.MaSP == itemCart.MaSP);
            if (sanPham==null)
            {
                return RedirectToAction("ShowCart");
            }
            if (itemCart.SoLuong < 1 || !IsNumber(itemCart.SoLuong.ToString()))
            {
                return RedirectToAction("DeleteItemCart", new { MaSP = itemCart.MaSP });
            }
            if (sanPham.SoLuongTon < itemCart.SoLuong)
            {
                return Content("Số lượng sản phẩm tồn không đủ !");
            }

            List<ItemCartViewModel> itemCarts = GetCart();

            ItemCartViewModel updateItemCart = itemCarts.Find(p=>p.MaSP==itemCart.MaSP);
            updateItemCart.SoLuong = itemCart.SoLuong;
            updateItemCart.ThanhTien = itemCart.SoLuong * updateItemCart.DonGia;
            return RedirectToAction("ShowCart");
        }
        public ActionResult DeleteItemCart(int? MaSP)
        {
            if (!MaSP.HasValue)
            {
                return RedirectToAction("ShowCart");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowCart");
            }

            SanPham sanPham = DB.SanPhams.SingleOrDefault(p => p.MaSP == MaSP);
            if (sanPham == null)
            {
                return RedirectToAction("ShowCart");
            }
            List<ItemCartViewModel> itemCarts = GetCart();

            ItemCartViewModel updateItemCart = itemCarts.Find(p => p.MaSP == MaSP);
            if (updateItemCart == null)
            {
                return RedirectToAction("ShowCart");
            }
            itemCarts.Remove(updateItemCart);
            return RedirectToAction("ShowCart");
        }
        public KhachHang IsKhachHang(KhachHang khachHang)
        {
            KhachHang isKhach = DB.KhachHangs.SingleOrDefault(p=>p.DienThoai==khachHang.DienThoai);
            if (isKhach != null)
            {
                return isKhach;
            }
            DB.KhachHangs.Add(khachHang);
            DB.SaveChanges();
            return khachHang;
        }
        public ActionResult Order(FormCollection f)
        {
            KhachHang khachHang;

            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowCart");
            }
            List<ItemCartViewModel> itemCarts = GetCart();
            if (Session["DangNhap"] != null)
            {
                ThanhVien thanhVien = Session["DangNhap"] as ThanhVien;
                khachHang = DB.KhachHangs.SingleOrDefault(p => p.MaThanhVien == thanhVien.MaThanhVien);
            }
            else
            {
                if (String.IsNullOrEmpty(f["TenKH"]) || String.IsNullOrEmpty(f["DiaChi"]) || String.IsNullOrEmpty(f["DienThoai"]) || String.IsNullOrEmpty(f["Email"]))
                {
                    return Content("Vui lòng điền đủ thông tin !");
                }
                khachHang = new KhachHang();
                khachHang.TenKH = f["TenKH"];
                khachHang.DiaChi = f["DiaChi"];
                khachHang.DienThoai = f["DienThoai"];
                khachHang.Email = f["Email"];
                khachHang = IsKhachHang(khachHang);
            }
           
            DonDatHang donDatHang = new DonDatHang();
            donDatHang.NgayDatHang = DateTime.Now;
            donDatHang.TrinhGiao = false;
            donDatHang.DaThanhToan = false;
            if (khachHang != null)
            {
                donDatHang.MaKH = khachHang.MaKH;
            }
            donDatHang.DaHuy = false;
            DB.DonDatHangs.Add(donDatHang);
            DB.SaveChanges();
            foreach (var itemCart in itemCarts)
            {
                ChiTietDonDatHang chiTiet = new ChiTietDonDatHang();
                chiTiet.MaDDH = donDatHang.MaDDH;
                chiTiet.MaSP = itemCart.MaSP;
                chiTiet.DonGia = itemCart.DonGia;
                chiTiet.SoLuong = itemCart.SoLuong;
                DB.ChiTietDonDatHangs.Add(chiTiet);
            }
            DB.SaveChanges();
            Session["Cart"] = null;
            return Content("<script>window.location.reload()</script>"); 
            //return RedirectToRoute("NotifiCation", new  { noification = "Đặt hàng thành công" });
        }
        public ActionResult NotifiCation(string noification)
        {
            ViewBag.notification = noification;
            return View();
        }
    }
}