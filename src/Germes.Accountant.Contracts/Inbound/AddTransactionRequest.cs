using System;

namespace Germes.Accountant.Contracts.Inbound
{
    public class AddTransactionRequest
    {
        public decimal Cost { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}