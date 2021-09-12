using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models;
using Germes.Abstractions.Services;
using Germes.Mediators.Requests;

namespace Germes.Mediators.Handlers
{
    /// <summary>
    ///     Naming Convention: Обязательный суфикс Handler
    ///     Обработчик запросов типа указан в <see cref="IRequestHandler{TRequest,TResponse}"/>.
    /// </summary>
    public class ApplicationInfoHandler : IRequestHandler<RequestApplicationInfo, ApplicationInfo>
    {
        private readonly IApplicationInfoService _service;
        public ApplicationInfoHandler(IApplicationInfoService service)
        {
            _service = service;
        }

        Task<ApplicationInfo> IRequestHandler<RequestApplicationInfo, ApplicationInfo>.Handle(RequestApplicationInfo request, CancellationToken cancellationToken)
            => Task.FromResult(_service.GetApplicationInfo());
    }
}
