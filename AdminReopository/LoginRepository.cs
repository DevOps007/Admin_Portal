using AdminReopository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AdminReopository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _configuration;
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
    }
}
