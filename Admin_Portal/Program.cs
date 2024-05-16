using AdminReopository;
using AdminReopository.EDMX;
using AdminReopository.Interface;
using AdminService;
using AdminService.Interface;
using Bank_Portal.Helpers;
using BusinessLayer;
using BusinessLayer.Interface;
using DataLayer;
using DataLayer.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Data;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//       .AddCookie(options =>
//       {
//           options.LoginPath = "/AdminLogin/Login";
//           options.AccessDeniedPath = "/DashBoard/DashBoard";
//           options.AccessDeniedPath = "/DashBoard/GetAccount";
//           options.AccessDeniedPath = "/AccountMaster/Search";
//       });


// Configure Serilog
var serilogConfiguration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(serilogConfiguration)
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    builder.Host.UseSerilog();

    builder.Services.AddRazorPages();

    // Add DbContext and Serilog
    builder.Services.AddDbContext<AdminEntities>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


    builder.Services.AddControllersWithViews();
    //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "Adminlogin/Login");

    // Add your services
    builder.Services.AddScoped<ILoginService, LoginService>();
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<IAccountMasterService, AccountMasterService>();
    builder.Services.AddScoped<IAccountMasterRepository, AccountMasterRepository>();
    builder.Services.AddTransient<ILogCleanupJob, LogCleanupJob>();
    builder.Services.AddScoped<DbConnection>();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });
    builder.Services.AddAuthorization();
    builder.Services.AddControllersWithViews();
    var app = builder.Build();
   

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseSerilogRequestLogging();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<ExceptionHandling>();

   
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=AdminLogin}/{action=Login}/{id?}");
  

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
