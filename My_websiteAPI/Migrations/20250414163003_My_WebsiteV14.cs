using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace My_websiteAPI.Migrations
{
    /// <inheritdoc />
    public partial class My_WebsiteV14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannerThrees",
                columns: table => new
                {
                    BannerThreeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Image2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Image3 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerThrees", x => x.BannerThreeId);
                });

            migrationBuilder.CreateTable(
                name: "Danhcho",
                columns: table => new
                {
                    DanhchoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Doituong = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danhcho", x => x.DanhchoId);
                });

            migrationBuilder.CreateTable(
                name: "Donggop",
                columns: table => new
                {
                    DonggopId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tieude = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Noidung = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SDT = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    trangthai = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donggop", x => x.DonggopId);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHinh",
                columns: table => new
                {
                    LoaiHinhId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenLoai = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHinh", x => x.LoaiHinhId);
                });

            migrationBuilder.CreateTable(
                name: "TinhThanh",
                columns: table => new
                {
                    TinhThanhId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenTinh = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhThanh", x => x.TinhThanhId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichsuTrainghiem",
                columns: table => new
                {
                    LichsuTrainghiemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichsuTrainghiem", x => x.LichsuTrainghiemId);
                    table.ForeignKey(
                        name: "FK_LichsuTrainghiem_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListAwait",
                columns: table => new
                {
                    ListAwaitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListAwait", x => x.ListAwaitId);
                    table.ForeignKey(
                        name: "FK_ListAwait_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diadiem",
                columns: table => new
                {
                    DiadiemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tieude = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Motangan = table.Column<string>(type: "text", nullable: false),
                    Mocuadongcua = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Diachi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DateOC = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SDT = table.Column<string>(type: "text", nullable: false),
                    Gia = table.Column<int>(type: "integer", nullable: false),
                    Tinhtrang = table.Column<bool>(type: "boolean", nullable: false),
                    Noidung = table.Column<string>(type: "text", nullable: false),
                    TinhThanhId = table.Column<int>(type: "integer", nullable: false),
                    LoaiHinhId = table.Column<int>(type: "integer", nullable: false),
                    DanhchoId = table.Column<int>(type: "integer", nullable: false),
                    Luotxem = table.Column<int>(type: "integer", nullable: false),
                    Loaisukien = table.Column<int>(type: "integer", nullable: false),
                    Imagemain = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image3 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image4 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image5 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    urlmap = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diadiem", x => x.DiadiemId);
                    table.ForeignKey(
                        name: "FK_Diadiem_Danhcho_DanhchoId",
                        column: x => x.DanhchoId,
                        principalTable: "Danhcho",
                        principalColumn: "DanhchoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diadiem_LoaiHinh_LoaiHinhId",
                        column: x => x.LoaiHinhId,
                        principalTable: "LoaiHinh",
                        principalColumn: "LoaiHinhId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diadiem_TinhThanh_TinhThanhId",
                        column: x => x.TinhThanhId,
                        principalTable: "TinhThanh",
                        principalColumn: "TinhThanhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SukienTintuc",
                columns: table => new
                {
                    SukienId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tieude = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Motangan = table.Column<string>(type: "text", nullable: false),
                    Diachi = table.Column<string>(type: "text", nullable: false),
                    DateOpen = table.Column<DateTime>(type: "date", nullable: false),
                    DateClose = table.Column<DateTime>(type: "date", nullable: false),
                    Gia = table.Column<int>(type: "integer", nullable: false),
                    SDT = table.Column<string>(type: "text", nullable: false),
                    Mota1 = table.Column<string>(type: "text", nullable: false),
                    TinhThanhId = table.Column<int>(type: "integer", nullable: false),
                    LoaiHinhId = table.Column<int>(type: "integer", nullable: false),
                    DanhchoId = table.Column<int>(type: "integer", nullable: false),
                    Loaisukien = table.Column<int>(type: "integer", nullable: false),
                    Imagemain = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image3 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image4 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image5 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SukienTintuc", x => x.SukienId);
                    table.ForeignKey(
                        name: "FK_SukienTintuc_Danhcho_DanhchoId",
                        column: x => x.DanhchoId,
                        principalTable: "Danhcho",
                        principalColumn: "DanhchoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SukienTintuc_LoaiHinh_LoaiHinhId",
                        column: x => x.LoaiHinhId,
                        principalTable: "LoaiHinh",
                        principalColumn: "LoaiHinhId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SukienTintuc_TinhThanh_TinhThanhId",
                        column: x => x.TinhThanhId,
                        principalTable: "TinhThanh",
                        principalColumn: "TinhThanhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Danhgias",
                columns: table => new
                {
                    DanhgiaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Diem = table.Column<int>(type: "integer", nullable: false),
                    Noidung = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Ngay_add = table.Column<DateTime>(type: "date", nullable: false),
                    DiadiemId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danhgias", x => x.DanhgiaId);
                    table.ForeignKey(
                        name: "FK_Danhgias_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Danhgias_Diadiem_DiadiemId",
                        column: x => x.DiadiemId,
                        principalTable: "Diadiem",
                        principalColumn: "DiadiemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itemLichsuTrainghiem",
                columns: table => new
                {
                    itemLichsuTrainghiemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LichsuTrainghiemId = table.Column<int>(type: "integer", nullable: false),
                    DiadiemId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemLichsuTrainghiem", x => x.itemLichsuTrainghiemId);
                    table.ForeignKey(
                        name: "FK_itemLichsuTrainghiem_Diadiem_DiadiemId",
                        column: x => x.DiadiemId,
                        principalTable: "Diadiem",
                        principalColumn: "DiadiemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemLichsuTrainghiem_LichsuTrainghiem_LichsuTrainghiemId",
                        column: x => x.LichsuTrainghiemId,
                        principalTable: "LichsuTrainghiem",
                        principalColumn: "LichsuTrainghiemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itemListAwait",
                columns: table => new
                {
                    itemListAwaitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListAwaitId = table.Column<int>(type: "integer", nullable: false),
                    DiadiemId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemListAwait", x => x.itemListAwaitId);
                    table.ForeignKey(
                        name: "FK_itemListAwait_Diadiem_DiadiemId",
                        column: x => x.DiadiemId,
                        principalTable: "Diadiem",
                        principalColumn: "DiadiemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemListAwait_ListAwait_ListAwaitId",
                        column: x => x.ListAwaitId,
                        principalTable: "ListAwait",
                        principalColumn: "ListAwaitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Danhgias_DiadiemId",
                table: "Danhgias",
                column: "DiadiemId");

            migrationBuilder.CreateIndex(
                name: "IX_Danhgias_UserId",
                table: "Danhgias",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Diadiem_DanhchoId",
                table: "Diadiem",
                column: "DanhchoId");

            migrationBuilder.CreateIndex(
                name: "IX_Diadiem_LoaiHinhId",
                table: "Diadiem",
                column: "LoaiHinhId");

            migrationBuilder.CreateIndex(
                name: "IX_Diadiem_TinhThanhId",
                table: "Diadiem",
                column: "TinhThanhId");

            migrationBuilder.CreateIndex(
                name: "IX_itemLichsuTrainghiem_DiadiemId",
                table: "itemLichsuTrainghiem",
                column: "DiadiemId");

            migrationBuilder.CreateIndex(
                name: "IX_itemLichsuTrainghiem_LichsuTrainghiemId",
                table: "itemLichsuTrainghiem",
                column: "LichsuTrainghiemId");

            migrationBuilder.CreateIndex(
                name: "IX_itemListAwait_DiadiemId",
                table: "itemListAwait",
                column: "DiadiemId");

            migrationBuilder.CreateIndex(
                name: "IX_itemListAwait_ListAwaitId",
                table: "itemListAwait",
                column: "ListAwaitId");

            migrationBuilder.CreateIndex(
                name: "IX_LichsuTrainghiem_UserId",
                table: "LichsuTrainghiem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListAwait_UserId",
                table: "ListAwait",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SukienTintuc_DanhchoId",
                table: "SukienTintuc",
                column: "DanhchoId");

            migrationBuilder.CreateIndex(
                name: "IX_SukienTintuc_LoaiHinhId",
                table: "SukienTintuc",
                column: "LoaiHinhId");

            migrationBuilder.CreateIndex(
                name: "IX_SukienTintuc_TinhThanhId",
                table: "SukienTintuc",
                column: "TinhThanhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BannerThrees");

            migrationBuilder.DropTable(
                name: "Danhgias");

            migrationBuilder.DropTable(
                name: "Donggop");

            migrationBuilder.DropTable(
                name: "itemLichsuTrainghiem");

            migrationBuilder.DropTable(
                name: "itemListAwait");

            migrationBuilder.DropTable(
                name: "SukienTintuc");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LichsuTrainghiem");

            migrationBuilder.DropTable(
                name: "Diadiem");

            migrationBuilder.DropTable(
                name: "ListAwait");

            migrationBuilder.DropTable(
                name: "Danhcho");

            migrationBuilder.DropTable(
                name: "LoaiHinh");

            migrationBuilder.DropTable(
                name: "TinhThanh");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
