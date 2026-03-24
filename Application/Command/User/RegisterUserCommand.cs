

using Application.Interface;

namespace Application.Command.User
{
    public class RegisterUserCommand : IEntityCommand
    {
        public string Email { get;  }

        public string Password { get; }
    }
}
