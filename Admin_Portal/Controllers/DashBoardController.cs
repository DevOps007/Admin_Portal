using AdminService;
using AdminService.Interface;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Admin_Portal.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IAccountService _accountservice;

        public DashBoardController(IAccountService accountservice)
        {
            _accountservice = accountservice;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashBoard()
        {
            return View();
        }
        public IActionResult GetAccount()
        {
            return View(new AccountModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetAccount(string accno, DateTime startDate, DateTime endDate)
        {
            try
            {
                var accountData = await _accountservice.GetAccountData(accno, startDate, endDate);

                return View(accountData); 
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
                return View("Error");
            }
        }





    }
}
