using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            [DisplayName("Tài Khoản")]
            [Required(ErrorMessage = "Tài Khoản không để trống")]
            public string TaiKhoan { get; set; }
            [DisplayName("Mật khẩu")]
            [Required(ErrorMessage = "Mật Khảu không để trống")]
            public string MatKhau { get; set; }
            [DisplayName("Họ tên")]
            [Required(ErrorMessage = "Họ tên không để trống")]
            public string HoTen { get; set; }
            [DisplayName("Địa chỉ")]
            [Required(ErrorMessage = "Địa chỉ không để trống")]
            public string DiaChi { get; set; }
            [Required(ErrorMessage = "Email không để trống")]

            [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$")]
            public string Email { get; set; }
            [DisplayName("Số điện thoại")]
            [Required(ErrorMessage = "Số điện thoại không để trống")]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                       ErrorMessage = "Số điện thoại nhập không dúng")]
            [StringLength(10, ErrorMessage = "Số điện thoại gồm 10 số")]
            public string DienThoai { get; set; }
            [DisplayName("Câu hỏi bí mật")]
            public string CauHoi { get; set; }
            [DisplayName("Câu trả lời")]
            [Required(ErrorMessage = "Câu trả lời không để trống")]
            public string cauTraLoi { get; set; }
        }
    }
}