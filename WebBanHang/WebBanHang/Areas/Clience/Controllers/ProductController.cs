using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using PagedList;
namespace WebBanHang.Areas.Clience.Controllers
{
    public class ProductController : Controller
    {
        // GET: Clience/Product
        DBBanHangEntities1 DB = new DatabaseEntity().GetData();
        int pagasize = 12;
        public ActionResult Index(int? page)
        {
            var ListProduct = DB.SanPhams.Where(p=>p.SoLuongTon>0&&p.DaXoa!=true)
                .OrderByDescending(p => p.NgayCapNhap).ToList();
            ViewBag.Title = "Tất cả sản phẩm";
            int pageNumber = (page ?? 1);
            return View(ListProduct.ToPagedList(pageNumber,pagasize));
        }

        public ActionResult ShowWithTypeProduct(int? MaLoaiSP,int? page)
        {
            var ListProduct = DB.SanPhams.Where(p => p.SoLuongTon > 0 && p.DaXoa != true&&p.MaLoaiSP==MaLoaiSP)
                 .OrderByDescending(p => p.NgayCapNhap)
                .ToList();
            LoaiSanPham sp = DB.LoaiSanPhams.SingleOrDefault(p => p.MaLoaiSP == MaLoaiSP);
            ViewBag.Title = sp.TenLoai;
            ViewBag.MaLoaiSP = MaLoaiSP;
            int pageNumber = (page ?? 1);
            return View(ListProduct.ToPagedList(pageNumber, pagasize));
        }

        public ActionResult ShowWithNCC(int? MaNCC, int? MaLoaiSP,int? page)
        {
            var ListProduct = DB.SanPhams.Where(p => p.SoLuongTon > 0 && p.DaXoa != true && p.MaNCC == MaNCC && p.MaLoaiSP == MaLoaiSP)
                 .OrderByDescending(p => p.NgayCapNhap)
                .ToList();
            NhaCungCap sp = DB.NhaCungCaps.SingleOrDefault(p => p.MaNCC == MaNCC);
            LoaiSanPham lsp = DB.LoaiSanPhams.SingleOrDefault(p => p.MaLoaiSP == MaLoaiSP);
            ViewBag.Title = "Sản phẩm " +lsp.TenLoai+ " của nhà cung cấp : " + sp.TenNCC;
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNCC = MaNCC;
            int pageNumber = (page ?? 1);
            return View(ListProduct.ToPagedList(pageNumber, pagasize));
        }

        public ActionResult ShowWithNSX(int? MaNSX , int? MaLoaiSP,int? page)
        {
            IEnumerable<SanPham> ListProduct;
            NhaSanXuat sp = DB.NhaSanXuats.Single(p => p.MaNSX == MaNSX);
            LoaiSanPham lsp = DB.LoaiSanPhams.SingleOrDefault(p => p.MaLoaiSP == MaLoaiSP);
            if (MaLoaiSP.HasValue)
            {
                 ListProduct = DB.SanPhams.Where(p => p.SoLuongTon > 0 && p.DaXoa != true && p.MaNSX == MaNSX && p.MaLoaiSP == MaLoaiSP)
                 .OrderByDescending(p => p.NgayCapNhap)
                .ToList();

                ViewBag.Title = "Sản phẩm " + lsp.TenLoai + " của nhà sản xuất : " + sp.TenNSX;
            }
            else
            {
                 ListProduct = DB.SanPhams.Where(p => p.SoLuongTon > 0 && p.DaXoa != true && p.MaNSX == MaNSX)
                 .OrderByDescending(p => p.NgayCapNhap)
                .ToList();

                ViewBag.Title = "Nhà sản xuất : " + sp.TenNSX;
            }
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            int pageNumber = (page ?? 1);
            return View(ListProduct.ToPagedList(pageNumber, pagasize));
        }

        public ActionResult ProductDetail(int? MaSP)
        {
            if (MaSP.HasValue)
            {
                SanPham product = DB.SanPhams.SingleOrDefault(p => p.MaSP == MaSP.Value);
                if (product.LuotXem != null)
                {
                    product.LuotXem++;
                }
                product.LuotXem=1;
                DB.SaveChanges();
                ViewBag.SPBanChay = DB.SanPhams.OrderByDescending(p => p.SoLanMua).Take(3).ToList();
                return View(product);
            }
           
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult CategoriesMenu()
        {
            var listProduct = DB.SanPhams;
            ViewBag.LoaiSP = DB.LoaiSanPhams;
            ViewBag.NSX = DB.NhaSanXuats;
            return View(listProduct);
        }
        public ActionResult Search(string strSearch,int? page)
        {
            var ListSanPham = DB.SanPhams.Where(p=>p.TenSP.Contains(strSearch)).ToList();
            int pageNumber = (page ?? 1);
            ViewBag.strSearch = strSearch;
            return View(ListSanPham.ToPagedList(pageNumber, pagasize));
        }
    }
}