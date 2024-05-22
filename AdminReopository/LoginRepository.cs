using AdminReopository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using AdminReopository.Model;
using Dapper;

namespace AdminReopository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<LoginRepository> _logger;

        public LoginRepository(IConfiguration configuration, ILogger<LoginRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
           
        }

        public bool Login(string username, string password)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_Login", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                      
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        SqlParameter resultParameter = new SqlParameter("@Result", SqlDbType.Int);
                        resultParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParameter);

                        connection.Open();
                        command.ExecuteNonQuery();

                      
                        int result = Convert.ToInt32(resultParameter.Value);

                       
                        _logger.LogInformation("Login attempt for username: {Username}, Result: {Result}", username, result);

                        
                        return result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "Error occurred during login");
                throw;
            }
        }

        public string? GetBankName(string username)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "SELECT BankName FROM Login WHERE UserName = @Username";

                    var bankName = connection.QuerySingleOrDefault<string>(sql, new { Username = username });

                    return bankName;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving bank name for username: {Username}", username);
                throw;
            }
        }
    }
}
