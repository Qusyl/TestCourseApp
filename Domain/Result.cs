

namespace Domain
{
    public sealed class Result<TValue, TError>
    {
        public bool IsSuccess { get; private set; }

        public TError? Error { get; private set; }

        public TValue Value { get; private set; }

        private Result(TValue? value, TError? error, bool success)
        {
            IsSuccess = success;
            Value = value;
            Error = error;
        }

        public static Result<TValue, TError> Success(TValue? value) => new Result<TValue, TError>(value, default, true);

        public static Result<TValue, TError> Failure(TError error) => new Result<TValue, TError>(default, error, false);

    }

    public sealed class Result<TError>
    {
        public bool IsSuccess { get; }
        public TError Error { get; }

        private Result(bool isSuccess, TError error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }


        public static Result<TError> Success => new Result<TError>(true, default);

        public static Result<TError> Failure(TError error) => new Result<TError>(false, error);
    }
}
