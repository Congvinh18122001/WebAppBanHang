using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataTypeAttribute(typeof(DonDatHangMetadata))]
    public partial class DonDatHang
    {
        internal sealed class DonDatHangMetadata
        {
            public Nullable<System.DateTime> NgayDatHang { get; set; }
            public Nullable<bool> TrinhGiao { get; set; }
            public Nullable<System.DateTime> NgayGiao { get; set; }
            public Nullable<bool> DaThanhToan { get; set; }
            public Nullable<int> MaKH { get; set; }
            public Nullable<int> UuDai { get; set; }
            public Nullable<bool> DaHuy { get; set; }
        }
    }
}