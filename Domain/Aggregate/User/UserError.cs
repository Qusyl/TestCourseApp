

namespace Domain.Aggregate.User
{
    public class UserError
    {
        public string Message { get;  }

        public string Code { get; }

        private UserError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public static UserError InvalidEmailError => new UserError("Email is incorrect", "invalid_email");
        public static UserError InvalidPasswordError => new UserError("Password is incorrect", "invalid_password");
    }

}
