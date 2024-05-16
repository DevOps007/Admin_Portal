using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DataLayer.Model;
using DataLayer;
using AdminReopository.EDMX;
using AdminReopository.Interface;
using System.Globalization;

namespace AdminReopository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AdminEntities _context;
        private readonly ILogger<AccountRepository> _logger;
        private readonly IDbConnection _dbConnection;

        public AccountRepository(AdminEntities context, ILogger<AccountRepository> logger, DbConnection dbConnection)
        {
            _context = context;
            _logger = logger;
            _dbConnection = new SqlConnection(dbConnection.GetDbConnection());
        }

        public async Task<AccountModel> ExecuteYourStoredProcedure(DateTime? startDate, DateTime? endDate, string accno)
        {
            try
            {
                using (var connection = _dbConnection)
                {
                    connection.Open();
                    var fromDate = startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : null;
                    var toDate = endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : null;
                    var parameters = new DynamicParameters();
                    parameters.Add("@from_Date", fromDate);
                    parameters.Add("@to_Date", toDate);
                    parameters.Add("@acc_number", accno);

                    var result =await connection.QueryAsync<accmast>("stmt", parameters, commandType: CommandType.StoredProcedure);
                    var accountModel=new AccountModel();
                    accountModel.Accmast = result.AsList();
                    return accountModel;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing stored procedure");
                throw;
            }
        }
    }
}
