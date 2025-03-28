using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyDBcontext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController
            (
            MyDBcontext context,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }
        [HttpPost("Dangnhap")]
        public async Task<IActionResult> Dangnhap(SignInModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Email đăng nhập không đúng vui lòng thử lại!" });
            }
            var checkpass = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkpass)
            {
                return Unauthorized(new { message = "Mật khẩu không chính xác!, vui lòng thử lại!" });
            }
            var thongtin = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("Username", user.UserName),
                    
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                thongtin.Add(new Claim(ClaimTypes.Role, role));
            }
            var khoa = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken
            (
              issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
                     claims: thongtin,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: new SigningCredentials(khoa, SecurityAlgorithms.HmacSha256)

                );
            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("Dangky")]
        public async Task<IActionResult> Dangky(SignUpModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email)|| string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest(new {message="Vui lòng nhập đầu đủ thông tin!"});
            }
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName= model.Username,
                FirstName= model.FirstName,
                LastName= model.LastName,
           
            };
             var result = await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                if(!await _roleManager.RoleExistsAsync(Phanquyen.Custommer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Phanquyen.Custommer));
                   
                }
                await _userManager.AddToRoleAsync(user, Phanquyen.Custommer);
                return Ok(new {message="Đăng ký tài khoản thành công!"});

            }
            else
            {
                return BadRequest(new {message="Đăng ký tài khoản không thành công, Vui lòng thử lại sau!" });
            }



        }

        [HttpPost("LogUpAdmin")]
        public async Task<IActionResult> LogUpAdmin(SignUpModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest(new { message = "Vui lòng điền đầy đủ thông tin đang ký!" });
            }
            var user = new ApplicationUser
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
               
                UserName = model.Username
            };

         var result =   await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Phanquyen.Admin)) // chuwa cos quyen thi tao
                {
                    await _roleManager.CreateAsync(new IdentityRole(Phanquyen.Admin));

                }
                await _userManager.AddToRoleAsync(user, Phanquyen.Admin);
                return Ok(new { message = "Đăng ký tài khoản Admin thành công!" });

            }
            else
            {
                return BadRequest(new { message = "Đăng ký tài khoản thất bại!" });
            }
        }

    }
}
