using System;
using System.ComponentModel.DataAnnotations;
using Inbound = Germes.User.Contracts.Inbound;

namespace Germes.User.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string ChatId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        [Timestamp] public byte[] RowVersion { get; private set; }

        public User(Inbound.AddUserRequest request)
        {
            ChatId = request.ChatId;
            Name = request.Name;
            CreatedAt = DateTime.UtcNow;
        }
        
        // For EF
        internal User()
        {}
    }
}