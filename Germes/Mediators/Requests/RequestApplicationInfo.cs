using Germes.Configurations;
using MediatR;

namespace Germes.Mediators.Requests
{
    /// <summary>
    ///     Naming Convention: Обязательный префикс Request
    /// </summary>
    public class RequestApplicationInfo : IRequest<ApplicationInfoSettings>
    {
        public string RequestParametrExample { get; set; } = "Example request parametr";
    }
}
