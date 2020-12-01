using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetadata
        {
            [Required(ErrorMessage = "Tên sản phẩm sản phẩm không đc để trống !")]
            [DisplayName("Tên sản phẩm ")]
            public string TenSP { get; set; }
            [DisplayName("Giá sản phẩm ")]
            public Nullable<decimal> DonGia { get; set; }
            [DisplayName("Ngày cập nhập")]
            //[Required(ErrorMessage = "Ngày cập nhập sản phẩm không đc để trống !")]

            public Nullable<System.DateTime> NgayCapNhap { get; set; }
            [DisplayName("Cấu hình")]
            [Required(ErrorMessage = "Cấu hình sản phẩm không đc để trống !")]

            public string CauHinh { get; set; }
            [DisplayName("Mô tả")]
            [Required(ErrorMessage = "Mô tả sản phẩm không đc để trống !")]

            public string MoTa { get; set; }
            [DisplayName("Hình ảnh")]
            [Required(ErrorMessage = "Hình ảnh sản phẩm không đc để trống !")]

            public string HinhAnh { get; set; }
            [DisplayName("Số lượng")]
            public Nullable<int> SoLuongTon { get; set; }
            [DisplayName("Lượt xem ")]
            public Nullable<int> LuotXem { get; set; }
            [DisplayName("Lượt bình chọn")]
            public Nullable<int> LuotBinhChon { get; set; }
            [DisplayName("Lượt bình luận")]
            public Nullable<int> LuotBinhLuan { get; set; }
            [DisplayName("Đã bán")]
            public Nullable<int> SoLanMua { get; set; }
            [DisplayName("Mới")]
            public Nullable<int> Moi { get; set; }
            [DisplayName("Hình ảnh 1")]

            public string HinhAnh1 { get; set; }
            [DisplayName("Hình ảnh 2")]

            public string HinhAnh2 { get; set; }
            [DisplayName("Hình ảnh 3")]

            public string HinhAnh3 { get; set; }
            [DisplayName("Hình ảnh 4")]

            public string HinhAnh4 { get; set; }
            [DisplayName("Nhà cung cấp")]

            public Nullable<int> MaNCC { get; set; }
            [DisplayName("Nhà sản xuất")]

            public Nullable<int> MaNSX { get; set; }
            [DisplayName("Loại sản phẩm")]

            public Nullable<int> MaLoaiSP { get; set; }
        }
    }
}