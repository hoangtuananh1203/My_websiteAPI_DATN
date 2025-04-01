using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerthreeController : ControllerBase
    {
        private readonly MyDBcontext _context;
        public BannerthreeController(MyDBcontext db)
        {
            _context = db;
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var banner = await _context.BannerThrees.ToListAsync();
            return Ok(new
            {
                items = banner
            }
                );
        }
        [HttpPost]
        [Authorize(Roles =Phanquyen.Admin)]
        public async Task<IActionResult> Create(BannerThree model)
        {
            var banner = await _context.BannerThrees.ToListAsync();
            if (banner != null)
            {
                 _context.BannerThrees.RemoveRange(banner);

                var bannerne = new BannerThree
                {
                    Image1 = model.Image1,
                    Image2 = model.Image2,
                    Image3 = model.Image3,
                };
              await  _context.BannerThrees.AddAsync(bannerne);
                await _context.SaveChangesAsync();
                return Ok(new {message="Thêm thành công!"});
            }
            var bannerne1 = new BannerThree
            {
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3,
            };
            await _context.BannerThrees.AddAsync(bannerne1);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Thêm thành công!" });

        }

    }
}
