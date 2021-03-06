﻿using Germes.Data;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Services.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services.Handlers
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
