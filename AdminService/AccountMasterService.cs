﻿    using BusinessLayer.Interface;
    using DataLayer.Interface;
    using DataLayer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace BusinessLayer
    {
        public class AccountMasterService : IAccountMasterService
        {
            private readonly IAccountMasterRepository _accountMasterRepository;

            public AccountMasterService(IAccountMasterRepository accountMasterRepository)
            {
                _accountMasterRepository = accountMasterRepository;
            }

        public async Task<AccountModel> GetAccountByAccnoAsync(string accno)
        {
            var accountModel=new AccountModel();
            accountModel.accmast= await _accountMasterRepository.GetAccountByAccnoAsync(accno);
            return accountModel;
        }

        public async Task<IEnumerable<accmast>> SearchAccountsAsync(accmast accmast)
            {
                try
                {
                    var searchResult = await _accountMasterRepository.SearchAccountsAsync(accmast);
                    return searchResult;
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while searching accounts.", ex);
                }
            }
        }
    }
