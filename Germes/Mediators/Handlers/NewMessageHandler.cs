using Germes.Domain.Data.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Germes.Data;
using Germes.Mediators.Requests;
using Germes.Implementations.Services;

namespace Germes.Mediators.Handlers
{
    public class NewMessageHandler : IRequestHandler<RequestNewMessage, OperationResult<BotResult>>
    {
        private readonly IBotService _bot;
        public NewMessageHandler(IBotService bot)
        {
            _bot = bot;
        }

        public Task<OperationResult<BotResult>> Handle(RequestNewMessage request, CancellationToken cancellationToken)
            => _bot.HandleNewMessageAsync(request.Message, cancellationToken);
    }
}
