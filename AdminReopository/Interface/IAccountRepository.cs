using AdminReopository.Model;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminReopository.Interface
{
    public interface IAccountRepository
    {
        Task<AccountModel> ExecuteYourStoredProcedure(DateTime? startDate, DateTime? endDate, string accountNumber);
    }
}
