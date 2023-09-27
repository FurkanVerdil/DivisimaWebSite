using Divisima.BL.Repositories;
using Divisima.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();  // Data Merkezli bir proje türü olduðu için WithViews() seçeneði mevcut deðil.


builder.Services.AddDbContext<SQLContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));


builder.Services.AddEndpointsApiExplorer();  // Route ile alakalý
// builder.Services.AddSwaggerGen();  // swagger isimli bir dökümantasyon sisteminin gösterilecek

builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Divisima API - Version 1",
        Description = "Bu projede .net core 7.0 kullanýlmýþtýr",
        TermsOfService = new Uri("https://www.divisima.com/sozlesme"),
        Contact = new OpenApiContact
        {
            Name = "Developer ALÝ",
            Email = "ali@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT Licence",
            Url = new Uri("https://www.divisima.com/sozlesme")
        }
    });
    sw.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "izinVerienOriginler",
        policy =>
        {
            policy.WithOrigins("http://localhost:5100").AllowAnyMethod(/*HTTPGET-HTTPPOST-HTTPPUT-HTTPDELETE*/).AllowCredentials();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5153", // API saðlayýcý Key -- API KATMANI
        ValidAudience = "hepsiburada",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BubenimOzelSignInKey"))
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // eðer geliþtirme ortamýndaysak
{
    app.UseSwagger(); // swagger kullan
    app.UseSwaggerUI();  // ve swagger UI olarak kullanýlsýn
}

app.UseCors("izinVerienOriginler");

app.UseAuthorization();

//app.UseCors(x => x
//                .AllowAnyMethod() // Genel HTTP metodlarý üzerinde bir sýnýrlama olmamasý için
//                .AllowAnyHeader() // http baþlýklý yerlerden gelen isteklere izin verir
//                .SetIsOriginAllowed(origin => true) // 
//                .AllowCredentials());

app.MapControllers(); // default bir route oluþtur

app.Run();
