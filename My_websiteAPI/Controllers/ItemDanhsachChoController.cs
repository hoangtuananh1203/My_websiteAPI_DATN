using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using My_websiteAPI.ModelView;
using System.Security.Claims;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDanhsachChoController : ControllerBase
    {
        private readonly MyDBcontext _context;
        public ItemDanhsachChoController(MyDBcontext context)
        {
            _context = context;
        }

        public static string GetLoaiSuKienName(int loai)
        {
            if (loai == 1)
            {
                return "Du lịch";
            }
            if (loai == 2)
            {
                return "Ẩm thực";
            }
            return "Khác";
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return Unauthorized(new { message = "Vui lòng đăng nhập!" });
            }
            var danhsachcho = await _context.ListAwait.FirstOrDefaultAsync(p=>p.UserId==user);
            if (danhsachcho == null)
            {
                return Ok(new {message="Không tìm thấy dịa điểm nào!"});
            }

            var danhsach = await _context.itemListAwait.Where(p=>p.ListAwaitId==danhsachcho.ListAwaitId).ToListAsync();
             var listDD= new List<DiadiemdanhsachchoMV>();
            var demsoitem = 0;
            var diadiems =  _context.Diadiem.Include(p=>p.TinhThanh).Include(p=>p.LoaiHinhDL).Include(p=>p.Danhcho).ToList(); 
            foreach(var item in danhsach)
              {
                var diadiem = diadiems.FirstOrDefault(p=>p.DiadiemId==item.DiadiemId);
                if (diadiem!=null)
                {
                   var itemdiadiem=new DiadiemdanhsachchoMV
                   {
                        DiadiemId = diadiem.DiadiemId,
                        Tieude = diadiem.Tieude,
                        Motangan = diadiem.Motangan,
                        Diachi = diadiem.Diachi,
                        DateOC = diadiem.DateOC,
                        Email = diadiem.Email,
                        SDT = diadiem.SDT,
                        Gia = diadiem.Gia,
                        Tinhtrang = diadiem.Tinhtrang,
                        Noidung = diadiem.Noidung,
                        TinhThanh = diadiem.TinhThanh.TenTinh,
                        LoaiHinh = diadiem.LoaiHinhDL.TenLoai,
                        Danhcho = diadiem.Danhcho.Doituong,
                        Luotxem = diadiem.Luotxem,
                        Loaisukien = GetLoaiSuKienName(diadiem.Loaisukien),
                        Imagemain = diadiem.Imagemain,
                        Image1 = diadiem.Image1,
                        Image2 = diadiem.Image2,
                        Image3 = diadiem.Image3,
                        Image4 = diadiem.Image4,
                        Image5 = diadiem.Image5,
                        itemListAwaitId=item.itemListAwaitId,
                        DateAdd= item.Date.Date,
                        ListAwaitId= item.ListAwaitId,
                    };
                    listDD.Add(itemdiadiem);
                    demsoitem++;
                }
                

            }

            return Ok(new
            {
              soitem= demsoitem,
                itemdiadiem =listDD

            });
                
            
        }

        [HttpPost]
        [Authorize(Roles =Phanquyen.Custommer)]
        public async Task<IActionResult> Create (itemDanhsachChoDTO model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user==null)
            {
                return BadRequest(new { message = "Vui lòng đăng nhập!" });
            }
            var checklist = await _context.ListAwait.Include(p=>p.itemListAwaits).FirstOrDefaultAsync(p => p.UserId == user);
            if (checklist == null) 
            {
                // CHƯA CÓ DANH SÁCH
                var danhsachcho = new ListAwait
                {
                    UserId = user,
                };
                await _context.ListAwait.AddAsync(danhsachcho);
                await _context.SaveChangesAsync();

                var itemdanhsach = new itemListAwait
                {
                    ListAwaitId = danhsachcho.ListAwaitId,
                    Date = DateTime.Now.Date,
                    DiadiemId = model.DiadiemId,
                };
                await _context.AddAsync(itemdanhsach);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm thành công địa điểm vào danh sách chờ!" });
            }
            else
            {
                var checksame =await _context.itemListAwait.FirstOrDefaultAsync(p => p.DiadiemId == model.DiadiemId && p.ListAwaitId== checklist.ListAwaitId);
                if (checksame != null) { return Ok(new { message = "Địa điểm đã tồn tại trong danh sách!" }); }
                // có danh sách ròi
                var itemdanhsach = new itemListAwait
                {
                    ListAwaitId = checklist.ListAwaitId,
                    Date = DateTime.Now.Date,
                    DiadiemId = model.DiadiemId,
                };
                await _context.AddAsync(itemdanhsach);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm thành công địa điểm vào danh sách chờ!" });

            }


        }

        [HttpDelete]
        [Authorize(Roles = Phanquyen.Custommer)]

        public async Task<IActionResult> DeleteItem(int id)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return BadRequest(new { message = "Vui Lòng đăng nhập trước" });
            }
            var list = await _context.ListAwait.FirstOrDefaultAsync(p=>p.UserId==user);
            if (list == null)
            {
                return BadRequest(new { message = "Bạn chưa có danh sách!" });

            }
            var item = await _context.itemListAwait.FirstOrDefaultAsync(p => p.ListAwaitId == list.ListAwaitId && p.itemListAwaitId == id);
            if (item == null)
            {
                return BadRequest(new { message = "Không tìm thấy địa điểm" });

            }
            _context.itemListAwait.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(new {message="Xoá địa điểm thành công"});


        }
    }
}
