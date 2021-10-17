using System;
using Inbound = Germes.User.Contracts.Inbound;

namespace Germes.User.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string ChatId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan RowVersion { get; set; }

        public User(Inbound.AddUserRequest request)
        {
            ChatId = request.ChatId;
            Name = request.Name;
        }
    }
}