using Germes.Abstractions.Models.Results;
using Germes.Data;
using Germes.Domain.Data;
using MediatR;

namespace Germes.Mediators.Requests
{
    public class RequestNewMessage : IRequest<OperationResult<BotResult>>
    {
        public BotMessage Message { get; set; }
    }
}
