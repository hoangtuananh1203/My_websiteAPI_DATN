using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using My_websiteAPI.ModelView;
using System.Net;
using System.Security.Claims;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhgiaController : ControllerBase
    {
        private readonly MyDBcontext _context;
        private static int Page_SIZE { get; set; } = 15;

        public DanhgiaController(MyDBcontext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize(Roles = Phanquyen.Admin)]

        public async Task<IActionResult> GetAll(int page=1)
        {
            var dt = _context.Danhgias.Include(p=>p.Diadiem).AsQueryable();
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { message = "Không tìm thấy đóng góp nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            var list = await dt.Select(p => new DanhGiaMV
            {
                DanhgiaId= p.DanhgiaId,
                Diem =p.Diem ,
                Noidung =p.Noidung ,
                Ngay_add=p.Ngay_add,
                Diadiem=p.Diadiem.Tieude,
               Iddiadiem=p.DiadiemId,
                Nameuser=p.User.UserName,
                UserId=p.UserId

            }).ToListAsync();

            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });


        }
        [HttpGet("DanhgiaDiaDiem")]
        public async Task<IActionResult> DanhgiaDiaDiem(int id ,int page = 1)
        {
            if (id < 0)
            {
                return BadRequest(new {message="Vui lòng nhập id địa điểm!"});
            }
            
            var dt = _context.Danhgias.Include(p => p.Diadiem).AsQueryable();
            dt = dt.Where(p => p.DiadiemId == id);
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { message = "Sản phẩm hiện không có đánh giá!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);

            var list = await dt.Select(p => new DanhGiaMV
            {
                DanhgiaId = p.DanhgiaId,
                Diem = p.Diem,
                Noidung = p.Noidung,
                Ngay_add = p.Ngay_add,
                Diadiem = p.Diadiem.Tieude,
                Iddiadiem = p.DiadiemId,
                Nameuser = p.User.UserName,
                

            }).ToListAsync();

            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });


        }

        [HttpGet("Danhgiathongke")]
        [Authorize(Roles = Phanquyen.Admin)]
        public async Task<IActionResult> Danhgiathongke( int page = 1)
        {
            var dt = _context.Danhgias.Include(p => p.Diadiem).AsQueryable();
            dt = dt.Where(p => p.Diem<=2);
            var totalItems = await dt.CountAsync();
            var danhGiaCao = _context.Danhgias.Count(p => p.Diem >= 3);
            var danhGiathap = _context.Danhgias.Count(p => p.Diem <=2);
            if (totalItems == 0)
            {
                return NotFound(new { message = "Sản phẩm hiện không có đánh giá!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);

            var list = await dt.Select(p => new DanhGiaMV
            {
                DanhgiaId = p.DanhgiaId,
                Diem = p.Diem,
                Noidung = p.Noidung,
                Ngay_add = p.Ngay_add,
                Diadiem = p.Diadiem.Tieude,
                Iddiadiem = p.DiadiemId,
                Nameuser = p.User.UserName,
                UserId = p.UserId

            }).ToListAsync();

            return Ok(new
            {
                items = list,
                totalPages = totalPages,
                danhGiaCao= danhGiaCao,
                danhGiathap= danhGiathap
            });


        }
        [HttpPost]
        [Authorize(Roles = Phanquyen.Custommer)]
        public async Task<IActionResult> Create(DanhGiaDTO model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return Unauthorized(new { message = "Vui lòng đăng nhập để đánh giá!" });
            }
            var check =await _context.Danhgias.FirstOrDefaultAsync(p => p.UserId == user &&p.DiadiemId==model.DiadiemId);
            if (check != null)
            {
                return BadRequest(new { message = "Bạn đã đánh giá địa điểm này rồi!" });

            }
            if (model.Diem<=0 || string.IsNullOrWhiteSpace(model.Noidung)) {
                return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin!" });
            }
            var danhgia = new Danhgia
            {
                Diem = model.Diem,
                Noidung = model.Noidung,
                Ngay_add  = DateTime.Now.Date,
                DiadiemId= model.DiadiemId,
                UserId = user,
            };
            await _context.AddAsync(danhgia);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đánh giá thành công!" });
        }

       [HttpDelete]
        [Authorize(Roles =Phanquyen.Admin)]
       public async Task<IActionResult> DeleteDanhgia(int id)
        {
            var checkdanhgia = await _context.Danhgias.FirstOrDefaultAsync(p => p.DanhgiaId == id);
            if (checkdanhgia == null)
            {
                return BadRequest(new { messase = "Không tìm thấy đánh giá!" });
            }
            _context.Danhgias.Remove(checkdanhgia);
            await _context.SaveChangesAsync();
            return Ok(new {message="Xoá đánh giá thành công"});

        }










    }
}
