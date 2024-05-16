using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DataLayer.Model;
using DataLayer;
using AdminReopository.EDMX;
using AdminReopository.Interface;


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

        public async Task<AccountModel> ExecuteYourStoredProcedure(DateTime? startDate, DateTime? endDate, string accno)
        {
            try
            {
                using (var connection = _dbConnection)
                {
                    connection.Open();
                    var fromDate = startDate?.ToString("yyyy-MM-dd");
                    var toDate = endDate?.ToString("yyyy-MM-dd");
                    var parameters = new DynamicParameters();
                    parameters.Add("@from_Date", startDate);
                    parameters.Add("@to_Date", endDate);
                    parameters.Add("@acc_number", accno);
                    var result = await connection.QueryAsync<accmast>("stmt", parameters, commandType: CommandType.StoredProcedure);
                    var accountModel = new AccountModel();
                    accountModel.Accmast = result.AsList();
                    return accountModel;
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
