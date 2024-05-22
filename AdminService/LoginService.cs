using AdminReopository.Interface;
using AdminService.Interface;
using Microsoft.Extensions.Logging;

using System;

namespace AdminService
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginService> _logger;

        public LoginService(ILoginRepository loginRepository, ILogger<LoginService> logger)
        {
            _loginRepository = loginRepository;
            _logger = logger;
        }
        public bool Login(string username, string password)
        {
            try
            {
                return _loginRepository.Login(username, password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for username: {Username}", username);
                throw;
            }
        }

        public string? GetBankName(string username)
        {
            try
            {
                return _loginRepository.GetBankName(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving bank name for username: {Username}", username);
                throw;
            }
        }
    }
}
