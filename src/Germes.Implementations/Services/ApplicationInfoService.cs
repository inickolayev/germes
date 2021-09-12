using Germes.Abstractions.Models;
using Germes.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace Germes.Implementations.Services
{
    public class ApplicationInfoService : IApplicationInfoService
    {
        private readonly ApplicationInfo _applicationInfo;
        private readonly ILogger<ApplicationInfoService> _logger;

        public ApplicationInfoService(ILogger<ApplicationInfoService> logger, ApplicationInfo info)
        {
            _applicationInfo = info;
            _logger = logger;
        }

        public ApplicationInfo GetApplicationInfo()
        {
            _logger.LogInformation("{@_applicationInfo}", _applicationInfo);
            return _applicationInfo;
        }
    }
}
