using AdminReopository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminReopository.Interface
{
    public interface ILoginRepository
    {
        bool Login(string username, string password);
         string? GetBankName(string username);
        //Task<Login> GetLoginAsync(string userName);
    }
}
