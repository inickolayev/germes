using System;
using Inbound = Germes.Accountant.Contracts.Inbound;

namespace Germes.Accountant.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan RowVersion { get; set; }

        public Transaction(Inbound.AddTransactionRequest request)
        {
            Cost = request.Cost;
            Comment = request.Comment;
            UserId = request.UserId;
            CategoryId = request.CategoryId;
            CreatedAt = request.CreatedAt;
        }
    }
}
