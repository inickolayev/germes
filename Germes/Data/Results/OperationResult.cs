using Newtonsoft.Json;
using System;

namespace Germes.Data.Results
{
    public class OperationResult<TResult> : OperationResult
    {
        public OperationResult(TResult result)
            : base()
        {
            Result = result;
        }
        public OperationResult(AbstractError error)
            : base(error)
        {
            Result = default;
        }

        public TResult Result { get; set; }
    }
    public class OperationResult
    {
        public OperationResult()
        {
            IsSuccess = true;
        }
        public OperationResult(AbstractError error)
            : this()
        {
            IsSuccess = false;
            Error = error;
        }

        public bool IsSuccess { get; set; }
        public AbstractError Error { get; set; }
        
        public OperationResult<TNewResult> To<TNewResult>()
            => new OperationResult<TNewResult>(Error);
    }
}