using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class accmast
    {
        public long id { get; set; }
        public string? acc_type { get; set; }
        public string? acc_sub_type { get; set; }
        public string? ledger { get; set; }
        public string? accno { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? from_Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? to_Date { get; set; }
        public string? name { get; set; }
        public string? product { get; set; }
        public string? proddesc { get; set; }
        public string? custid { get; set; }
        public string? mop { get; set; }
        public string? nri { get; set; }
        public string? staff { get; set; }
        public string? open_date { get; set; }
        public string? eff_date { get; set; }
        public string? close_date { get; set; }
        public double? depamt { get; set; }
        public double? matamt { get; set; }
        public string? matdate { get; set; }
        public double? year { get; set; }
        public double? month { get; set; }
        public double? days { get; set; }
        public decimal? limit { get; set; }
        public decimal? dp { get; set; }
        public string? limexpdt { get; set; }
        public string? f_txn_dt { get; set; }
        public string? l_txn_dt { get; set; }
        public string? fdrno { get; set; }
        public string? drcraccount { get; set; }
        public string? drcrbr { get; set; }
        public string? drcracc { get; set; }
        public string? status { get; set; }
        public string? lien { get; set; }
        public string? inttbl { get; set; }
        public double? intdiff { get; set; }
        public double? roi { get; set; }
        public string? lastchang { get; set; }
        public decimal? op_bal { get; set; }
        public decimal? cl_bal { get; set; }
        public string? majoritydt { get; set; }
        public string? sanc_date { get; set; }
        public double? sec_value { get; set; }
        public string? instfreq { get; set; }
        public double? instlamt { get; set; }
        public string? newacno { get; set; }
        public string? freeze { get; set; }
        public string? remarks1 { get; set; }
        public string? remarks2 { get; set; }
        public string? acc_desc { get; set; }
        public string? mkr { get; set; }
        public string? ckr { get; set; }
        public int? linkid { get; set; }
        public string? frzreson { get; set; }
        public string? dob { get; set; }
        public double? capint { get; set; }
        public string? lcsttxndt { get; set; }
        public string? lsystxndt { get; set; }
        public double? intpaid { get; set; }
        public string? glhead { get; set; }
        public string? glsubhead { get; set; }
        public string? minor { get; set; }
        public decimal? int_available { get; set; }
        public string? branch { get; set; }
        public string? grace_exp { get; set; }
        public string? pr_repstart_date { get; set; }
        public string? int_repstart_date { get; set; }
        public double? inca { get; set; }
        public string? baddebt { get; set; }
        public string? npa_date { get; set; }
        public string? newsol { get; set; }
        public string? statusdate { get; set; }
        public string? accowner { get; set; }
        public string? oldacno { get; set; }
        public int? PXid { get; set; }
        public string? bank { get; set; }
        public string? txndt { get; set; }
        public string? txnid { get; set; }
        public string? txn_number { get; set; }
        public string? txnleg { get; set; }
        public string? txntype { get; set; }
        public string? dr_cr { get; set; }
        public string? txn_desc { get; set; }
        public string? txn_desc2 { get; set; }
        public string? chq_no { get; set; }
        public decimal dramt { get; set; }
        public decimal cramt { get; set; }
        public decimal openingbalance { get; set; }
        public decimal closingbalance { get; set; }
        public decimal clbal { get; set; }
        public string? cname { get; set; }
        public string? foracid { get; set; }
        public string? bankname { get; set; }
        public string? br_name { get; set; }
        public string? br_addr1 { get; set; }
        public string? addr1 { get; set; }
        public string? addr2 { get; set; }
        public string? addr3 { get; set; }
        public string? accstatus { get; set; }
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public string? baltype { get; set; }
        public decimal tot_dr { get; set; }
        public decimal tot_cr { get; set; }



    }
    public class AccountModel
    {
        public accmast accmast { get; set; }=new accmast();
        public List<accmast> Accmast { get; set; } = new List<accmast>();
    }
}
