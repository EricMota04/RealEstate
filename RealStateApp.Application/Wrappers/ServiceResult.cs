namespace RealEstateApp.Application.Wrappers
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public List<string> Errors { get; private set; } = new();

        private ServiceResult(bool isSuccess, T? value, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Value = value;
            if (errors != null) Errors = errors;
        }

        public static ServiceResult<T> Success(T value) => new ServiceResult<T>(true, value);

        public static ServiceResult<T> Failure(string error) => new ServiceResult<T>(false, default, new List<string> { error });

        public static ServiceResult<T> Failure(List<string> errors) => new ServiceResult<T>(false, default, errors);
    }
}
