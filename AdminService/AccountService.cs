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

        public async Task<IEnumerable<TxnHistory>> GetAccountData(string accno, DateTime startDate, DateTime endDate)
        {
            try
            {
                var accmast = new accmast()
                {
                    from_Date = startDate,
                    to_Date = endDate
                };
                var accountStatementList= await _accountRepository.ExecuteYourStoredProcedure(accmast.from_Date, accmast.to_Date, accno);
                return accountStatementList;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred while retrieving account data for account number: {AccountNumber}", accno);
                throw; // Re-throw the exception to be caught by the caller
            }
        }
    }
}
