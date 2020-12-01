using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles ="Member")]
    public class ThongKeController : Controller
    {
        DBBanHangEntities1 db = new DatabaseEntity().GetData();
        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            ViewBag.LuotTruyCap = HttpContext.Application["LuotTruyCap"].ToString();
            ViewBag.IsOnline = HttpContext.Application["DangHoatDong"].ToString();
            ViewBag.TongDoanhThu = TongDoanhThu();
            ViewBag.TongSoDonHang = TongSoDonHang();
            ViewBag.TongSoThanhVien = TongSoThanhVien();
            return View();
        }

        public decimal TongDoanhThu()
        {
            if (db.ChiTietDonDatHangs.Count()==0)
            {
                return 0;
            }
            return decimal.Parse(db.ChiTietDonDatHangs.Sum(p => p.SoLuong * p.DonGia).Value.ToString());
        }
        public int TongSoDonHang()
        {
            if (db.DonDatHangs.Count() == 0)
            {
                return 0;
            }
            return db.DonDatHangs.Count();
        }
        public int TongSoThanhVien()
        {
            return db.ThanhViens.Count();
        }
    }
}