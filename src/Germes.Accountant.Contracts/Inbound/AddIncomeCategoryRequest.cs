using System;

namespace Germes.Accountant.Contracts.Inbound
{
    public class AddIncomeCategoryRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}