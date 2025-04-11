using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using My_websiteAPI.ModelView;
using System.Globalization;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiadiemController : ControllerBase
    {
        private readonly MyDBcontext _context;
        private static int Page_SIZE { get; set; } = 15;

        public DiadiemController(MyDBcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page=1)
        {
            var dt =  _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return Ok(new { mesage = "Không tìm thấy địa điểm nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


            var list = await dt.Select(p => new DiadiemMV1
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,
                DanhchoId = p.DanhchoId,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                LoaisukienId = p.Loaisukien,


            }).ToListAsync();

            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }

        [HttpGet("thinhhanh")]
        public async Task<IActionResult> GetfiveIndex()
        {
            var dt2 = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).Take(2).OrderByDescending(p=>p.Luotxem);
            var dt3 =  _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).Take(3).OrderByDescending(p=>p.Luotxem);

         

            var list2 = await dt2.Select(p => new DiadiemMV1
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,
                DanhchoId = p.DanhchoId,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                LoaisukienId = p.Loaisukien,


            }).ToListAsync();
            var list3 = await dt3.Select(p => new DiadiemMV1
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,
                DanhchoId = p.DanhchoId,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                LoaisukienId = p.Loaisukien,


            }).ToListAsync();
        
            return Ok(new
            {
                items1 = list2,
                items2 = list3,
           

            });
        }
        [HttpGet("ddmoi")]
        public async Task<IActionResult> cacdiadiemmoi()
        {
            var dt2 = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).Where(p=>p.Loaisukien==LoaiDiadiem.Dulich).Take(4);
            var dt3 = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).Where(p => p.Loaisukien == LoaiDiadiem.Amthuc).Take(4);



            var list2 = await dt2.Select(p => new DiadiemMV1
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,
                DanhchoId = p.DanhchoId,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                LoaisukienId = p.Loaisukien,


            }).ToListAsync();
            var list3 = await dt3.Select(p => new DiadiemMV1
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,
                DanhchoId = p.DanhchoId,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                LoaisukienId = p.Loaisukien,


            }).ToListAsync();

            return Ok(new
            {
                items1 = list2,
                items2 = list3,


            });
        }





        [HttpGet("Find")]
        public async Task<IActionResult> Finbyid(int id)
        {
            var p = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).FirstOrDefault(p => p.DiadiemId == id);
            var diadem = new DiadiemMV1
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,
                DanhchoId = p.DanhchoId,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                LoaisukienId = p.Loaisukien,
            };
            if (p == null)
            {
                return Ok(new { mesage = "Không tìm thấy địa điểm!" });
            }
            return Ok(diadem);
        }
        [HttpGet("danhcho")]
        public async Task<IActionResult> GetAllDanhcho(int id,int page=1)
        {
            var dt = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            dt = dt.Where(p => p.DanhchoId == id);
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { mesage = "Không tìm thấy địa điểm nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


            var list = await dt.Select(p => new DiadiemMV
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,


            }).ToListAsync();

            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }
        [HttpGet("loaihinh")]
        public async Task<IActionResult> GetAllLoaiHinh(int id, int page = 1)
        {
            var dt = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            dt = dt.Where(p => p.LoaiHinhId == id);
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { mesage = "Không tìm thấy địa điểm nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


            var list = await dt.Select(p => new DiadiemMV
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,


            }).ToListAsync();

            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }

        [HttpGet("diadiemlienquan")]
        public async Task<IActionResult> diadiemlienquan(int id)
        {
            var dt = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            dt = dt.Where(p => p.LoaiHinhId == id).Take(4);
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { mesage = "Không tìm thấy địa điểm nào!" });
            }
           


            var list = await dt.Select(p => new DiadiemMV
            {
                DiadiemId = p.DiadiemId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Diachi = p.Diachi,
                DateOC = p.DateOC,
                Email = p.Email,
                SDT = p.SDT,
                Gia = p.Gia,
                Tinhtrang = p.Tinhtrang,
                Noidung = p.Noidung,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Luotxem = p.Luotxem,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                urlmap = p.urlmap,


            }).ToListAsync();

            return Ok(new
            {
                items = list,
                
            });
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
        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var dt = await _context.Diadiem.CountAsync();

                return Ok(new { diadiem = dt });
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
        [HttpGet("Soluoxem")]
        public async Task<IActionResult> Soluotxem()
        {
            try
            {
                var dt = await _context.Diadiem.SumAsync(p=>p.Luotxem);

                return Ok(new { luotxem = dt });
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
        [HttpGet("thongkesodd")]
        public async Task<IActionResult> thongkesodd()
        {
            try
            {
                var all = await _context.Diadiem.CountAsync();
                var dl = await _context.Diadiem.CountAsync(p => p.Loaisukien==LoaiDiadiem.Dulich);
                var at = await _context.Diadiem.CountAsync(p => p.Loaisukien==LoaiDiadiem.Amthuc);

                return Ok(new { 
                    all = all,
                    dulich = dl,
                    amthuc = at,


                });
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
        public async Task<IActionResult> SearchDiaDiem(string? search, int? tinhthanh=null, int? loaidiadiem = null, int? loaihinh = null, int? doituong = null, string? sortBy="", int page=1)
        {
           
            try
            {
             
                 var dt = _context.Diadiem.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
                #region loc theo
                // từ khoá 
                if (!string.IsNullOrEmpty(search))
                {
                    dt = dt.Where(p => p.Tieude != null && p.Tieude.ToLower().Contains(search.ToLower()));
                }
            
                //tinhthanh
                if (tinhthanh.HasValue && tinhthanh > 0)
                {
                    dt = dt.Where(p => p.TinhThanhId == tinhthanh);
                }
                if (loaihinh.HasValue && loaihinh > 0)
                {
                    dt = dt.Where(p => p.LoaiHinhId == loaihinh);
                }
                if (doituong.HasValue && doituong > 0)
                {
                    dt = dt.Where(p => p.DanhchoId == doituong);
                }
                //tinhthanh
                if (loaidiadiem.HasValue && loaidiadiem > 0)
                {
                    dt = dt.Where(p => p.Loaisukien == loaidiadiem); 
                }

                #endregion

                #region sắp xếp theo 
                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy)
                    {
                        case "tieude":
                            dt = dt.OrderBy(p => p.Tieude);
                            break;

                        case "luotxemtang":
                            dt = dt.OrderBy(p=>p.Luotxem);
                            break;
                        case "luotxemgiam":
                            dt = dt.OrderByDescending(p => p.Luotxem);
                            break;
                        default:
                            dt = dt.OrderBy(p => p.Tieude);
                            break;

                    }
                }
                #endregion

             
                var totalItems = await dt.CountAsync();
                if (totalItems == 0)
                {
                    return Ok(new { message = "Không tìm thấy địa điểm nào!" });
                }
                var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
                dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


                var list = await dt.Select(p => new DiadiemMV1
                {
                    DiadiemId = p.DiadiemId,
                    Tieude = p.Tieude,
                    Motangan = p.Motangan,
                    Diachi = p.Diachi,
                    DateOC = p.DateOC,
                    Email = p.Email,
                    SDT = p.SDT,
                    Gia = p.Gia,
                    Tinhtrang = p.Tinhtrang,
                    Noidung = p.Noidung,
                    TinhThanh = p.TinhThanh.TenTinh,
                    LoaiHinh = p.LoaiHinhDL.TenLoai,
                    Danhcho = p.Danhcho.Doituong,
                    Luotxem = p.Luotxem,
                    Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                    Imagemain = p.Imagemain,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    urlmap = p.urlmap,
                    DanhchoId = p.DanhchoId,
                    LoaiHinhId = p.LoaiHinhId,
                    TinhThanhId = p.TinhThanhId,
                    LoaisukienId=p.Loaisukien,


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
        [Authorize(Roles = Phanquyen.Admin)]

        public async Task<IActionResult> Create(DiadiemDTO p)
        {
            var requiredFields = new List<string> { p.Tieude, p.Noidung, p.Motangan, p.Diachi, p.Imagemain, p.Image1, p.Image2, p.Image3, p.Image4, p.Image5,p.Mocuadongcua,p.DateOC, };

           
            if (requiredFields.Any(string.IsNullOrEmpty))
            {
                return BadRequest(new { message = "Vui lòng nhập đầy đủ các trường có dấu *!" });
            }

            if (p.TinhThanhId == 0 || p.DanhchoId == 0 || p.LoaiHinhId == 0)
            {
                return BadRequest(new { message = "Vui lòng chọn đầy đủ các khoá ngoại!" });

            }

            try
            {

                var newdt = new Diadiem
                {
                   
                    Tieude = p.Tieude,
                    Motangan = p.Motangan,
                    Mocuadongcua=p.Mocuadongcua,
                    Diachi = p.Diachi,
                    DateOC = p.DateOC,
                    Email = p.Email,
                    SDT = p.SDT,
                    Gia = p.Gia,
                    Tinhtrang = p.Tinhtrang,
                    Noidung = p.Noidung,
                   TinhThanhId = p.TinhThanhId,
                   DanhchoId =p.DanhchoId,
                   LoaiHinhId = p.LoaiHinhId,
                    Luotxem = 1,
                    Loaisukien = p.Loaisukien,
                    Imagemain = p.Imagemain,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    urlmap = p.urlmap,
                };
                await _context.Diadiem.AddAsync(newdt);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm địa điểm thành công!" });
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
        public async Task<IActionResult> EditDiaDiem(int id, DiadiemDTO p)
        {

            try
            {
                var sk = await _context.Diadiem.FirstOrDefaultAsync(p => p.DiadiemId == id);
                if (sk == null)
                {
                    return NotFound(new { message = "Không tìm thấy địa điểm!" });
                }


                    sk.Tieude = p.Tieude;
                    sk.Motangan = p.Motangan;
                    sk.Diachi = p.Diachi;
                    sk.DateOC = p.DateOC;
                    sk.Email = p.Email;
                    sk.SDT = p.SDT;
                    sk.Gia = p.Gia;
                    sk.Tinhtrang = true;
                    sk.Noidung = p.Noidung;
                    sk.TinhThanhId = p.TinhThanhId;
                    sk.DanhchoId = p.DanhchoId;
                    sk.LoaiHinhId = p.LoaiHinhId;
                    sk.Luotxem = 0;
                    sk.Loaisukien = p.Loaisukien;
                    sk.Imagemain = p.Imagemain;
                    sk.Image1 = p.Image1;
                    sk.Image2 = p.Image2;
                    sk.Image3 = p.Image3;
                    sk.Image4 = p.Image4;
                    sk.Image5 = p.Image5;
                    sk.urlmap = p.urlmap;


                await _context.SaveChangesAsync();
                return Ok(new { message = "Sửa địa điểm thành công!" });
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
        [HttpPut("Luotxem")]
        
        public async Task<IActionResult> Congluotxem(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "ID không hợp lệ!" });
            }

            try
            {
                var sk = await _context.Diadiem.FirstOrDefaultAsync(p => p.DiadiemId == id);
                if (sk == null)
                {
                    return NotFound(new { message = "Không tìm thấy địa điểm!" });
                }


                sk.Luotxem ++;
              


                await _context.SaveChangesAsync();
                return Ok(new { message = "Add count success" });
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
                var sk = await _context.Diadiem.FindAsync(id);
                if (sk == null)
                {
                    return NotFound(new { message = "Không tìm thấy địa điểm nào phù hợp!" });
                }
                _context.Diadiem.Remove(sk);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Xoá địa điểm thành công!" });

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
