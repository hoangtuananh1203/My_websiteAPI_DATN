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
    public class LoaiHinhController : ControllerBase
    {
        private readonly MyDBcontext _context;
        public LoaiHinhController(MyDBcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoaiHinh()
        {
            var loaihinh = await _context.LoaiHinh.Select(p => new LoaiHinhDTO
            {
                LoaiHinhId = p.LoaiHinhId,
                TenLoai = p.TenLoai
            }).ToListAsync();

            if (!loaihinh.Any())
            {
                return Ok(new { message = "Không tìm thấy loại hình nào phù hợp!" });
            }


            return Ok(loaihinh);
        }
        [HttpGet("GetallLH")]
        public async Task<IActionResult> GetAllLoaiHinh2()
        {
            var loaihinh = await _context.LoaiHinh.Select(p => new LoaiHinhDTO
            {
                LoaiHinhId = p.LoaiHinhId,
                TenLoai = p.TenLoai
            }).ToListAsync();
            if (!loaihinh.Any())
            {
                return Ok(new { message = "Không tìm thấy loại hình nào phù hợp!" });
            }

            var listlh = new List<LoaiHinhMV>();
           
            foreach (var items in loaihinh)
            {
             var countlh = await _context.Diadiem.CountAsync(p=>p.LoaiHinhId== items.LoaiHinhId);

                var item = new LoaiHinhMV
                {
                    LoaiHinhId = items.LoaiHinhId,
                    TenLoai = items.TenLoai,
                    sodiadiem = countlh,
                };
                listlh.Add(item);
            }

           

            return Ok(listlh);

        }
        [HttpGet("countLoaiHinh")]
        public async Task<IActionResult> countLoaiHinh()
        {
            var numbertt = await _context.LoaiHinh.CountAsync();
            return Ok(new { soloaihinh = numbertt });

        }
        [HttpGet("search")]
        public async Task<IActionResult> GetLoaiHinh(string search="")
        {
            if (string.IsNullOrEmpty(search)) 
            {
                return BadRequest(new { message = "Vui lòng điền đầy đủ thông tin!" });
            }

            var loaihinh = await _context.LoaiHinh.Where(p => p.TenLoai.Contains(search)).Select(p => new LoaiHinhDTO   
            {
                LoaiHinhId = p.LoaiHinhId,
                TenLoai = p.TenLoai
            }).ToListAsync();
            if (!loaihinh.Any())
            {
                return NotFound(new { message = "Không tìm thấy loại hình nào phù hợp!" });
            }
            return Ok(loaihinh);
        }
        [HttpPost("Create")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> CreateLoaiHinh(LoaiHinhDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.TenLoai))
            {
                return BadRequest(new { message = "Vui lòng điền đẩy đủ thông tin!" });
            }
            else
            {
                bool result = await _context.LoaiHinh.AnyAsync(p => p.TenLoai == model.TenLoai && p.LoaiHinhId != model.LoaiHinhId);
                if (result == true)
                {
                    return BadRequest(new { message = "Loại hình này đã có trong hệ thống rồi!" });
                }
                var loaihinh = new LoaiHinh
                {
                    TenLoai = model.TenLoai

                };
                await _context.AddAsync(loaihinh);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm loại hình thành công!" });

            }

        }
        [HttpPut("Edit")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> EditLoaiHinh(int id, LoaiHinhDTO model)
        {
           
            try
            {
                var loaihinh = await _context.LoaiHinh.FirstOrDefaultAsync(p => p.LoaiHinhId == id);
                if (loaihinh == null)
                {
                    return NotFound(new { message = "Không tìm thấy loại hình!" });
                }
                if (string.IsNullOrWhiteSpace(model.TenLoai))
                {
                    return BadRequest(new { message = "Vui lòng điền đầy đủ thông tin!" });
                }

                bool result = await _context.LoaiHinh.AnyAsync(p => p.TenLoai == model.TenLoai && p.LoaiHinhId != id);
                if (result == true)
                {
                    return BadRequest(new { message = "Loại hình này đã có trong hệ thống rồi!" });
                }
                loaihinh.TenLoai = model.TenLoai;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Sửa loại hình thành công!" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Lỗi!", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var loaihinh = await _context.LoaiHinh.FirstOrDefaultAsync(p => p.LoaiHinhId == id);
                if (loaihinh == null)
                {
                    return NotFound(new { message = "Không tìm thấy loại hình!" });
                }
                _context.Remove(loaihinh);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Xoá loại hình thành công!" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Lỗi!", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }

        }
    }
}
