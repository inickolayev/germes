using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Germes.Configurations;
using Germes.Mediators.Requests;

namespace Germes.Controllers
{
    public class VersionController : BaseApiController
    {
        private readonly IMediator _meditor;

        public VersionController(IMediator mediator)
        {
            _meditor = mediator;
        }

        [HttpGet]
        public async Task<ApplicationInfoSettings> IndexAsync(CancellationToken token)
        {
            var appInfo = await _meditor.Send(new RequestApplicationInfo(), token);
            return new ApplicationInfoSettings
            {
                Name = appInfo.Name,
                Version = appInfo.Version
            };
        }
    }
}
