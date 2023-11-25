using System;
using System.Collections.Generic;

namespace BookingTourWeb_WebAPI.Models;

public partial class Khachhang
{
    public long MaKh { get; set; }

    public long MaTaiKhoan { get; set; }

    public string TenKh { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string GmailKh { get; set; } = null!;

    public string Phai { get; set; } = null!;

    public virtual Taikhoan MaTaiKhoanNavigation { get; set; } = null!;

    public virtual ICollection<Ve> Ves { get; set; } = new List<Ve>();
}
