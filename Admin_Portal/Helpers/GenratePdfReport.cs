using DataLayer.Model;
using System.Text;

namespace Bank_Portal.Helpers
{
    public class GenratePdfReport
    {
        public string GetHtmlTemplate(IEnumerable<TxnHistory> transactions)
        {
            string htmlTemplate = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
</head>
<body style='margin: 0; padding: 0; text-indent: 0;'>
    <div style='position: absolute; left: 0; right: 0; top: 11%; text-align: center;'>
      <p style='margin: 0; padding: 0 0 2px 0; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: bold; text-decoration: none; font-size: 10pt;'>{transactions.FirstOrDefault()?.bankname}</p>
        <p style='margin: 0; padding: 0 0 2px 0; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: bold; text-decoration: none; font-size: 10pt;'>{transactions.FirstOrDefault()?.br_name}</p>
        <p style='margin: 0; padding: 0 0 2px 0; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: bold; text-decoration: none; font-size: 10pt;'>{transactions.FirstOrDefault()?.br_addr1}</p>
        <br />
        <table style='vertical-align: top; overflow: visible; margin: 0 auto;'>
            <tr style='margin-top: 20px;'>
                <td style='text-align: left;'>
                    <p style='padding-top: 4pt;margin-top:20px!important; padding-left: 5pt; text-indent: 0pt; line-height: 11pt; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 12pt; vertical-align: -1pt;'>Account Number: {transactions.FirstOrDefault()?.newacno}</p>
                    <p style='text-indent: -11pt; line-height: 130%; padding-left: 5pt; text-indent: 0pt; line-height: 11pt; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 12pt; vertical-align: -1pt;'>Address: {transactions.FirstOrDefault()?.addr1}</p>
                </td>
                <td style='text-align: right;'>
                    <p style='padding-left: 52pt; text-indent: 0pt; text-align: right;'>Scheme: INDIVIDUAL SAVING</p>
                    <p style='padding-left: 65pt; text-indent: -11pt; line-height: 130%; text-align: right;'>New A/c No. {transactions.FirstOrDefault()?.accno}</p>
                    <p style='padding-left: 65pt; text-indent: -11pt; line-height: 130%; text-align: right;'>A/c Status: {transactions.FirstOrDefault()?.accstatus}</p>
                </td>
            </tr>
        </table>
        <p style='text-indent: 0pt; text-align: left;'><br /></p>
        <table style='vertical-align: top; overflow: visible; margin: 0 auto;'>
            <tr>
                <td style='text-align: center;'>
                    <p style='padding-left: 46pt; text-indent: 0pt; text-align: center; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 12pt;'>Account Statement From 01-01-2023 To 22-12-2023</p>
                </td>
            </tr>
        </table>
        <p style='text-indent: 0pt; text-align: left;'><br /></p>
        <table style='border-collapse:collapse; vertical-align: top; overflow: visible; margin: 0 auto; width: 80%;' cellspacing='0'>
            <thead>
                <tr style='height: 19pt;border-bottom: 1pt solid black;'>
                    <td style='width: 55pt; text-align: center; border-bottom: 1pt solid black; border-top: 1pt solid black;'>
                           <p style='text-indent: 0pt; text-align: center; font-weight: bold; font-size: 10pt;'>Date</p>
                    </td>
                    <td style='width: 200pt; text-align: center; border-bottom: 1pt solid black; border-top: 1pt solid black;'>
                         <p style='text-indent: 0pt; text-align: center; font-weight: bold; font-size: 10pt;'>Txn Type</p>
                        <p style='text-indent: 0pt; text-align: center; font-weight: bold; font-size: 10pt;'>Particulars Chq. No.</p>
                    </td>
                    <td style='width: 70pt; text-align: center; border-bottom: 1pt solid black; border-top: 1pt solid black;'>
                        <p style='text-indent: 0pt; text-align: center; font-weight: bold; font-size: 10pt;'>Withdrawal</p>
                    </td>
                    <td style='width: 70pt; text-align: center; border-bottom: 1pt solid black; border-top: 1pt solid black;'>
                        <p style='text-indent: 0pt; text-align: center; font-weight: bold; font-size: 10pt;'>Deposit</p>
                    </td>
                    <td style='width: 70pt; text-align: center; border-bottom: 1pt solid black; border-top: 1pt solid black;'>
                        <p style='text-indent: 0pt; text-align: center; font-weight: bold; font-size: 10pt;'>Balance</p>
                    </td>
                </tr>
            </thead>
            <tbody>";
            foreach (var txn in transactions)
            {
                htmlTemplate += $@"
                <tr style='height: 26pt;'>
                    <td style='width: 55pt;'>
                        <p style='padding-top: 2pt; padding-left: 2pt; text-indent: 0pt; text-align: left; color: black; font-family: Arial Narrow, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 10pt;'>{txn.txndate.ToString("dd-MM-yyyy")}</p>
                    </td>
                    <td style='width: 200pt;'>
                        <p style='padding-top: 2pt; padding-left: 10pt; text-indent: 0pt; line-height: 11pt; text-align: left; color: black; font-family: Arial Narrow, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 10pt;'>{txn.txn_desc}</p>
                        <p style='padding-left: 10pt; text-indent: 0pt; line-height: 11pt; text-align: left; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 10pt;'>{txn.txnid}</p>
                    </td>
                    <td style='width: 70pt;'>
                        <p style='padding-top: 1pt; padding-right: 21pt; text-indent: 0pt; text-align: right; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 10pt;'>{txn.dramt}</p>
                    </td>
                    <td style='width: 70pt;'>
                        <p style='padding-top: 1pt; padding-right: 16pt; text-indent: 0pt; text-align: right; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 10pt;'>{txn.cramt}</p>
                    </td>
                    <td style='width: 70pt;'>
                        <p style='padding-top: 1pt; padding-right: 19pt; text-indent: 0pt; text-align: right; color: black; font-family: Calibri, sans-serif; font-style: normal; font-weight: normal; text-decoration: none; font-size: 10pt;'>{txn.closingbalance}CR</p>
                    </td>
                </tr>";
            }

            htmlTemplate += @"
            </tbody>
        </table>
    </div>
</body>
</html>";
            return htmlTemplate;
        }
        public static string GetTemplate(IEnumerable<TxnHistory> transactions)
        {
            var transaction = transactions.GetEnumerator();
            transaction.MoveNext(); // Assuming there is at least one transaction for simplicity

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' xml:lang='en' lang='en'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <style type='text/css'>
        * {
            margin: 0;
            padding: 0;
            text-indent: 0;
        }
        .p, p {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 12pt;
            margin: 0pt;
        }
        .s1 {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 12pt;
            vertical-align: -1pt;
        }
        .s2 {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 12pt;
        }
        .s3 {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 10pt;
        }
        .s4 {
            color: black;
            font-family: 'Arial Narrow', sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 10pt;
        }
        .s5 {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 12pt;
            vertical-align: 3pt;
        }
        .s6 {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 10pt;
        }
        table, tbody {
            vertical-align: top;
            overflow: visible;
        }
    </style>
</head>
<body>
    <div style='position: absolute; left: 25%; top: 11%; display: grid;'>
        <h3 style='align-content:center; margin-left: 15%;'>" + transaction.Current.bankname + @"</h3><br />
        <h4 style='align-content: center; margin-left: 40%;'>" + transaction.Current.br_name + @"</h4>
        <h5 style='align-content: center; margin-left: 21%; margin-top: 13px;'>" + transaction.Current.br_addr1 + @"</h5><br />
        <table>
           <tr style='margin-top:20px'>
               <td>
                   <p class='s1' style='padding-top: 4pt; padding-left: 5pt; text-indent: 0pt; line-height: 11pt; text-align: left;'><span class='p'>Account Number :" + transaction.Current.accno + @"</span></p>
                   <p class='s1' style='padding-top: 4pt; padding-left: 5pt; text-indent: 0pt; line-height: 11pt; text-align: left;'><span class='p'>Address :" + transaction.Current.addr1 + @"</span></p>
                   <p style='padding-top: 5pt; padding-left: -11pt; text-indent: 0pt; text-align: left;'>DGIR DIST LATUR HANDI 413518</p>
               </td>
               <td>
                   <p style='padding-left: 52pt; text-indent: 0pt; text-align: left;'>Scheme : " + transaction.Current.proddesc + @"</p>
                   <p style='padding-left: 65pt; text-indent: -11pt; line-height: 130%; text-align: left;'>New A/c No. " + transaction.Current.newacno + @"</p>
                   <p style='padding-left: 65pt; text-indent: -11pt; line-height: 130%; text-align: left;'>A/c Status : " + transaction.Current.accstatus + @"</p>
               </td>
           </tr>
        </table>
        <p style='text-indent: 0pt; text-align: left;'><br /></p>
        <table style='border-collapse:collapse' cellspacing='0'>
            <tr style='height:19pt'>
                <td style='width:55pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p style='text-indent: 0pt; text-align: left;'><br /></p>
                </td>
                <td style='width:261pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p class='s2' style='padding-left: 46pt; text-indent: 0pt; text-align: left;'>Account Statement From " + transaction.Current.fromdate + @" To</p>
                </td>
                <td style='width:90pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p class='s2' style='padding-left: 3pt; text-indent: 0pt; text-align: left;'>" + transaction.Current.todate + @"</p>
                </td>
                <td style='width:175pt; border-bottom-style:solid; border-bottom-width:1pt' colspan='2'>
                    <p style='text-indent: 0pt; text-align: left;'><br /></p>
                </td>
            </tr>
            <tr style='height:17pt'>
                <td style='width:55pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p style='text-indent: 0pt; text-align: left;'></p>
                    <p class='s2' style='padding-top: 1pt; padding-left: 8pt; text-indent: 0pt; line-height: 14pt; text-align: left;'>Date</p>
                </td>
                <td style='width:261pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p class='s2' style='padding-left: 16pt; text-indent: 0pt; line-height: 14pt; text-align: left;'>Txn Type Particulars Chq. No.</p>
                </td>
                <td style='width:90pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p class='s2' style='padding-right: 16pt; text-indent: 0pt; line-height: 14pt; text-align: right;'>Withdrawal</p>
                </td>
                <td style='width:80pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p class='s2' style='padding-left: 16pt; text-indent: 0pt; text-align: left;'>Deposit</p>
                </td>
                <td style='width:95pt; border-bottom-style:solid; border-bottom-width:1pt'>
                    <p class='s2' style='padding-left: 16pt; text-indent: 0pt; text-align: left;'>Balance</p>
                </td>
            </tr>
            <tr style='height:17pt'>
                <td style='width:55pt'>
                    <p style='text-indent: 0pt; text-align: left;'><br /></p>
                </td>
                <td style='width:261pt'>
                    <p class='s2' style='padding-left: 22pt; text-indent: 0pt; text-align: left;'>Opening Balance</p>
                </td>
                <td style='width:90pt'>
                    <p style='text-indent: 0pt; text-align: left;'><br /></p>
                </td>
                <td style='width:80pt'>
                    <p style='text-indent: 0pt; text-align: left;'><br /></p>
                </td>
                <td style='width:95pt'>
                    <p class='s3' style='padding-top: 3pt; padding-right: 19pt; text-indent: 0pt; text-align: right;'>" + transaction.Current.openingbalance + @"CR</p>
                </td>
            </tr>");

            foreach (var txn in transactions)
            {
                sb.Append($@"
            <tr style='height:25pt'>
                <td style='width:55pt'>
                    <p class='s4' style='padding-top: 2pt; padding-left: 2pt; text-indent: 0pt; text-align: left;'>{txn.txndt}</p>
                </td>
                <td style='width:261pt'>
                    <p class='s4' style='padding-top: 2pt; padding-left: 10pt; text-indent: 0pt; line-height: 11pt; text-align: left;'>{txn.txn_desc}</p>
                    <p class='s3' style='padding-left: 10pt; text-indent: 0pt; line-height: 11pt; text-align: left;'>{txn.txn_desc2}</p>
                </td>
                <td style='width:90pt'>
                    <p class='s3' style='padding-top: 1pt; padding-right: 21pt; text-indent: 0pt; text-align: right;'>{(txn.dramt > 0 ? txn.dramt.ToString() : "")}</p>
                </td>
                <td style='width:80pt'>
                    <p class='s3' style='padding-top: 1pt; padding-right: 21pt; text-indent: 0pt; text-align: right;'>{(txn.cramt > 0 ? txn.cramt.ToString() : "")}</p>
                </td>
                <td style='width:95pt'>
                    <p class='s3' style='padding-top: 1pt; padding-right: 19pt; text-indent: 0pt; text-align: right;'>{txn.clbal}CR</p>
                </td>
            </tr>");
            }

            sb.Append(@"
        </table>
    </div>
</body>
</html>");

            return sb.ToString();
        }
    }

}

