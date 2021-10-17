using System;

namespace Germes.User.Contracts.Inbound
{
    public class AddUserRequest
    {
        public string ChatId { get; set; }
        public string Name { get; set; }
    }
}