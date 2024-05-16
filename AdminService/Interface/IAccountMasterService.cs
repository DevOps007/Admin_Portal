using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAccountMasterService
    {
        Task<IEnumerable<accmast>> SearchAccountsAsync(accmast accmast);

        Task<AccountModel> GetAccountByAccnoAsync(string accno);
    }
}
