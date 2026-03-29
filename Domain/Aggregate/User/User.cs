

namespace Domain.Aggregate.User
{
    public class User
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string HashPassword { get; private set; }

        public UserRole Role { get; private set; }

        private User() {}

        public static Result<User,UserError> Create(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || !email.Contains('@') || email.Length < 5)
            {
                return Result<User, UserError>.Failure(UserError.InvalidEmailError);
            }
            if (string.IsNullOrEmpty(password))
            {
                return Result<User, UserError>.Failure(UserError.InvalidPasswordError);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                HashPassword = password,
                Role = UserRole.User
            };
            return Result<User, UserError>.Success(user);
        }
    }
}
