using Germes.Controllers;
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
        public Task<ApplicationInfoSettings> IndexAsync(CancellationToken token)
            => _meditor.Send(new RequestApplicationInfo(), token);
    }
}
