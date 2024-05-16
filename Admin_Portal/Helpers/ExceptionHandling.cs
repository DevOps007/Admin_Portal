using Bank_Portal.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using System.Text.Json;

namespace Bank_Portal.Helpers
{
    public class ExceptionHandling
    {

        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExceptionHandling> _logger;
        private readonly ITempDataProvider _tempDataProvider;

        public ExceptionHandling(RequestDelegate next, IConfiguration configuration, ILogger<ExceptionHandling> logger, ITempDataProvider tempDataProvider)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tempDataProvider = tempDataProvider ?? throw new ArgumentNullException(nameof(tempDataProvider));
        }

        public async Task Invoke(HttpContext context)
        
        {

            bool enableTrace = bool.Parse(_configuration["EnableTrace"] ?? "false");

            FnTraceWrap traceWrap = null;

            if (enableTrace)
            {
                traceWrap = new FnTraceWrap();
            }

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (traceWrap != null)
                {
                    traceWrap.TraceMessage("Exception occurred: {0}", ex.Message);
                }

                _logger.LogError(ex, "An unhandled exception occurred.");

                var errorResponse = new ErrorModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message ?? "An error occurred while processing your request.",
                    Exception = ex
                };
                var result = JsonSerializer.Serialize(errorResponse);

                var tempData = _tempDataProvider.LoadTempData(context);
                tempData["ErrorMessage"] = result;
                _tempDataProvider.SaveTempData(context, tempData);

                context.Response.Redirect("/Error/Exception500Error");
            }
            finally
            {
                traceWrap?.Dispose();
            }
        }

    }
}
