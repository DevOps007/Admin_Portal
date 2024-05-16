using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminReopository.Model
{
    public class Login
    {
        public int Pid { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? Location { get; set; }
        public string? Solid { get; set; }
        public string? BankCode { get; set; }
        public string? BranchCode { get; set; }
        public DateTime TxnStart { get; set; }
        public DateTime TxnEnd { get; set; }
        public string? CEDSolid { get; set; }
    }
}
