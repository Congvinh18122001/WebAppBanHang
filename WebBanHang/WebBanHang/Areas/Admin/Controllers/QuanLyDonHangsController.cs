using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using PagedList;
namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Member")]

    public class QuanLyDonHangsController : Controller
    {
        DBBanHangEntities1 db = new DatabaseEntity().GetData();
        // GET: Admin/QuanLyDonHnangs
        public ActionResult ShowDonDatHang(int? MaDDH)
        {
            DonDatHang donhang = db.DonDatHangs.SingleOrDefault(p=>p.MaDDH==MaDDH);
            return View(donhang);
        }
        public ActionResult LuuDonHang(DonDatHang donHang)
        {
            DonDatHang  dh = db.DonDatHangs.Single(p=>p.MaDDH==donHang.MaDDH);
            dh.DaThanhToan = donHang.DaThanhToan;
            dh.DaHuy = donHang.DaHuy;

            if (dh.TrinhGiao==false && donHang.TrinhGiao==true)
            {
                dh.NgayGiao = DateTime.Now;
            }
            dh.TrinhGiao = donHang.TrinhGiao;
            db.SaveChanges();
            return RedirectToAction("DonHangHoanThanh");
        }
        int pageSize = 8;
        public  ActionResult DonHangHoanThanh(int? page)
        {
            List<DonDatHang> donDatHangs = db.DonDatHangs.Where(p=>p.DaThanhToan==true && p.TrinhGiao==true).OrderByDescending(p => p.NgayGiao).ToList();
            ViewBag.Title = "Đã hoàn thành .";
            ViewBag.isLink = "a";
            int pageNumber = (page ?? 1);
            return View(donDatHangs.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult DonHangChưaThanhToans(int? page)
        {
            List<DonDatHang> donDatHangs = db.DonDatHangs.Where(p => p.DaThanhToan != true && p.TrinhGiao == true).OrderByDescending(p=>p.NgayGiao).ToList();
            ViewBag.Title = "Chưa thanh toán & Đã giao!";
            int pageNumber = (page ?? 1);
            ViewBag.isLink = "b";
            return View("DonHangHoanThanh",donDatHangs.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DonHangChuaGiao(int? page)
        {
            List<DonDatHang> donDatHangs = db.DonDatHangs.Where(p => p.DaThanhToan == true && p.TrinhGiao != true).OrderByDescending(p => p.NgayDatHang).ToList();
            ViewBag.Title = "Đã Thanh toán & Chưa giáo .";
            ViewBag.isLink = "c";
            int pageNumber = (page ?? 1);
            return View("DonHangHoanThanh", donDatHangs.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ChoDuyet(int? page)
        {
            List<DonDatHang> donDatHangs = db.DonDatHangs.Where(p => p.DaThanhToan != true && p.TrinhGiao != true).OrderByDescending(p => p.NgayDatHang).ToList();
            ViewBag.Title = "Chờ Duyệt";
            ViewBag.isLink = "d";
            int pageNumber = (page ?? 1);
            return View("DonHangHoanThanh", donDatHangs.ToPagedList(pageNumber, pageSize));
        }




    }
}