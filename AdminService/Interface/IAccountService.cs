using AdminReopository.Model;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<TxnHistory>> GetAccountData(string accountNumber, DateTime startDate, DateTime endDate);
    }
}
