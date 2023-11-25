using BookingTourWeb_WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace BookingTourWeb_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly DvmayBayContext _context;
        private int _otp;

        public AuthController(DvmayBayContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task SendOtpAsync(InputForgetPassword request)
        {
            var mail = "ximvhs26092002@gmail.com";
            var pass = "niuiehecquymxqdr";
            var message = new MailMessage();
            message.From = new MailAddress(mail);
            message.To.Add(new MailAddress(request.email));
            message.Subject = "Reset password";
            message.Body = "<html><body> Your code is: " + request.message + "</body></html>";
            message.IsBodyHtml = true;
            this._otp = Int32.Parse(request.message);
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pass),
                EnableSsl = true
            };

            client.Send(message);

        }

        [HttpPost]
        public async Task<ActionResult> CheckOtpAsync(int input)
        {
            if (this._otp == input)
            {
                return Ok(true);
            }

            return NotFound(false);
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(InputLogin request)
        {
            var checkTK = await _context.Taikhoans.Where(x => x.TaiKhoan1 == request.TaiKhoan1).FirstOrDefaultAsync();
            if (checkTK != null)
            {
                var checkMK = checkTK.MatKhau == request.MatKhau;
                if (checkMK == true)
                {
                    return Ok(true);
                }
            }
            return NotFound(false);
        }

        [HttpPost]
        public async Task<ActionResult<InputRegister>> RegisterAsync(InputRegister request)
        {
            var taiKhoan = new Taikhoan()
            {
                MaTaiKhoan = 0,
                TaiKhoan1 = request.TaiKhoan1,
                MatKhau = request.MatKhau,
                VaiTro = 1,
            };
            _context.Taikhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();
            var khachHang = new Khachhang()
            {
                MaKh = 0,
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                TenKh = request.TenKH,
                Sdt = request.Sdt,
                GmailKh = request.GamilKH,
                Phai=request.Phai,
            };
            _context.Khachhangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return request;
        }

    }
}