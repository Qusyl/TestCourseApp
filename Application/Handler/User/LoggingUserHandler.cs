using Application.Command.User;
using Application.Dto;
using Application.Interface;
using Domain;


namespace Application.Handler.User
{
    public class LoggingUserHandler 
    {
        private readonly IUserRepository userRepository;

        private readonly IPasswordHasher _hasher;

        private readonly ITokenService _tokenService;

        public LoggingUserHandler(IUserRepository userRepository, IPasswordHasher hasher, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthResult, ApplicationError>> Handle(LoggingUserCommand command)
        {
            var user = await userRepository.GetUserByEmail(command.Email);

            if (user == null || !_hasher.Verify(command.Password, user.HashPassword)) return Result<AuthResult, ApplicationError>.Failure(ApplicationError.InvalidCredentials);

            var token = _tokenService.Generate(user.Id);

            return Result<AuthResult, ApplicationError>.Success(new AuthResult(token));
        }
    }
}
