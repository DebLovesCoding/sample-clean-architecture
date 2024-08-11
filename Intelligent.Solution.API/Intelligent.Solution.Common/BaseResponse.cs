namespace Intelligent.Solution.Common
{
    public class BaseResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public Error? Error { get; set; }
        public T? Data { get; set; }
    }

    public class Error
    {
        public string? Code { get; set; }
        public string? Message { get; set; }

        public static Error ToErrorMessage(string code, string errorMessage)
        {
            return new Error { Code = code, Message = errorMessage };
        }
    }
}
