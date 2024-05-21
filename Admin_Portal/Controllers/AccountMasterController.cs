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
            var result = await _accountMasterService.SearchAccountsAsync(accountModel.accmast);
            accountModel.Accmast = result.ToList();
            return View(accountModel);
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

                if (account == null)
                {
                    return NotFound();
                }

                var accountModel = new accmast
                {
                    accno = account.accmast.accno,
                    name = account.accmast.name,
                    acc_type = account.accmast.acc_type,
                    branch = account.accmast.branch,
                    bank = account.accmast.bank,
                    custid = account.accmast.custid,
                    cl_bal = account.accmast.cl_bal,
                    op_bal = account.accmast.op_bal,
                    accowner = account.accmast.accowner,
                    acc_desc = account.accmast.acc_desc,
                    acc_sub_type = account.accmast.acc_sub_type,
                    close_date = account.accmast.close_date,
                    open_date = account.accmast.open_date,
                    status = account.accmast.status,
                };

                return View("Details", accountModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving account details for account number {Accno}", accno);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
