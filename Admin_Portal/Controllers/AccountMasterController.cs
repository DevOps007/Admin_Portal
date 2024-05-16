using BusinessLayer.Interface;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace Bank_Portal.Controllers
{
    public class AccountMasterController : Controller
    {
        public readonly IAccountMasterService _accountmasterservice ;

        public AccountMasterController(IAccountMasterService accountMasterService)
        {
            _accountmasterservice = accountMasterService;
        }


        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SearchResult(accmast accmaster)
        {
            var searchResult = await _accountmasterservice.SearchAccountsAsync(accmaster);

            TempData["AccNo"] = accmaster.accno;
            TempData["OldAccNo"] = accmaster.oldacno;
            TempData["CloseDate"] = accmaster.close_date;
            TempData["OpenDate"] = accmaster.open_date;

            var accountModel = new AccountModel
            {
                Accmast = searchResult.ToList()
            };
            return View(accountModel.Accmast);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string accno)
        {
            try
            {
                if (string.IsNullOrEmpty(accno))
                {
                    return NotFound();
                }

                var account = await _accountmasterservice.GetAccountByAccnoAsync(accno);

                if (account == null)
                {
                    return NotFound();
                }

                var accountModel = new accmast
                {
                    accno = account.accno,
                    name = account.name,
                    acc_type = account.acc_type,
                    branch = account.branch,
                    bank = account.bank,
                    custid = account.custid,
                    cl_bal= account.cl_bal,
                    op_bal = account.op_bal,
                    accowner = account.accowner,
                    acc_desc = account.acc_desc,
                    acc_sub_type = account.acc_sub_type,
                    close_date = account.close_date,
                    open_date = account.open_date,
                    status = account.status,
                };

                return View("Details", accountModel);

            }
            catch (Exception ex)
            {
                // Log the exception here
                Log.Error(ex, "An error occurred while retrieving account details for account number {Accno}", accno);

                // Redirect to an error page
                return RedirectToAction("Error", "Home");
            }
        }










    }
}
