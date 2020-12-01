using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Web.Security;

namespace WebBanHang.Areas.Clience.Controllers
{
    public class ClienceController : Controller
    {
        // GET: Clience/SanPham
        DBBanHangEntities1 DB= new DatabaseEntity().GetData();
        public ActionResult Home()
        {
            var ListNP = DB.SanPhams.Where(p => p.DaXoa != true && p.Moi == 1 && p.LoaiSanPham.MaLoaiSP == 2).OrderByDescending(p => p.NgayCapNhap).ToList();
            ViewBag.ListNP = ListNP;
            var ListNL = DB.SanPhams.Where(p => p.DaXoa != true && p.Moi == 1 && p.LoaiSanPham.MaLoaiSP == 1).OrderByDescending(p => p.NgayCapNhap).ToList();
            ViewBag.ListNL = ListNL;
            return View();
        }
        [ChildActionOnly]

        public ActionResult ProductStylePatial1()
        {
            
            return View();
        }
        [ChildActionOnly]
        public ActionResult ProductStylePatial2()
        {

            return View();
        }
        public ActionResult MenuPartial()
        {
            var ListProduct = DB.SanPhams;
            return View(ListProduct);
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.CauHoi = new SelectList(CauHois());
            return View();
        }

        [HttpPost]
        public ActionResult Register(ThanhVien  tv)
        {
            ViewBag.CauHoi = new SelectList(CauHois());
            if (this.IsCaptchaValid("Captcha is Valid"))
            {
                ThanhVien thanhVien = DB.ThanhViens.SingleOrDefault(p=>p.TaiKhoan==tv.TaiKhoan);
                if (thanhVien != null)
                {
                    ViewBag.ThongBao = "Đăng ký Thất bại (Tài khoản đã tồn tại !)";
                    return View();
                }
                thanhVien = DB.ThanhViens.SingleOrDefault(p => p.Email == tv.Email);
                if (thanhVien != null)
                {
                    ViewBag.ThongBao = "Đăng ký Thất bại (Email đã tồn tại !)";
                    return View();
                }
                thanhVien = DB.ThanhViens.SingleOrDefault(p => p.DienThoai == tv.DienThoai);
                if (thanhVien != null)
                {
                    ViewBag.ThongBao = "Đăng ký Thất bại (Số điện thoại đã tồn tại !)";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    ViewBag.ThongBao = "Thêm thành công";
                    tv.MatKhau = GetMD5(tv.MatKhau);
                    DB.ThanhViens.Add(tv);
                    DB.SaveChanges();
                    KhachHang khachHang = new KhachHang();
                    khachHang.DiaChi = tv.DiaChi;
                    khachHang.TenKH = tv.HoTen;
                    khachHang.MaThanhVien = tv.MaThanhVien;
                    khachHang.Email = tv.Email;
                    khachHang.DienThoai = tv.DienThoai;
                    DB.KhachHangs.Add(khachHang);
                    DB.SaveChanges();
                    return RedirectToAction("login", tv);
                }
                else
                {
                    ViewBag.ThongBao = "Đăng ký Thất bại";
                    return View();
                }
                

            }
            ViewBag.ThongBao = "Sai mã captcha";
            return View();
        }

        public List<string> CauHois()
        {
            List<string> cauhois = new List<string>() {"Con vật yêu thích nhất ?"," Các màu sác bạn thích nhất?" };
            return cauhois;
        }
        [HttpPost]
        public ActionResult login(FormCollection f)
        {
            string username = f["username"].ToString();
            string password = GetMD5(f["password"].ToString());
            ThanhVien tv = DB.ThanhViens.SingleOrDefault(p=>p.TaiKhoan==username&&p.MatKhau==password);
            if (tv!=null)
            {
                var listQuyen = DB.LoaiThanhVien_Quyen.Where(p=>p.MaLoaiTV==tv.MaLoaiTV);
                string Quyen = "";
                foreach (var item in listQuyen)
                {
                    Quyen += item.MaQuyen + ",";
                }
                Quyen = Quyen.Substring(0,Quyen.Length-1);
                PhanQuyen(tv.TaiKhoan,Quyen);
                Session["DangNhap"] = tv;
                return Content("<script> window.location.reload(); </script>");
            }
            return Content("Thông tin đăng nhập bị sai .");
        }
        public void PhanQuyen(string TaiKhoan , string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(
                1,
                TaiKhoan,
                DateTime.Now,
                DateTime.Now.AddHours(3),
                false,
                Quyen,
                FormsAuthentication.FormsCookiePath
                );
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,FormsAuthentication.Encrypt(ticket));

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            Response.Cookies.Add(cookie);

        }


        public ActionResult login(int MaThanhVien)
        {
            if (MaThanhVien!=null)
            {
              ThanhVien  thanhVien = DB.ThanhViens.SingleOrDefault(p => p.MaThanhVien == MaThanhVien);
                Session["DangNhap"] = thanhVien;
            }
            return RedirectToAction("Index", "Product");
        }
        public ActionResult KhongDuQuyen()
        {
            return View();
        }
        public ActionResult logout()
        {
            Session["DangNhap"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Home");

        }
        public  string GetMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }

            return sbHash.ToString();

        }        
        public ActionResult PersonalInfor()
        {
            if (Session["DangNhap"] != null && Session["DangNhap"] != "")
            {
                ThanhVien thanhVien = Session["DangNhap"] as ThanhVien;
                return View(thanhVien);
            }
            return RedirectToAction("Home");
        }


    }
}