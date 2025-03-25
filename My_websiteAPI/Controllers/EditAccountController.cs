using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.ModelView;
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
      
        public async Task<IActionResult> GetAllAccount()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email
                })
                .ToListAsync();

            return Ok(users);
        }
        [HttpGet("searchAccount")]
        [Authorize(Roles =Phanquyen.Admin)]
        public async Task<IActionResult> TimkiemAccount(string email)
        {
                var user = await _context.Users
            .Where(p => p.Email == email)
            .Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email
            })
            .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound(new { message = "Không tìm thấy tài khoản!" });
                }

                return Ok(user);
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
                return Unauthorized(new { message = "Mật khẩu không chính xác!, vui lòng thửu lại!" });
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
