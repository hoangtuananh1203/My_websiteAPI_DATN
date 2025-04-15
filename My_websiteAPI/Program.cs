using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using My_websiteAPI.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var conf = builder.Configuration;
builder.Services.AddSingleton(new ElasticsearchClient(new ElasticsearchClientSettings(new Uri("http://localhost:9200"))));

// cau hinh cho cac client co the sd 
builder.Services.AddCors(option =>
{
    option.AddPolicy("Allow", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "MY_website", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description="Nhap Token tai day",
        Type= SecuritySchemeType.Http,
        BearerFormat="JWT",
        Name="Authorization",
        Scheme="Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id="Bearer"
                }

            },new string[]{}
        }

    });
});


// Add services to the container.(
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<MyDBcontext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = conf["JWT:ValidAudience"],
        ValidIssuer = conf["JWT:ValidIssuer"],
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});


builder.Services.AddAuthorization();
builder.Services.AddDbContext<MyDBcontext>(option => option.UseNpgsql(conf.GetConnectionString("MyDB1")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Allow");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
