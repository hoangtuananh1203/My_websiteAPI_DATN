using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using My_websiteAPI.ModelView;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemlichsuTraiNghiemController : ControllerBase
    {
        private readonly MyDBcontext _context;
        public ItemlichsuTraiNghiemController(MyDBcontext context)
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

            var danhsachcho = await _context.LichsuTrainghiem.FirstOrDefaultAsync(p => p.UserId == user);
            if (danhsachcho == null)
            {
                return NotFound(new { message = "Không tìm thấy dịa điểm nào!" });
            }

            var danhsachitem = await _context.itemLichsuTrainghiem.Where(p => p.LichsuTrainghiemId == danhsachcho.LichsuTrainghiemId).ToListAsync();
            var listDD = new List<DiadiemDatrainghiemMV>();
            var demsoitem = 0;
            var diadiems = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.LoaiHinhDL).Include(p => p.Danhcho).ToList();
            foreach (var item in danhsachitem)
            {
                var diadiem = diadiems.FirstOrDefault(p => p.DiadiemId == item.DiadiemId);
                if (diadiem != null)
                {
                    var itemdiadiem = new DiadiemDatrainghiemMV
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
                        itemTrainghiem = item.itemLichsuTrainghiemId,
                        DateAdd = item.Date.Date,
                        trainghiem = item.LichsuTrainghiemId,
                    };
                    listDD.Add(itemdiadiem);
                    demsoitem++;
                }


            }

            return Ok(new
            {
                soitem = demsoitem,
                itemdiadiem = listDD

            });


        }
        [HttpGet("timdiadiem")]
        public async Task<IActionResult> Searchdd(string keyword)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return Unauthorized(new { message = "Vui lòng đăng nhập!" });
            }

            var danhsachcho = await _context.LichsuTrainghiem.FirstOrDefaultAsync(p => p.UserId == user);
            if (danhsachcho == null)
            {
                return NotFound(new { message = "Không tìm thấy dịa điểm nào!" });
            }

            var danhsachitem = await _context.itemLichsuTrainghiem.Where(p => p.LichsuTrainghiemId == danhsachcho.LichsuTrainghiemId).ToListAsync();
            var listDD = new List<DiadiemDatrainghiemMV>();
            var demsoitem = 0;
            var diadiems = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.LoaiHinhDL).Include(p => p.Danhcho).Where(p=>p.Tieude.ToLower().Contains(keyword.ToLower())).ToList();
            foreach (var item in danhsachitem)
            {
                var diadiem = diadiems.FirstOrDefault(p => p.DiadiemId == item.DiadiemId);
                if (diadiem != null)
                {
                    var itemdiadiem = new DiadiemDatrainghiemMV
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
                        itemTrainghiem = item.itemLichsuTrainghiemId,
                        DateAdd = item.Date.Date,
                        trainghiem = item.LichsuTrainghiemId,
                    };
                    listDD.Add(itemdiadiem);
                    demsoitem++;
                }


            }

            return Ok(new
            {
                soitem = demsoitem,
                itemdiadiem = listDD

            });


        }
        [HttpPost]
        [Authorize(Roles = Phanquyen.Custommer)]
        public async Task<IActionResult> Create(ItemLichSuTraiNghiemDTO model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return BadRequest(new { message = "Vui lòng đăng nhập!" });
            }
            var checklist = await _context.LichsuTrainghiem.Include(p => p.itemLichsuTrainghiems).FirstOrDefaultAsync(p => p.UserId == user);
            if (checklist == null)
            {
                // CHƯA CÓ DANH SÁCH
                var danhsachcho = new LichsuTrainghiem
                {
                    UserId = user,
                };
                await _context.LichsuTrainghiem.AddAsync(danhsachcho);
                await _context.SaveChangesAsync();

                var itemdanhsach = new itemLichsuTrainghiem
                {
                    LichsuTrainghiemId = danhsachcho.LichsuTrainghiemId,
                    Date = DateTime.Now.Date,
                    DiadiemId = model.DiadiemId,
                };
                await _context.AddAsync(itemdanhsach);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Đánh dấu địa điểm đã trải nghiệm thành công!" });
            }
            else
            {
                var checksame = await _context.itemLichsuTrainghiem.FirstOrDefaultAsync(p => p.DiadiemId == model.DiadiemId && p.LichsuTrainghiemId == checklist.LichsuTrainghiemId);
                if (checksame != null) { return Ok(new { message = "Địa điểm đã tồn tại trong danh sách đã trải nghiệm!" }); }
                // có danh sách ròi
                var itemdanhsach = new itemLichsuTrainghiem
                {
                    LichsuTrainghiemId = checklist.LichsuTrainghiemId,
                    Date = DateTime.Now.Date,
                    DiadiemId = model.DiadiemId,
                };
                await _context.AddAsync(itemdanhsach);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Đánh dấu địa điểm đã trải nghiệm thành công!" });


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
            var list = await _context.LichsuTrainghiem.FirstOrDefaultAsync(p => p.UserId == user);
            if (list == null)
            {
                return BadRequest(new { message = "Bạn chưa có danh sách!" });

            }
            var item = await _context.itemLichsuTrainghiem.FirstOrDefaultAsync(p => p.LichsuTrainghiemId == list.LichsuTrainghiemId && p.itemLichsuTrainghiemId == id);
            if (item == null)
            {
                return BadRequest(new { message = "Không tìm thấy địa điểm" });

            }
            _context.itemLichsuTrainghiem.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xoá địa điểm thành công" });


        }
    }
}
