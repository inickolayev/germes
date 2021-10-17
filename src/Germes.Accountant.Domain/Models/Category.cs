using System;
using Inbound = Germes.Accountant.Contracts.Inbound;
using Dto = Germes.Accountant.Contracts.Dto;

namespace Germes.Accountant.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan RowVersion { get; set; }
        public CategoryTypes CategoryType { get; set; }

        public Category(Inbound.AddExpenseCategoryRequest request)
        {
            UserId = request.UserId;
            Name = request.Name;
            Description = request.Description;
            CategoryType = CategoryTypes.Expose;
        }

        public Category(Inbound.AddIncomeCategoryRequest request)
        {
            UserId = request.UserId;
            Name = request.Name;
            Description = request.Description;
            CategoryType = CategoryTypes.Expose;
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
