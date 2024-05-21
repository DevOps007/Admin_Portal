using BusinessLayer.Interface;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Bank_Portal.Controllers
{
    [Authorize]
    public class AccountMasterController : Controller
    {
        private readonly IAccountMasterService _accountMasterService;

        public AccountMasterController(IAccountMasterService accountMasterService)
        {
            _accountMasterService = accountMasterService ?? throw new ArgumentNullException(nameof(accountMasterService));
        }

        
        [HttpGet]
        public async Task<IActionResult> SearchResult()
        {
            return View(new AccountModel());
        }

        [HttpPost]
        public async Task<IActionResult> SearchResult(AccountModel accountModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchResult = await _accountMasterService.SearchAccountsAsync(accmaster);

            TempData["AccNo"] = accmaster.accno;
            TempData["OldAccNo"] = accmaster.oldacno;
            TempData["CloseDate"] = accmaster.close_date;
            TempData["OpenDate"] = accmaster.open_date;

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

            return View(accountModel.Accmast);
        }

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

                var account = await _accountMasterService.GetAccountByAccnoAsync(accno);

                if (accountModel == null)
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
                    cl_bal = account.cl_bal,
                    op_bal = account.op_bal,
                    accowner = account.accowner,
                    acc_desc = account.acc_desc,
                    acc_sub_type = account.acc_sub_type,
                    close_date = account.close_date,
                    open_date = account.open_date,
                    status = account.status,
                };

                return View("Details", accountModel.accmast);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving account details for account number {Accno}", accno);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
