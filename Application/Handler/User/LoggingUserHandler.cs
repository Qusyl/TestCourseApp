using Application.Command.User;
using Application.Interface;
using Domain;


namespace Application.Handler.User
{
    public class LoggingUserHandler 
    {
        private readonly IUserRepository userRepository;

        private readonly IPasswordHasher _hasher;

        public LoggingUserHandler(IUserRepository userRepository, IPasswordHasher hasher)
        {
            this.userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<Result<Guid, ApplicationError>> Handle(LoggingUserCommand command)
        {

            
            var user = await userRepository.GetUserByEmail(command.Email);

            if (user == null || !_hasher.Verify(command.Password, user.HashPassword)) return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidCredentials);

            return Result<Guid, ApplicationError>.Success(user.Id);
        }
    }
}
