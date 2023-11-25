using System;
using System.Collections.Generic;

namespace BookingTourWeb_WebAPI.Models;

public partial class Maybay
{
    public string MaMayBay { get; set; } = null!;

    public string TenMayBay { get; set; } = null!;

    public int SlgheBsn { get; set; }

    public int SlgheEco { get; set; }

    //public virtual ICollection<Chuyenbay> Chuyenbays { get; set; } = new List<Chuyenbay>();
}
