﻿using AdminService;
using AdminService.Interface;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace Admin_Portal.Controllers
{
    public class Adminlogin : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IAccountService _accountservice;

        public Adminlogin(ILoginService loginService, IAccountService accountservice)
        {
            _loginService = loginService;
            _accountservice = accountservice;
        }

        [HttpGet]
        public IActionResult Login()
        {

            ViewBag.ShowLoginForm = true;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (_loginService.Login(username, password))
            {

                return RedirectToAction("DashBoard", "DashBoard");
            }
            else
            {

                Log.Warning("Unsuccessful login attempt for username: {Username}", username);

                ModelState.AddModelError(string.Empty, "Invalid username or password.");

                ViewBag.ShowAlert = true;
                return View();
            }
        }
        //[HttpPost]
        ////public IActionResult Login(string username, string password)
        ////{
        ////    if (_loginService.Login(username, password))
        ////    {
        ////        // Successful login
        ////        Log.Information("Successful login for username: {Username}", username);
        ////        TempData["SuccessMessage"] = "Login successful.";
        ////        return RedirectToAction("DashBoard", "DashBoard");
        ////    }
        ////    else
        ////    {
        ////        // Unsuccessful login
        ////        Log.Warning("Unsuccessful login attempt for username: {Username}", username);
        ////        TempData["ErrorMessage"] = "Invalid username or password.";
        ////        return RedirectToAction("Login", "AdminLogin"); // Redirect to login page or any other appropriate page
        ////    }
        ////}

    }

}
