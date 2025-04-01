using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using My_websiteAPI.ModelView;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinhThanhController : ControllerBase
    {
        private readonly MyDBcontext _context;
        public TinhThanhController(MyDBcontext context) 
        {
            _context = context;        
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvince()
        {
            var tinhthanhs =await _context.TinhThanh.Select(p=>new TinhThanhDTO {
                IdTinh=p.TinhThanhId,
                TenTinh=p.TenTinh
            }).ToListAsync();
            if (!tinhthanhs.Any())
            {
                return Ok(new { message = "Không tìm thấy tỉnh thành nào phù hợp!" });
            }


            return Ok(tinhthanhs);
        }
        [HttpGet("countProvince")]
        public async Task<IActionResult> countProvince()
        {
            var numbertt = await _context.TinhThanh.CountAsync();
          return Ok(new { sotinh = numbertt });

        }
        [HttpGet("search")]
        public async Task<IActionResult> GetProvince(string search="")
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest(new { message = "Vui lòng điền đầy đủ thông tin!" });
            }
            var tinhthanh =await _context.TinhThanh.Where(p=>p.TenTinh.Contains(search)).Select(p => new TinhThanhDTO
            {
                IdTinh = p.TinhThanhId,
                TenTinh = p.TenTinh
            }).ToListAsync();
            if (!tinhthanh.Any())
            {
                return BadRequest(new { message = "Không tìm thấy tỉnh thành nào phù hợp!" });
            }
            return Ok(tinhthanh);
        }






        [HttpPost("Create")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> CreateProvince (TinhThanhDTO model)
        {

            if (string.IsNullOrWhiteSpace(model.TenTinh))
            {
                return BadRequest(new {message="Vui lòng không bỏ trống tên tỉnh!"});
            }
            else
            {
                bool result = await _context.TinhThanh.AnyAsync(p => p.TenTinh == model.TenTinh && p.TinhThanhId != model.IdTinh);
                if (result == true)
                {
                    return Ok(new { message = "Tỉnh thành này đã có trong hệ thống rồi!" });
                }
                var province = new TinhThanh
                {
                    TenTinh = model.TenTinh

                };
                await _context.AddAsync(province);
             await  _context.SaveChangesAsync();
                return Ok(new {message="Thêm tỉnh thành thành công!"});

            }

        }
        [HttpPut("Edit")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> EditProvince(int id,TinhThanhDTO model)
        {
            var tinhthanh =await _context.TinhThanh.FirstOrDefaultAsync(p=>p.TinhThanhId==id);
            if (tinhthanh == null)
            {
                return NotFound(new { message = "Không tìm thấy tỉnh thành!" });
            }
            if (string.IsNullOrWhiteSpace(model.TenTinh))
            {
                return BadRequest(new { message = "Vui lòng không bỏ trống tên tỉnh!" });
            }
                
            bool result =await _context.TinhThanh.AnyAsync(p => p.TenTinh == model.TenTinh && p.TinhThanhId !=id);
            if (result == true)
            {
                return Ok(new { message = "Tỉnh thành này đã có trong hệ thống rồi!" });
            }
                    tinhthanh.TenTinh = model.TenTinh;

                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Sửa tỉnh thành thành công!" });
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            var tinhthanh = await _context.TinhThanh.FirstOrDefaultAsync(p => p.TinhThanhId == id);
            if (tinhthanh == null)
            {
                return NotFound(new { message = "Không tìm thấy tỉnh thành!" });
            }
            _context.Remove(tinhthanh);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xoá tỉnh thành thành công!" });
        }




    }
}
