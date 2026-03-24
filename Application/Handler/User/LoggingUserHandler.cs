using Application.Command.User;
using Application.Interface;
using Domain;


namespace Application.Handler.User
{
    public class LoggingUserHandler : IHandler
    {
        private readonly IUserRepository userRepository;

        private readonly IPasswordHasher _hasher;

        public LoggingUserHandler(IUserRepository userRepository, IPasswordHasher hasher)
        {
            this.userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<Result<Guid, ApplicationError>> Handle(IEntityCommand command)
        {

            var castCommand = command as LoggingUserCommand;
            if (castCommand == null)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.CommandCastError);
            }
            var user = await userRepository.GetUserByEmail(castCommand.Email);

            if (user == null || !_hasher.Verify(castCommand.Password, user.HashPassword)) return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidCredentials);

            return Result<Guid, ApplicationError>.Success(user.Id);
        }
    }
}
