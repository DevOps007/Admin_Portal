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


        //public IActionResult GetAccount(string accno, DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {

        //        accmast obj = new accmast();

        //        var accountData = _accountservice.GetAccountData(accno, startDate, endDate);
        //        var accountModel = new accmast
        //        {

        //            accno = obj.accno,
        //            from_Date = endDate.ToString("dd-mm-yyyy"),
        //            to_Date = startDate.ToString("dd-mm-yyyy"),
        //            acc_type = obj.acc_type,
        //            acc_desc = obj.acc_desc,
        //            branch = obj.branch,
        //            cl_bal = obj.cl_bal,
        //            op_bal = obj.op_bal,
        //            fdrno = obj.fdrno,
        //            open_date = obj.open_date,
        //            accowner = obj.accowner


        //        };



        //        return View(accountData);
        //    }
        //    catch (Exception ex)
        //    {

        //        Log.Error(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
        //        throw;
        //    }
        //}
        public IActionResult GetAccount()
        {
            return View(new AccountModel());
        }
        //[HttpPost]
        //public IActionResult GetAccount(string accno, DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {
        //        var accountData = _accountservice.GetAccountData(accno, startDate, endDate);
        //        var accmast = new accmast()
        //        {
        //            accno = accno,
        //            from_Date = startDate,
        //            to_Date = endDate
        //        };
        //        var accountModel = new AccountModel
        //        {
        //            accmast = accmast
        //        };

        //        return View(accountModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
        //        throw;
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> GetAccount(string accno, DateTime startDate, DateTime endDate)
        {
            try
            {
                var accountData = await _accountservice.GetAccountData(accno, startDate, endDate);

                return View(accountData); // Make sure this returns the correct view
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
                return View("Error"); // Return an error view if needed
            }
        }





    }
}
