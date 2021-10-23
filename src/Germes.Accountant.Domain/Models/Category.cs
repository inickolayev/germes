using System;
using System.ComponentModel.DataAnnotations;
using Inbound = Germes.Accountant.Contracts.Inbound;
using Dto = Germes.Accountant.Contracts.Dto;

namespace Germes.Accountant.Domain.Models
{
    public class Category
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public CategoryTypes CategoryType { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        [Timestamp] public byte[] RowVersion { get; private set; }

        public Category(Inbound.AddExpenseCategoryRequest request)
        {
            UserId = request.UserId;
            Name = request.Name;
            Description = request.Description;
            CategoryType = CategoryTypes.Expose;
            CreatedAt = DateTime.UtcNow;
        }

        public Category(Inbound.AddIncomeCategoryRequest request)
        {
            UserId = request.UserId;
            Name = request.Name;
            Description = request.Description;
            CategoryType = CategoryTypes.Expose;
            CreatedAt = DateTime.UtcNow;
        }
        
        // For EF
        internal Category()
        {
        }

        public Dto.ExpenseCategoryDto ToExpenseCategoryDto()
            => new()
            {
                Id = Id,
                UserId = UserId,
                Name = Name,
                Description = Description
            };
        
        public Dto.IncomeCategoryDto ToIncomeCategoryDto()
            => new()
            {
                Id = Id,
                UserId = UserId,
                Name = Name,
                Description = Description
            };
    }
}
