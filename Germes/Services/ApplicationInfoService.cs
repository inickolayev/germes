using Germes.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Germes.Services
{
    public class ApplicationInfoService : IApplicationInfoService
    {
        private readonly ApplicationInfoSettings _applicationInfo;
        private readonly ILogger<ApplicationInfoService> _logger;

        public ApplicationInfoService(ILogger<ApplicationInfoService> logger, IConfiguration configuration)
        {
            _applicationInfo = configuration.GetSection("ApplicationInfo").Get<ApplicationInfoSettings>();
            _logger = logger;
        }

        public ApplicationInfoSettings GetApplicationInfo()
        {
            _logger.LogInformation("{@_applicationInfo}", _applicationInfo);
            return _applicationInfo;
        }
    }
}
