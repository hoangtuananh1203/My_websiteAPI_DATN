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
    public class DoiTuongController : ControllerBase
    {
        private readonly MyDBcontext _context;
        public DoiTuongController(MyDBcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dt = await _context.Danhcho.Select(p => new DanhchoDTO
            {
                DanhchoId = p.DanhchoId,
                Doituong = p.Doituong,

            }).ToListAsync();
            if (!dt.Any()) {
                return NotFound(new { message = "Không tìm thấy đối tượng nào!" });
            }
            return Ok(dt);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var dt = await _context.Danhcho.CountAsync();

                return Ok(new { sodoituong = dt });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Lỗi !", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchDoituong(string search = "")
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest(new { mesage = "Vui lòng nhập trường thông tin tìm kiếm!" });
            }
            try
            {
                var dt = await _context.Danhcho.Where(p => p.Doituong.Contains(search)).Select(p => new DanhchoDTO
                {
                    DanhchoId = p.DanhchoId,
                    Doituong = p.Doituong,

                }).ToListAsync();
                if (!dt.Any())
                {
                    return NotFound(new { message = "Không tìm thấy đối tượng nào!" });
                }
                return Ok(dt);
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


        [HttpPost("Create")]
        [Authorize(Roles =Phanquyen.Admin)]
        public async Task<IActionResult> Create(DanhchoDTO model)
        {
            if (string.IsNullOrEmpty(model.Doituong))
            {
                return BadRequest(new { message = "Vui lòng điền đầy đủ thông tin!" });
            }

            try
            {
                var doituong = await _context.Danhcho.FirstOrDefaultAsync(p => p.Doituong == model.Doituong);
                if (doituong != null)
                {
                    return BadRequest(new { messase = "Đối tượng đã tồn tại trong hệ thống!" });
                }
                var newdt = new Danhcho
                {
                    Doituong = model.Doituong,
                };
                await _context.AddAsync(newdt);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm đối tượng thành công!" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Lỗi thêm dữ liệu!", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
        [HttpPut("Edit")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> Edit(int id,DanhchoDTO model)
        {
            var searchchdt =await _context.Danhcho.FirstOrDefaultAsync(p => p.DanhchoId == id);
            try
            {
                if (searchchdt == null)
                {
                    return NotFound(new { message = "Không tìm thấy đối tượng!" });
                }
                if (string.IsNullOrEmpty(model.Doituong))
                {
                    return BadRequest(new { message = "Vui lòng điền đầy đủ thông tin!" });
                }

                var doituong = await _context.Danhcho.FirstOrDefaultAsync(p => p.Doituong == model.Doituong && p.DanhchoId != id);
                if (doituong != null)
                {
                    return BadRequest(new { messase = "Đối tượng đã tồn tại trong hệ thống!" });
                }

                searchchdt.Doituong = model.Doituong;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Sửa đối tượng thành công!" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Lỗi cập nhật dữ liệu!", error = ex.Message });
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
            try {
                var searchchdt = await _context.Danhcho.FindAsync(id);
                if (searchchdt == null)
                {
                    return NotFound(new { message = "Không tìm thấy đối tượng!" });
                }
                _context.Danhcho.Remove(searchchdt);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Xoá đối tượng thành công!" });

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
