using Germes.Data;
using Germes.Services.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services.Handlers
{
    /// <summary>
    ///     Naming Convention: Обязательный суфикс Handler
    ///     Обработчик запросов типа указанно в IRequestHandler<TRequest..
    /// </summary>
    public class ApplicationInfoHandler : IRequestHandler<RequestApplicationInfo, ApplicationInfoSettings>
    {
        private readonly IApplicationInfoService _service;
        public ApplicationInfoHandler(IApplicationInfoService service)
        {
            _service = service;
        }

        Task<ApplicationInfoSettings> IRequestHandler<RequestApplicationInfo, ApplicationInfoSettings>.Handle(RequestApplicationInfo request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetApplicationInfo());
        }
    }
}
