using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Germes.Configurations;
using Germes.Mediators.Requests;
using Germes.Implementations.Services;

namespace Germes.Mediators.Handlers
{
    /// <summary>
    ///     Naming Convention: Обязательный суфикс Handler
    ///     Обработчик запросов типа указан в <see cref="IRequestHandler{TRequest,TResponse}"/>.
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
