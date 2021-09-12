using Germes.Abstractions.Models;
using MediatR;

namespace Germes.Mediators.Requests
{
    /// <summary>
    ///     Naming Convention: Обязательный префикс Request
    /// </summary>
    public class RequestApplicationInfo : IRequest<ApplicationInfo>
    {
        public string RequestParametrExample { get; set; } = "Example request parametr";
    }
}
