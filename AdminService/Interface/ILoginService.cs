using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Interface
{
    public interface ILoginService
    {
        bool Login(string username, string password);
    }
}
