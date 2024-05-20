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

                string sql = @"
            SELECT 
                acc_type, 
                accno, 
                name, 
                fdrno, 
                limit, 
                op_bal, 
                cl_bal,
                newacno,
                status 
            FROM 
                accmast 
            WHERE 1=1"; // Start with a condition that is always true

                var parameters = new DynamicParameters();

                bool hasFilter = false;

                // Append filters only if the parameters are provided
                if (!string.IsNullOrEmpty(accmast.accno))
                {
                    sql += " AND newacno = @accno";
                    parameters.Add("@accno", accmast.accno);
                    hasFilter = true;
                }
                if (accmast.open_date.HasValue || accmast.open_end_date.HasValue)
                {
                    if (!hasFilter) sql += " AND ";
                    else sql += " OR ";
                    if (accmast.open_end_date != null)
                    {
                        sql += " open_date >= @open_date AND open_date <= @open_end_date";
                    }
                    else
                    {
                        sql += " open_date >= @open_date";
                    }
                    parameters.Add("@open_date", accmast.open_date);
                    parameters.Add("@open_end_date", accmast.open_end_date);
                    hasFilter = true;
                }


                if (accmast.close_date.HasValue || accmast.close_end_date.HasValue)
                {
                    if (!hasFilter) sql += " AND ";
                    else sql += " OR ";
                    if (accmast.close_end_date != null)
                    {
                        sql += " close_date >= @close_date AND close_date <= @close_end_date";
                    }
                    else
                    {
                        sql += " close_date >= @close_date";
                    }
                    parameters.Add("@close_date", accmast.close_date);
                    parameters.Add("@close_end_date", accmast.close_end_date);
                    hasFilter = true;
                }

                if (!string.IsNullOrEmpty(accmast.oldacno))
                {
                    if (!hasFilter) sql += " AND ";
                    else sql += " OR ";
                    sql += " oldacno = @oldacno";
                    parameters.Add("@oldacno", accmast.oldacno);
                    hasFilter = true;
                }
                if (!string.IsNullOrEmpty(accmast.name))
                {
                    if (!hasFilter) sql += " AND ";
                    else sql += " OR ";
                    sql += "name = @name";
                    parameters.Add("@name", accmast.name);
                    hasFilter = true;
                }
                if (!string.IsNullOrEmpty(accmast.status))
                {
                    if (!hasFilter) sql += " AND ";
                    else sql += " OR ";
                    sql += " status = @status";
                    parameters.Add("@status", accmast.status);
                    hasFilter = true;
                }

                var result = await _dbConnection.QueryAsync<accmast>(sql, parameters);

                return result;
            }
            catch (Exception ex)
            {
                // Handle exceptions or logging here
                return null;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

    }
}
