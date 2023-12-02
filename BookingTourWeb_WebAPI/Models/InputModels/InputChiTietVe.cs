namespace BookingTourWeb_WebAPI.Models.InputModels
{
    public class InputChiTietVe
    {
        public long MaTaiKhoan { get; set; }
        public long MaVe { get; set; }
        public string MaChuyenBay { get; set; } = null!;
        public string LoaiVe { get; set; } = null!;

        public int SoLuong { get; set; }

        public string TinhTrang { get; set; } = null!;

        public decimal TongGia { get; set; }
        public string TenKh { get; set; } = null!;

        public string Sdt { get; set; } = null!;

        public string GmailKh { get; set; } = null!;

        public string Phai { get; set; } = null!;

        public string NgayDatVe { get; set; }
    }
}
