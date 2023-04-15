using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using QuanLyHoiThao.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("QuanLyHoiThaoDBContextConnection") ?? throw new InvalidOperationException("Connection string 'QuanLyHoiThaoDBContextConnection' not found.");

builder.Services.AddDbContext<QuanLyHoiThaoDBContext>(options => options.UseSqlServer(connectionString));

//Xác thực không được để trống ký tự
builder.Services.AddDefaultIdentity<HoiThaoUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<QuanLyHoiThaoDBContext>();


//Xác thực mail
//builder.Services.AddDefaultIdentity<HoiThaoUser>(options =>
//{
//    options.SignIn.RequireConfirmedEmail = true;
//})
//    .AddEntityFrameworkStores<QuanLyHoiThaoDBContext>()
//    .AddDefaultTokenProviders();

//builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
//builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Yêu cầu password trên 6 ký tự
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
