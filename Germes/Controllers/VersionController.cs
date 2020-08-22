using Germes.Controllers;
using Germes.Data;
using Germes.Services.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CheckIIS.Controllers
{
    public class VersionController : BaseApiController
    {
        private readonly IMediator _meditor;

        public Mediator Mediator { get; }

        public VersionController(IMediator mediator)
        {
            _meditor = mediator;
        }

        [HttpGet]
        public Task<ApplicationInfoSettings> IndexAsync(CancellationToken token)
            => _meditor.Send(new RequestApplicationInfo(), token);
    }
}
