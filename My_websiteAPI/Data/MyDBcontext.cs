using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace My_websiteAPI.Data
{
    public class MyDBcontext:IdentityDbContext<ApplicationUser>
    {
        public MyDBcontext(DbContextOptions<MyDBcontext> options):base(options) { }

        public DbSet<My_websiteAPI.Model.Danhcho> Danhcho { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.Diadiem> Diadiem { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.Donggop> Donggop { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.itemLichsuTrainghiem> itemLichsuTrainghiem { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.itemListAwait> itemListAwait { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.LichsuTrainghiem> LichsuTrainghiem { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.ListAwait> ListAwait { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.LoaiHinh> LoaiHinh { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.SukienTintuc> SukienTintuc { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.TinhThanh> TinhThanh { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.Danhgia> Danhgias { get; set; } = default!;
        public DbSet<My_websiteAPI.Model.BannerThree> BannerThrees { get; set; } = default!;

    }
}
