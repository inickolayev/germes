using System;
using Germes.Abstractions.Models.Results;

namespace Germes.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        private readonly BusinessError _error;

        public BusinessException(BusinessError error)
            : base(error.Message)
        {
            _error = error;
        }
    }
}