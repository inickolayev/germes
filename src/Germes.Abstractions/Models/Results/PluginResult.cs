namespace Germes.Abstractions.Models.Results
{
    public class PluginResult
    {
        public bool IsSuccess { get; set; }
        public PluginSuccessResult Result { get; set; }
        public PluginErrorResult Error { get; set; }

        public static PluginResult Success(string message = null, bool needToAnswer = false)
            => new PluginResult
            {
                IsSuccess = true,
                Result = new PluginSuccessResult
                {
                    NeedToAnswer = string.IsNullOrEmpty(message) || needToAnswer,
                    Message = message
                }
            };
        
        public static PluginResult Fail(string error)
            => new PluginResult
            {
                IsSuccess = false,
                Error = new PluginErrorResult
                {
                    Message = error
                }
            };
    }

    public class PluginErrorResult
    {
        public string Message { get; set; }
    }

    public class PluginSuccessResult
    {
        public bool NeedToAnswer { get; set; }
        public string Message { get; set; }
    }
}