using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataType(typeof(NhaSanXuatMetadata))]
    public partial class NhaSanXuat
    {
        internal sealed class NhaSanXuatMetadata
        {
            [DisplayName("Nhà sản xuất")]
            public string TenNSX { get; set; }
            [DisplayName("Thông tin")]
            public string ThongTin { get; set; }
            public string Logo { get; set; }
        }
    }
}