using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using My_websiteAPI.ModelView;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DongGopController : ControllerBase
    {
        private readonly MyDBcontext _context;
        private static int Page_SIZE { get; set; } = 15;
        public DongGopController(MyDBcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page=1)
        {
            var dt =  _context.Donggop.AsQueryable();
            var totalItems =await dt.CountAsync();
            if (totalItems==0)
            {
                return Ok(new { message = "Không tìm thấy đóng góp nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
           
            var list =await dt.Select(p=>new DonggopDTO
                {
                    DonggopId = p.DonggopId,
                    Tieude = p.Tieude,
                    Noidung = p.Noidung,
                    Name = p.Name,
                    Email = p.Email,
                    SDT = p.SDT,
                    Date = p.Date,
                    trangthai = p.trangthai,

                }).ToListAsync();
                
            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var dt = await _context.Donggop.CountAsync();

                return Ok(new { sodonggop = dt });
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
        public async Task<IActionResult> SearchDonggop(string? search = "",int page=1, int? trangthai=null)
        {
          
            try
            {
                var dt = _context.Donggop.AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    dt = dt.Where(p => p.Tieude.Contains(search));
                }
                if (trangthai.HasValue)
                {
                    dt = dt.Where(p => p.trangthai == trangthai);

                }

                var totalItems = await dt.CountAsync();
                if (totalItems == 0)
                {
                    return NotFound(new {mesage="Không tìm thấy đóng góp nào!"});
                }
                var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
                dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


                var list = await dt.Select(p => new DonggopDTO
                {
                    DonggopId = p.DonggopId,
                    Tieude = p.Tieude,
                    Noidung = p.Noidung,
                    Name = p.Name,
                    Email = p.Email,
                    SDT = p.SDT,
                    Date = p.Date,
                    trangthai = p.trangthai,

                }).ToListAsync();

                return Ok(new
                {
                    items = list,
                    totalPages = totalPages
                });
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
      
        public async Task<IActionResult> Create(DonggopDTO model)
        {
            if (string.IsNullOrEmpty(model.Tieude) || string.IsNullOrEmpty(model.Name)|| string.IsNullOrEmpty(model.Noidung))
            {
                return BadRequest(new { message = "Vui lòng điền đầy đủ thông tin!" });
            }

            try
            {
               
                var newdt = new Donggop
                {
                   
                    Tieude = model.Tieude,
                    Noidung = model.Noidung,
                    Name = model.Name,
                    Email = model.Email,
                    SDT = model.SDT,
                    Date =  DateTime.Now,
                   trangthai = TrangthaiDonggop.chuaxem,
                };
                await _context.Donggop.AddAsync(newdt);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm đóng góp thành công!" });
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
        public async Task<IActionResult> Edit(int id)
        {
            var searchchdt = await _context.Donggop.FirstOrDefaultAsync(p => p.DonggopId == id);
            try
            {
                if (searchchdt == null)
                {
                    return NotFound(new { message = "Không tìm thấy đối tượng!" });
                }
                

               

                searchchdt.trangthai = TrangthaiDonggop.daxem;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Sửa cập nhật trạng thái đóng góp thành công!" });
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
            try
            {
                var donggop = await _context.Donggop.FindAsync(id);
                if (donggop == null)
                {
                    return NotFound(new { message = "Không tìm thấy đóng góp!" });
                }
                _context.Donggop.Remove(donggop);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Xoá đóng góp thành công!" });

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
