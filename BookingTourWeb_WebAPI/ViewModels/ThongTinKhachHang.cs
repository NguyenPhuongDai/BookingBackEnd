using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TourBookingWeb_API.ViewModels
{
    public class ThongTinKhachHang
    {
        [Key]
        public long Makhachhang { get; set; }
        public long MaTaiKhoan { get; set; }
        public string HoTenKh { get; set; } = null!;
        public string Phai { get; set; }
        public string Sdt { get; set; } = null!;
        public string GmailKh { get; set; } = null!;



        public string MaChuyenBay { get; set; } = null!;

        public long MaVe { get; set; }
    }
}
