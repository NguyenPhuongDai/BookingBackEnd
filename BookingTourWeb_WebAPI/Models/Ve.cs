using System;
using System.Collections.Generic;

namespace BookingTourWeb_WebAPI.Models;

public partial class Ve
{
    public long MaVe { get; set; }

    public long MaKh { get; set; }

    public DateTime NgayDatVe { get; set; }

    public virtual Khachhang MaKhNavigation { get; set; } = null!;
}
