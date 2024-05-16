using AdminReopository.EDMX;
using DataLayer.Interface;
using DataLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataLayer
{
    public class AccountMasterRepository : IAccountMasterRepository
    {
        private readonly IDbConnection _dbConnection;

        public AccountMasterRepository(DbConnection dbConnection)
        {
            _dbConnection = new SqlConnection(dbConnection.GetDbConnection());
        }

        public async Task<accmast> GetAccountByAccnoAsync(string accno)
        {
            
            string query = "SELECT * FROM accmast WHERE accno = @Accno";

            return await _dbConnection.QueryFirstOrDefaultAsync<accmast>(query, new { Accno = accno });
        }

        public async Task<IEnumerable<accmast>> SearchAccountsAsync(accmast accmast)
        {
            try
            {
                 _dbConnection.Open();
                var sql = "SELECT acc_type,accno,name,fdrno,limit,op_bal,cl_bal,status from accmast where accno=@accno or open_date=@open_date or close_date=@close_date or oldacno=@oldacno or status=@status";
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(new { accmast.accno,accmast.open_date,accmast.close_date,accmast.oldacno,accmast.status});
                var result = await _dbConnection.QueryAsync<accmast>(sql, parameters);
                return result;
            }
            catch (Exception ex) { return null; }

        }
    }
}
