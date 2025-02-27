using System.Configuration;
using NetCoreMVC.Models;
using ExtendMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using NetCoreMVC.Models;
using NetCoreMVC.Services;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using NetCoreMVC.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddOptions();
var mailSetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSetting);
builder.Services.AddScoped<SmtpClient>();
builder.Services.AddScoped<IEmailSender, SendMailService>();

builder.Services.AddDbContext<AppDbContext>(option => {
    string connectString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseSqlServer(connectString);
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Mac dinh la tim file theo kieu : /Views/ControllerName/ActionName.cshtml
    /*
        {0} - Ten Action
        {1} - Ten controller
        {2} - Ten area
    */
    options.ViewLocationFormats.Add("/MyViews/{1}/{0}" + RazorViewEngine.ViewExtension);
    // Tim file theo kieu : /Views/ControllerName/ActionName.cshtml


});

builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<PlanetService>();

// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    // SignIn settings
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = true;
});

builder.Services.AddAuthentication()
    .AddGoogle(options => {
        var gconfig = builder.Configuration.GetSection("Authentication:Google");
        options.ClientId = gconfig["ClientId"];
        options.ClientSecret = gconfig["ClientSecret"];
        options.CallbackPath = "/signin-google";
    })
    .AddFacebook(options => {
        var fconfig = builder.Configuration.GetSection("Authentication:Facebook");
        options.AppId = fconfig["AppId"];
        options.AppSecret = fconfig["AppSecret"];
        options.CallbackPath = "/signin-facebook";
    });

builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewManageMenu", builder => {
        builder.RequireAuthenticatedUser();
        builder.RequireRole(RoleName.Administrator);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // Endpoint Routing Middleware
app.AddStatusCodePage(); // Tạo ra trang lỗi từ 400 trở đi
// Có thể đưa vào tham số để chỉ hiển thị trang lỗi cho các mã lỗi cụ thể


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapStaticAssets();

app.MapGet("/sayhi", async context =>
{
    await context.Response.WriteAsync($"Hello World! {DateTime.Now}");
});

// Tao cac endpoint anh xa url vao controller
/*
    MapController -> endpoint ddịnh nghĩa trực tiếp trong Controller
    MapControllerRoute
    MapDefaultControllerRoute
    MapAreaControllerRoute -> Controller nằm trong các mục riêng
*/

app.MapControllerRoute(
    name : "first",
    pattern : "/{url}/{id?}", // Nếu viết nnay sẽ có thể truy cập từ mọi url
    defaults : new { controller = "First", action = "ViewProduct" },
    constraints : new {
        url = "xemsanpham" , // Điều kiện url
        id = new RangeRouteConstraint(2,4), // Điều kiện id
    } // có thể viết ở pattern. Ví dụ : /{url:xemsanpham}/{id:range(2,4)}
);

app.MapAreaControllerRoute(
    name : "ProductManage",
    areaName : "ProductManage",
    pattern : "/{controller}/{action=Index}/{id?}"
);

//Chỉ dành cho Controller không có Area
app.MapControllerRoute(
    name : "default",
    pattern : "/{controller=Home}/{action=Index}/{id?}"//-> CÓ thể bỏ default value nếu không bỏ sẽ dùng trong default value
    //defaults : new { controller = "First", action = "ViewProduct", id = 3 }
    // controller = "Home" -> Controller name
    // action = "Index" -> Action name
);



app.MapRazorPages();

app.Run();
