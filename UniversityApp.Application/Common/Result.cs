namespace UniversityApp.Application.Common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }

        public static Result<T> Ok(T data)    => new() { Success = true,  Data = data };
        public static Result<T> Fail(string e) => new() { Success = false, Error = e    };
    }
}
