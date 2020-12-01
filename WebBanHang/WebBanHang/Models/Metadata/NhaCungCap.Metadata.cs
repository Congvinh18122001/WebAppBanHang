using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataType(typeof(NhaCungCapMetadata))]
    public partial class NhaCungCap
    {
        internal sealed class NhaCungCapMetadata
        {
            [DisplayName("Nhà cung cấp")]
            public string TenNCC { get; set; }
            [DisplayName("Địa chỉ")]
            public string DiaChi { get; set; }
            public string Email { get; set; }
            [DisplayName("Số điện thoại")]
            public string Sdt { get; set; }
        }

    }
}