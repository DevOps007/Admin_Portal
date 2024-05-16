using AdminService.Interface;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Admin_Portal.Controllers
{
    [Authorize]
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
        public IActionResult GetAccount(string accno, DateTime startDate, DateTime endDate)
        {
            try
            {
                var accountData = _accountservice.GetAccountData(accno, startDate, endDate);
                var accmast = new accmast()
                {
                    accno = accno,
                    from_Date = startDate,
                    to_Date = endDate
                };
                var accountModel = new AccountModel
                {
                    accmast = accmast
                };

                return View(accountModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
                throw;
            }
        }

    }
}
