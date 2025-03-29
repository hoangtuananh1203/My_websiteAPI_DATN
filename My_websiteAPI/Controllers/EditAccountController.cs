using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.ModelView;
using System.Collections.Generic;
using System.Security.Claims;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditAccountController : ControllerBase
    {
        private readonly MyDBcontext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private static int Page_SIZE { get; set; } = 2;


        public EditAccountController
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
        [HttpDelete]
        [Authorize(Roles = $"{Phanquyen.Admin},{Phanquyen.Custommer}")]
        public async Task<IActionResult> DeleteAccount(string iduser)
        {
            
            var user =await _userManager.FindByIdAsync(iduser);
            if (user == null) 
                {
                return NotFound(new { message = "Người dùng không tồn tại!" });
            }
        var result=    await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Xóa tài khoản thất bại!", errors = result.Errors });
            }

            return Ok(new { message = "Xóa tài khoản thành công!" });

        }
        [HttpGet]
      
        public async Task<IActionResult> GetAllAccount(int page=1)
        {
           

            var users = _context.Users.AsQueryable();

            var totalItems = users.Count();
            if (totalItems == 0)
            {
                return NotFound(new { mesage = "Không tìm thấy tài khoản nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            users = users.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


            var list = await users.Select(p => new
            {
                Iduser = p.Id,
                p.UserName,
                p.Email,
               
            }).ToListAsync();

            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }
        [HttpGet("searchAccount")]
        [Authorize(Roles =Phanquyen.Admin)]
        public async Task<IActionResult> TimkiemAccount(string email)
        {

                var user =  _context.Users
            .Where(p => p.Email.ToLower().Contains(email.ToLower()))
            .Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email
            })
            .FirstOrDefault();

                if (user == null)
                {
                
                return Ok(new{   message = "Không tìm thấy tài khoản!",
                  

                });
                
                }

                return Ok(user);
        }
        [HttpPut("datlaimk")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> Datlaimk(string iduser)
        {

            var user = await _userManager.FindByIdAsync(iduser);
            if (user == null)
            {
                return NotFound(new { message = "Người dùng không tồn tại!" });
            }

            if (user == null)
            {

                return Ok(new{   message = "Không tìm thấy tài khoản!",  });

            }


            string newPassword = "Ictu123@";

          
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Lỗi khi đặt lại mật khẩu!", errors = result.Errors });
            }

            return Ok(new { message = "Đặt lại mật khẩu thành công! Mật khẩu mới là: Ictu123@" });
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangPassword(DoimatkhauMV model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new {message="Vui lòng đăng nhập trước"});
            }
            var user =await _userManager.FindByIdAsync(userId);
          
            if (user == null)
            {
                return NotFound(new { message = "Người dùng không tồn tại!" });
            }
           
            var checkpass = await _userManager.CheckPasswordAsync(user, model.oldPasss);
            if (!checkpass)
            {
                return Unauthorized(new { message = "Mật khẩu không chính xác!, vui lòng thử lại!" });
            }
        var result =    await _userManager.ChangePasswordAsync(user, model.oldPasss, model.newPasss);
            if (result.Succeeded)
            {
                return Ok(new {message="Đổi mật khẩu thành công!"});
            }
            return BadRequest(new { message = "Đổi mật khẩu thất bại!" });

        }














    }
}
