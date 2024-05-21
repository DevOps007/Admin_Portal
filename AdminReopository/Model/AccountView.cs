using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class AccountView
    {
        public string accno { get; set; }
        public DateTime from_Date { get; set; }
        public DateTime to_Date { get; set; }
        
        public List<accmast> Accmaste { get; set; } = new List<accmast>();
    }
}
