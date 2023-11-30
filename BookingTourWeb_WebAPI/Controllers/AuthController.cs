using BookingTourWeb_WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace BookingTourWeb_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly DvmayBayContext _context;
        private readonly IConfiguration _config;
        private int _otp;

        public AuthController(DvmayBayContext context, IConfiguration config)
        {
            this._context = context;
            this._config = config;
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
                    var token = checkTK.MaTaiKhoan.ToString() + ' ' + GenerateToken(checkTK)+ ' ' + checkTK.VaiTro.ToString();
                    return Ok(token);
                }
            }
            return Ok(false);
        }

        private string GenerateToken(Taikhoan user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.TaiKhoan1),
                new Claim(ClaimTypes.Role ,user.VaiTro == 0 ? "admin" : "khachHang")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

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