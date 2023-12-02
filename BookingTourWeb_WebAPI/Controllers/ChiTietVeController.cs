using BookingTourWeb_WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingTourWeb_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChiTietVeController : ControllerBase
    {
        private readonly DvmayBayContext _context;

        public ChiTietVeController(DvmayBayContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<object>>> GetAllAsync()
        {
            var ves = _context.Ves.ToList().Join(_context.Khachhangs, x => x.MaKh, khachHang => khachHang.MaKh, (x, khachHang) => new
            {
                MaVe = x.MaVe,
                NgayDatVe = x.NgayDatVe,
                MaKh = x.MaKh,
                TenKhachHang = khachHang.TenKh,
            }).ToList();
            var chiTietVes = _context.Chitietves.ToList().Join(ves, x => x.MaVe, ve => ve.MaVe, (x, ve) => new
            {
                MaCTV = x.MaCTV,
                MaVe = x.MaVe,
                NgayDatVe = ve.NgayDatVe,
                MaKh = ve.MaKh,
                TenKhachHang = ve.TenKhachHang,
                MaChuyenBay = x.MaChuyenBay,
                LoaiVe = x.LoaiVe,
                SoLuong = x.SoLuong,
                TinhTrang = x.TinhTrang,
                TongGia = x.TongGia,
            }).ToList();
            return Ok(chiTietVes);
        }

        [HttpPost]
        public async Task<ActionResult<List<object>>> FilterChiTietVeAsync(InputFilterGuestsChuyenBay input)
        {
            var filterVe = await _context.Ves.Where(x => x.NgayDatVe >= DateTime.Parse(input.bookDate)).ToListAsync();
            var filterChiTietVes = await _context.Chitietves.Where(x => filterVe.Select(x => x.MaVe).Contains(x.MaVe)).ToListAsync();
            var chuyenBays = await _context.Chuyenbays.Where(x => x.NgayXuatPhat == DateTime.Parse(input.startDate) && x.NoiXuatPhat == input.fromPlace && x.NoiDen == input.toPlace).ToListAsync();
            var ves = filterVe.Join(_context.Khachhangs, x => x.MaKh, khachHang => khachHang.MaKh, (x, khachHang) => new
            {
                MaVe = x.MaVe,
                NgayDatVe = x.NgayDatVe,
                MaKh = x.MaKh,
                TenKhachHang = khachHang.TenKh,
            }).ToList();
            var chiTietVes = filterChiTietVes.Join(ves, x => x.MaVe, ve => ve.MaVe, (x, ve) => new
            {
                MaCTV = x.MaCTV,
                MaVe = x.MaVe,
                NgayDatVe = ve.NgayDatVe,
                MaKh = ve.MaKh,
                TenKhachHang = ve.TenKhachHang,
                MaChuyenBay = x.MaChuyenBay,
                LoaiVe = x.LoaiVe,
                SoLuong = x.SoLuong,
                TinhTrang = x.TinhTrang,
                TongGia = x.TongGia,
            }).Where(x => chuyenBays.Select(xx => xx.MaChuyenBay).Contains(x.MaChuyenBay) && x.LoaiVe == input.typeSeat).ToList();

            return Ok(chiTietVes);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(long inputMaCTV)
        {
            var MachiTietVe = await _context.Chitietves.Where(x => x.MaCTV == inputMaCTV).FirstOrDefaultAsync();
            if (MachiTietVe == null)
            {
                return NotFound(MachiTietVe);
            }
            _context.Chitietves.Remove(MachiTietVe);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(Chitietve input)
        {
            if (input == null)
            {
                return StatusCode(400);
            }
            _context.Chitietves.Update(input);
            await _context.SaveChangesAsync();
            return Ok(input);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(InputChiTietVe request)
        {
            var kh = await _context.Khachhangs.Where(x => x.GmailKh == request.GmailKh).FirstOrDefaultAsync();
            var newVe = new Ve() { MaVe = request.MaVe, MaKh= kh.MaKh, MaKhNavigation= kh, NgayDatVe= DateTime.Parse(request.NgayDatVe)};
            await _context.Ves.AddAsync(newVe);
            _context.SaveChanges();
            var newCTV = new Chitietve() { MaCTV = 0, MaVe = newVe.MaVe, LoaiVe = request.LoaiVe, MaChuyenBay=request.MaChuyenBay, SoLuong = request.SoLuong, TinhTrang= request.TinhTrang, TongGia=request.TongGia };
            await _context.Chitietves.AddAsync(newCTV);
            var chuyenBay = await _context.Chuyenbays.Where(x => x.MaChuyenBay == request.MaChuyenBay).FirstOrDefaultAsync();
            if(request.LoaiVe == "BSN")
            {
                chuyenBay.SoLuongVeBsn = chuyenBay.SoLuongVeBsn - request.SoLuong;
            }
            else
            {
                chuyenBay.SoLuongVeEco = chuyenBay.SoLuongVeEco - request.SoLuong;
            }
            _context.Chuyenbays.Update(chuyenBay);
            _context.SaveChanges();
            return Ok(newCTV);
        }
    }
}