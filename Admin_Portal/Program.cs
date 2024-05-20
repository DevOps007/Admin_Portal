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

    
    builder.Services.AddDbContext<AdminEntities>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


    builder.Services.AddControllersWithViews();
 

    
    builder.Services.AddScoped<ILoginService, LoginService>();
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<IAccountMasterService, AccountMasterService>();
    builder.Services.AddScoped<IAccountMasterRepository, AccountMasterRepository>();
    builder.Services.AddTransient<ILogCleanupJob, LogCleanupJob>();
    builder.Services.AddScoped<DbConnection>();

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
