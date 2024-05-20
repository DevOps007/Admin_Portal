using AdminReopository.Interface;
using AdminReopository.Model;
using AdminService.Interface;
using DataLayer.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AdminService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<AccountModel> GetAccountData(string accno, DateTime startDate, DateTime endDate)
        {
            try
            {
                var accmast = new accmast()
                {
                    accno = accno,
                    from_Date = startDate,
                    to_Date = endDate
                };
                var accountMasterList= await _accountRepository.ExecuteYourStoredProcedure(accmast.from_Date, accmast.to_Date, accno);
                var accountModel = new AccountModel()
                {
                    accmast = accmast,
                    Accmast= accountMasterList.Accmast
                };
                return accountModel;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
                throw;
            }
        }
    }
}
