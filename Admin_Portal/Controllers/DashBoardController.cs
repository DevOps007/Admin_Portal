using AdminReopository.Model;
using AdminService.Interface;
using Bank_Portal.Helpers;
using DataLayer.Model;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection.Metadata;
using System.Collections;
using Admin_Portal.Models;
using AdminReopository.Interface;
using System.Drawing.Printing;
using AdminService;

namespace Admin_Portal.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAccountService _accountservice;
        private readonly ILoginRepository _loginRepository;
        private readonly ILoginService _loginService;
        public DashBoardController(IAccountService accountservice, IWebHostEnvironment webHostEnvironment, ILoginRepository loginRepository, ILoginService loginService)
        {
            _accountservice = accountservice;
            _webHostEnvironment = webHostEnvironment;
            _loginRepository = loginRepository;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _loginService = loginService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashBoard()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                string bankName = _loginService.GetBankName(username);
                ViewBag.bankName = bankName;
            }
            return View();
        }
       
        public IActionResult GetAccount()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                string bankName = _loginService.GetBankName(username);
                ViewBag.bankName = bankName;
            }           
            var model = new Tuple<AccountModel, IEnumerable<TxnHistory>>(new AccountModel(), new List<TxnHistory>());
         
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccount(string accno, DateTime startDate, DateTime endDate)
        {
            try
            {
                
                if (User.Identity.IsAuthenticated)
                {
                    string username = User.Identity.Name;
                    string bankName = _loginService.GetBankName(username);
                    ViewBag.bankName = bankName;
                }

                
                var accountStatement = await _accountservice.GetAccountData(accno, startDate, endDate);

               
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

                
                return View(new Tuple<AccountModel, IEnumerable<TxnHistory>>(accountModel, accountStatement));
            }
            catch (Exception ex)
            {
               
                Log.Error(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
                return View("Error");
            }
        }

        public async Task<IActionResult> DownloadPdf(DateTime startDate, DateTime endDate, string accno)
        {
            try
            {
                var transactions = await _accountservice.GetAccountData(accno, startDate, endDate);
                if (transactions == null || !transactions.Any())
                {
                    throw new InvalidOperationException("No transactions found for the given account and date range.");
                }

                //var lastTransaction = transactions.OrderByDescending(t => t.fromdate).FirstOrDefault();

                
                //var lastBalance = lastTransaction.balance;

                string reportFilePath = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report.rdlc";
                List<Admin_Portal.Models.Login> loginList = new List<Admin_Portal.Models.Login>();
                List<DataLayer.Model.AccountMaster> accountMasters = new List<AccountMaster>();
                AccountMaster accountMaster = new AccountMaster();
                Admin_Portal.Models.Login loginModel = new Admin_Portal.Models.Login();

                    loginModel.BankName = transactions.FirstOrDefault().bankname;
                    loginModel.BranchName = transactions.FirstOrDefault().br_name;
                    loginModel.Location = transactions.FirstOrDefault().addr1;
                    loginModel.txn_start = DateTime.Now.Date;

                    accountMaster.proddesc = transactions.FirstOrDefault().accstatus;
                    accountMaster.newacno = transactions.FirstOrDefault().newacno;
                    accountMaster.dp = transactions.FirstOrDefault().cname;
                    accountMaster.staff = $"Account Statement From {startDate.ToString("dd-MM-yyyy")} To {endDate.ToString("dd-MM-yyyy")}";
            
                transactions.ToList().ForEach(x => { x.chq_no = x.chq_no == null ? "" : x.chq_no; });
                transactions.ToList().ForEach(x => { x.baltype = x.baltype == null ? "" : x.baltype; });
                
                accountMasters.Add(accountMaster);
                loginList.Add(loginModel);

                using (Stream reportDefinition = new FileStream(reportFilePath, FileMode.Open, FileAccess.Read))
                {
                    IEnumerable dataSource = transactions;
                    IEnumerable loginDataSource = loginList;
                    IEnumerable accountMasterDataSource = accountMasters;

                    using (LocalReport report = new LocalReport())
                    {
                        report.LoadReportDefinition(reportDefinition);
                        report.DataSources.Add(new ReportDataSource("TxnHistory", dataSource));
                        report.DataSources.Add(new ReportDataSource("Login", loginDataSource));
                        report.DataSources.Add(new ReportDataSource("AccountMaster", accountMasterDataSource));
                        var pageSettings = report.GetDefaultPageSettings();
                        byte[] reportData = report.Render("PDF");
                        return File(reportData, "application/pdf");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }

}
