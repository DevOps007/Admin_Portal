using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class AccountMaster
    {
        
        public string? accno { get; set; }
        public string ? txndt { get; set; }
        public string? txnid { get; set; }
        public string ?txn_number { get; set; }
        public string ?txnleg { get; set; }
        public string? txntype { get; set; }
        public string? dr_cr { get; set; }
        public string? txn_desc { get; set; }
        public string?  txn_desc2 { get; set; }
        public string? chq_no { get; set; }
        public decimal dramt { get; set; }
        public decimal cramt { get; set; }
        public decimal openingbalance { get; set; }
        public decimal closingbalance { get; set; }
        public decimal clbal { get; set; }
        public string? cname { get; set; }
        public string? custid { get; set; }
        public string? foracid { get; set; }
        public string? bankname { get; set; }
        public string? br_name { get; set; }
        public string? br_addr1 { get; set; }
        public string? newacno { get; set; }
        public string?  addr1 { get; set; }
        public string? addr2 { get; set; }
        public string? addr3 { get; set; }
        public string? proddesc { get; set; }
        public string? accstatus { get; set; }
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public string? baltype { get; set; }
        public decimal tot_dr { get; set; }
        public decimal tot_cr { get; set; }
        public string dp { get; set; }
        public string staff { get; set; }
    }
}