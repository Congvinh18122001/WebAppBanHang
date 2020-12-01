using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models;
namespace WebBanHang.Areas.Clience.Models
{
    public class ItemCartViewModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
        DBBanHangEntities1 DB = new DatabaseEntity().GetData();
        public ItemCartViewModel(int MaSP , int sl)
        {
            this.MaSP = MaSP;
            SanPham sanPham = DB.SanPhams.Single(p=>p.MaSP==MaSP);
            this.TenSP = sanPham.TenSP;
            this.SoLuong = sl;
            this.DonGia = sanPham.DonGia.Value;
            this.ThanhTien = SoLuong * DonGia;
            this.HinhAnh = sanPham.HinhAnh;
        }
        public ItemCartViewModel(int MaSP)
        {
            this.MaSP = MaSP;
            SanPham sanPham = DB.SanPhams.Single(p => p.MaSP == MaSP);
            this.TenSP = sanPham.TenSP;
            this.SoLuong = 1;
            this.DonGia = sanPham.DonGia.Value;
            this.ThanhTien = SoLuong * DonGia;
            this.HinhAnh = sanPham.HinhAnh;
        }
        public ItemCartViewModel()
        {
        }
    }
}