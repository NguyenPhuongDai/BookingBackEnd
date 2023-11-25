using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace BookingTourWeb_WebAPI.ViewModels
{
    public class ThongTinChuyenBay
    {
        [Key]
        public string MaChuyenBay { get; set; } = null!;

        public string MaMayBay { get; set; } = null!;
        [JsonIgnore]
        [AllowNull]
        public string TenMayBay { get; set; } = System.String.Empty;

        public TimeSpan GioBay { get; set; }

        public string NoiXuatPhat { get; set; } = null!;

        public string NoiDen { get; set; } = null!;

        public DateTime NgayXuatPhat { get; set; }

        public decimal DonGia { get; set; }

        public int SoLuongVeBsn { get; set; }

        public int SoLuongVeEco { get; set; }
    }
}
