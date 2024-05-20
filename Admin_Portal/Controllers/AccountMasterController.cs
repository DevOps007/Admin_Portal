using BusinessLayer.Interface;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace Bank_Portal.Controllers
{
    [Authorize]
    public class AccountMasterController : Controller
    {
        public readonly IAccountMasterService _accountmasterservice ;

        public AccountMasterController(IAccountMasterService accountMasterService)
        {
            _accountmasterservice = accountMasterService;
        }

        
        [HttpGet]
        public async Task<IActionResult> SearchResult()
        {
            return View(new AccountModel());
        }

        [HttpPost]
        public async Task<IActionResult> SearchResult(AccountModel accountModel)
        {
            if (accountModel == null)
            {
                return BadRequest("Account model cannot be null");
            }

            var searchResult = await _accountmasterservice.SearchAccountsAsync(accountModel.accmast);

            var accmast = new accmast()
            {
                accno = accountModel.accmast.accno,
                oldacno = accountModel.accmast.oldacno,
                close_date = accountModel.accmast.close_date,
                close_end_date=accountModel.accmast.close_end_date,
                open_date = accountModel.accmast.open_date,
                open_end_date=accountModel.accmast.open_end_date,
                name= accountModel.accmast.name,
                status= accountModel.accmast.status,
            };

            var updatedAccountModel = new AccountModel
            {
                Accmast = searchResult.ToList(),
                accmast = accmast
            };

            return View(updatedAccountModel);
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

                var accountModel = await _accountmasterservice.GetAccountByAccnoAsync(accno);

                if (accountModel == null)
                {
                    return NotFound();
                }
                return View("Details", accountModel.accmast);

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
