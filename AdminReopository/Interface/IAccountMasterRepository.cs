using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface IAccountMasterRepository
    {
        Task<IEnumerable<accmast>> SearchAccountsAsync(accmast accmast);

        Task<accmast> GetAccountByAccnoAsync(string accno);

    }
}
