using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Member")]

    public class AddPhieuNhapController : Controller
    {
        DBBanHangEntities1 db = new DatabaseEntity().GetData();
        // GET: Admin/PhieuNhaps
        public ActionResult Index()
        {

            ViewBag.MaSP = db.SanPhams.ToList();
            ViewBag.MaNCC = db.NhaCungCaps.ToList();
            return View();
        }
    
        [HttpPost]
        public ActionResult ThemPhieuNhap(PhieuNhap phieuNhap , List<ChiTietPhieuNhap> chiTiets)
        {
            if (chiTiets != null && phieuNhap!=null)
            {
                phieuNhap.DaXoa = false;
            db.PhieuNhaps.Add(phieuNhap);
            db.SaveChanges();
            SanPham sanPham;
            
                foreach (var item in chiTiets)
                {
                    item.MaPN = phieuNhap.MaPN;
                    sanPham = db.SanPhams.SingleOrDefault(p => p.MaSP == item.MaSP);
                    sanPham.SoLuongTon = sanPham.SoLuongTon + (int)item.SoLuongNhap;
                    sanPham.DonGia = (decimal)item.DonGiaNHap;
                    sanPham.NgayCapNhap = DateTime.Now;
                }
                db.ChiTietPhieuNhaps.AddRange(chiTiets);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index","PhieuNhaps");
        }
    }
}