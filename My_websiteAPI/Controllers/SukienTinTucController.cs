using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using My_websiteAPI.Model;
using My_websiteAPI.ModelView;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SukienTinTucController : ControllerBase
    {
        private readonly MyDBcontext _context;
        private static int Page_SIZE { get; set; } = 15;
        public SukienTinTucController(MyDBcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page =1)
        {
            var dt = _context.SukienTintuc.Include(p=>p.TinhThanh).Include(p=>p.Danhcho).Include(p=>p.LoaiHinhDL).AsQueryable();
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return Ok(new { message = "Không tìm thấy sự kiện -  tin tức nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            var list =await dt.Select(p=> new SuKienTintucMV1
                {
                    SukienId = p.SukienId,
                    Tieude = p.Tieude,
                    Motangan = p.Motangan,
                    Mota1 = p.Mota1,
                    Diachi = p.Diachi,
                    SDT = p.SDT,
                    DateOpen = p.DateOpen,
                    DateClose = p.DateClose,
                    TinhThanh = p.TinhThanh.TenTinh,
                    LoaiHinh = p.LoaiHinhDL.TenLoai,
                    Danhcho = p.Danhcho.Doituong,
                    Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                    Imagemain = p.Imagemain,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    Gia = p.Gia,
                    LoaiHinhId = p.LoaiHinhId,
                    TinhThanhId=p.TinhThanhId,
                    DanhchoId= p.DanhchoId,
                    LoaisukienId=p.LoaiHinhId

                }).ToListAsync();
            


            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }
        [HttpGet("Find")]
        public async Task<IActionResult> Finbyid(int id)
        {
            var p = _context.SukienTintuc.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).FirstOrDefault(p => p.SukienId == id);
            var diadem = new SuKienTintucMV1
                {
                SukienId = p.SukienId,
                    Tieude = p.Tieude,
                    Motangan = p.Motangan,
                    Mota1 = p.Mota1,
                    Diachi = p.Diachi,
                    SDT = p.SDT,
                    DateOpen = p.DateOpen,
                    DateClose = p.DateClose,
                    TinhThanh = p.TinhThanh.TenTinh,
                    LoaiHinh = p.LoaiHinhDL.TenLoai,
                    Danhcho = p.Danhcho.Doituong,
                    Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                    Imagemain = p.Imagemain,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    Gia = p.Gia,
                    LoaiHinhId = p.LoaiHinhId,
                    TinhThanhId = p.TinhThanhId,
                    DanhchoId = p.DanhchoId,
                    LoaisukienId = p.LoaiHinhId

                };
            if (p == null)
            {
                return Ok(new { mesage = "Không tìm thấy sự kiện!" });
            }
            return Ok(diadem);
        }
        [HttpGet("loaihinh")]
        public async Task<IActionResult> GetAlltheoLH(int idloaisukien,int page = 1)
        {
            
            var dt = _context.SukienTintuc.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            if(idloaisukien>=1 && idloaisukien<=3)
            {
                dt = dt.Where(p => p.Loaisukien == idloaisukien);
            }
            if(idloaisukien==0)
            {
                dt = dt;

            }


            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return Ok(new { message = "Không tìm thấy sự kiện -  tin tức nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            var list = await dt.Select(p => new SuKienTintucMV1
            {
                SukienId = p.SukienId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Mota1 = p.Mota1,
                Diachi = p.Diachi,
                SDT = p.SDT,
                DateOpen = p.DateOpen,
                DateClose = p.DateClose,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                Gia = p.Gia,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                DanhchoId = p.DanhchoId,
                LoaisukienId = p.LoaiHinhId

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
            var dt = _context.SukienTintuc.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            dt = dt.Where(p => p.LoaiHinhId == id).Take(4);
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { message = "Không tìm thấy sự kiện nào!" });
            }



            var list = await dt.Select(p => new SuKienTintucMV1
            {
                SukienId = p.SukienId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Mota1 = p.Mota1,
                Diachi = p.Diachi,
                SDT = p.SDT,
                DateOpen = p.DateOpen,
                DateClose = p.DateClose,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                Gia = p.Gia,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                DanhchoId = p.DanhchoId
               

            }).ToListAsync();


            return Ok(new
            {
                items = list,

            });
        }


        [HttpGet("FillterLoaidd")]
        public async Task<IActionResult> GetLoaiHinh(int loai,int page=1)
        {
            var dt = _context.SukienTintuc.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            dt = dt.Where(p => p.LoaiHinhId == loai);
            
            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { message = "Không tìm thấy sự kiện -  tin tức nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            var list = await dt.Select(p => new SuKienTintucMV1
            {
                SukienId = p.SukienId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Mota1 = p.Mota1,
                Diachi = p.Diachi,
                SDT = p.SDT,
                DateOpen = p.DateOpen,
                DateClose = p.DateClose,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                Gia = p.Gia,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                DanhchoId = p.DanhchoId,
                LoaisukienId = p.LoaiHinhId

            }).ToListAsync();



            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }
        [HttpGet("Fillterttdate")]
        public async Task<IActionResult> GetTinTucDate(int page=1)
        {
            DateTime today = DateTime.Now;
          
            var dt = _context.SukienTintuc.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();
            dt = dt.Where(p => p.DateOpen >= today).OrderBy(p => p.DateOpen);

            var totalItems = await dt.CountAsync();
            if (totalItems == 0)
            {
                return NotFound(new { message = "Không tìm thấy sự kiện -  tin tức nào!" });
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
            dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            var list = await dt.Select(p => new SuKienTintucMV1
            {
                SukienId = p.SukienId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Mota1 = p.Mota1,
                Diachi = p.Diachi,
                SDT = p.SDT,
                DateOpen = p.DateOpen,
                DateClose = p.DateClose,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                Gia = p.Gia,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                DanhchoId = p.DanhchoId,
                LoaisukienId = p.LoaiHinhId

            }).ToListAsync();



            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }
        [HttpGet("Firstsktt")]
        public async Task<IActionResult> GetTinTucDatenow()
        {
            DateTime today = DateTime.Now.Date;

       
            var p = await _context.SukienTintuc
                .Include(p => p.TinhThanh)
                .Include(p => p.Danhcho)
                .Include(p => p.LoaiHinhDL)
                .Where(p => p.DateOpen.Date >= today)
                .OrderBy(p => p.DateOpen)
                .FirstOrDefaultAsync();

            if (p == null)
                return NotFound("Không có sự kiện sắp diễn ra trong tương lai.");

            var list = new SuKienTintucMV1
            {
                SukienId = p.SukienId,
                Tieude = p.Tieude,
                Motangan = p.Motangan,
                Mota1 = p.Mota1,
                Diachi = p.Diachi,
                SDT = p.SDT,
                DateOpen = p.DateOpen,
                DateClose = p.DateClose,
                TinhThanh = p.TinhThanh.TenTinh,
                LoaiHinh = p.LoaiHinhDL.TenLoai,
                Danhcho = p.Danhcho.Doituong,
                Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                Imagemain = p.Imagemain,
                Image1 = p.Image1,
                Image2 = p.Image2,
                Image3 = p.Image3,
                Image4 = p.Image4,
                Image5 = p.Image5,
                Gia = p.Gia,
                LoaiHinhId = p.LoaiHinhId,
                TinhThanhId = p.TinhThanhId,
                DanhchoId = p.DanhchoId,
                LoaisukienId = p.LoaiHinhId
            };

            return Ok(list);
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
                var dt = await _context.SukienTintuc.CountAsync();

                return Ok(new { sukientintuc = dt });
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
        public async Task<IActionResult> SearchSukienTintuc(string? search,int? loaihinh=null,int? tinhthanh=null, int? danhcho=null,int? loaisukien=null ,string? sortBy="",int page=1)
        {
         
            try
            {
                DateTime now = DateTime.Now;
                var dt = _context.SukienTintuc.Include(p => p.TinhThanh).Include(p => p.Danhcho).Include(p => p.LoaiHinhDL).AsQueryable();

                #region loc theo
                // từ khoá 
                if (!string.IsNullOrEmpty(search))
                {
                    dt = dt.Where(p => p.Tieude != null && p.Tieude.ToLower().Contains(search.ToLower()));
                }
                //loại hình
                if (loaihinh.HasValue && loaihinh>0)
                {
                    dt = dt.Where(p => p.LoaiHinhId == loaihinh);
                }
                //danhcho
                if (danhcho> 0 && danhcho.HasValue)
                {
                    dt = dt.Where(p => p.DanhchoId == danhcho);
                }
                //tinhthanh
                if (tinhthanh.HasValue && tinhthanh> 0)
                {
                    dt = dt.Where(p => p.TinhThanhId == tinhthanh);
                }
                //tinhthanh
                if (loaisukien.HasValue && loaisukien > 0)
                {
                    dt = dt.Where(p => p.Loaisukien == loaisukien);
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
                     
                        case "date":
                            dt = dt.Where(p=>p.DateOpen>=now).OrderBy(p => p.DateOpen);
                            break;
                        default:
                            dt = dt.OrderByDescending(p => p.DateOpen); 
                            break;

                    }
                }
                #endregion
                var totalItems = await dt.CountAsync();
                if (totalItems == 0)
                {
                    return Ok(new { message = "Không tìm thấy sự kiện -  tin tức nào!" });
                }
                var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);
                dt = dt.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
                var list = await dt.Select(p => new SuKienTintucMV1
                {
                    SukienId = p.SukienId,
                    Tieude = p.Tieude,
                    Motangan = p.Motangan,
                    Mota1 = p.Mota1,
                    Diachi = p.Diachi,
                    SDT = p.SDT,
                    DateOpen = p.DateOpen,
                    DateClose = p.DateClose,
                    TinhThanh = p.TinhThanh.TenTinh,
                    LoaiHinh = p.LoaiHinhDL.TenLoai,
                    Danhcho = p.Danhcho.Doituong,
                    Loaisukien = GetLoaiSuKienName(p.Loaisukien),
                    Imagemain = p.Imagemain,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    Gia = p.Gia,
                    LoaiHinhId = p.LoaiHinhId,
                    TinhThanhId = p.TinhThanhId,
                    DanhchoId = p.DanhchoId,
                    LoaisukienId = p.LoaiHinhId

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

        public async Task<IActionResult> Create(SuKienTintucDTO p)
        {
            var requiredFields = new List<string> { p.Tieude, p.Mota1, p.Motangan, p.Diachi, p.Imagemain, p.Image1, p.Image2, p.Image3, p.Image4, p.Image5 };

            // Kiểm tra nếu bất kỳ trường nào bị rỗng
            if (requiredFields.Any(string.IsNullOrEmpty))
            {
                return BadRequest(new { message = "Vui lòng nhập đầy đủ các trường có dấu *!" });
            }

            // Kiểm tra ID không được bằng 0
            if (p.TinhThanhId == 0 || p.DanhchoId == 0 || p.LoaiHinhId == 0)
            {
                return BadRequest(new { message = "Vui lòng chọn đầy đủ các khoá ngoại!" });

            }

            try
            {

                var newdt = new SukienTintuc
                {
                    Tieude = p.Tieude,
                    Motangan = p.Motangan,
                    Mota1 = p.Mota1,
                    Diachi = p.Diachi,
                    SDT = p.SDT,
                    DateOpen = p.DateOpen,
                    DateClose = p.DateClose,
                    TinhThanhId = p.TinhThanhId,
                    LoaiHinhId = p.LoaiHinhId,
                    DanhchoId = p.DanhchoId,
                    Loaisukien = p.Loaisukien,
                    Imagemain = p.Imagemain,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    Gia = p.Gia
                };
                await _context.SukienTintuc.AddAsync(newdt);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm sự kiện - tin tức thành công!" });
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
        public async Task<IActionResult> EditLoaiHinh(int id, SuKienTintucDTO model)
        {

            try
            {
                var sk = await _context.SukienTintuc.FirstOrDefaultAsync(p => p.SukienId == id);
                if (sk == null)
                {
                    return NotFound(new { message = "Không tìm thấy sự kiện tin tức!" });
                }

                sk.Tieude = model.Tieude;
                sk.Motangan = model.Motangan;
                sk.Diachi = model.Diachi;
                sk.DateOpen = model.DateOpen;
                sk.DateClose = model.DateClose;
                sk.Gia = model.Gia;
                sk.SDT = model.SDT;
                sk.Mota1 = model.Mota1;
                sk.TinhThanhId = model.TinhThanhId;
                sk.LoaiHinhId = model.LoaiHinhId;
                sk.DanhchoId = model.DanhchoId;
                sk.Loaisukien = model.Loaisukien;
                sk.Imagemain = model.Imagemain;
                sk.Image1 = model.Image1;
                sk.Image2 = model.Image2;
                sk.Image3 = model.Image3;
                sk.Image4 = model.Image4;
                sk.Image5 = model.Image5;


                await _context.SaveChangesAsync();
                return Ok(new { message = "Sửa tin tức thành công!" });
            }
            catch (DbUpdateException ex)
            {
                return Ok(new { message =  ex.Message });
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
                var sk = await _context.SukienTintuc.FindAsync(id);
                if (sk == null)
                {
                    return NotFound(new { message = "Không tìm thấy sự kiện - tin tức!" });
                }
                _context.SukienTintuc.Remove(sk);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Xoá sự kiện - tin tức thành công!" });

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
