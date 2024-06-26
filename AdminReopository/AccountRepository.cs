﻿using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DataLayer.Model;
using DataLayer;
using AdminReopository.EDMX;
using AdminReopository.Interface;
using AdminReopository.Model;


namespace AdminReopository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;
        private readonly IDbConnection _dbConnection;

        public AccountRepository(AdminEntities context, ILogger<AccountRepository> logger, DbConnection dbConnection)
        {
            _logger = logger;
            _dbConnection = new SqlConnection(dbConnection.GetDbConnection());
        }

        public async Task<IEnumerable<TxnHistory>> ExecuteYourStoredProcedure(DateTime? startDate, DateTime? endDate, string accno)
        {
            try
            {
                using (var connection = _dbConnection)
                {
                    connection.Open();
                    var fromDate = startDate.HasValue ? startDate.Value.ToString("dd-MM-yyyy") : null;
                    var toDate = endDate.HasValue ? endDate.Value.ToString("dd-MM-yyyy") : null;
                    var parameters = new DynamicParameters();
                    parameters.Add("@from_Date", fromDate);
                    parameters.Add("@to_Date", toDate);
                    parameters.Add("@acc_number", accno);
                    var result = await connection.QueryAsync<TxnHistory>("stmt", parameters, commandType: CommandType.StoredProcedure);
                    return result.AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing stored procedure");
                throw;
            }
            finally
            {
                _dbConnection.Close();
            }
        }
    }
}
