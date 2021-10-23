using System;
using System.ComponentModel.DataAnnotations;
using Inbound = Germes.Accountant.Contracts.Inbound;

namespace Germes.Accountant.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public decimal Cost { get; private set; }
        public string Comment { get; private set; }
        public Guid UserId { get; private set; }
        public Guid CategoryId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        [Timestamp] public byte[] RowVersion { get; private set; }

        public Transaction(Inbound.AddTransactionRequest request)
        {
            Cost = request.Cost;
            Comment = request.Comment;
            UserId = request.UserId;
            CategoryId = request.CategoryId;
            CreatedAt = DateTime.UtcNow;
        }
        
        // For EF
        internal Transaction()
        {
        }
    }
}
