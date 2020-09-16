﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Requests
{
    public class RequestNewMessage : IRequest<BotResult>
    {
        public BotMessage Message { get; set; }
    }
}
