using Domain.Aggregate.User;
using Application.Command.User;
using Application.Interface;
using Domain;

namespace Application.Handler.User
{
    public class RegisterUserHandler
    {
        private readonly IUserRepository _userRepos;

        private readonly IPasswordHasher _hasher;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserHandler(IUserRepository userRepos,IPasswordHasher hasher, IUnitOfWork unit)
        {
            _userRepos = userRepos;
            _hasher = hasher;
            _unitOfWork = unit;
        }
        public async Task<Result<Guid, ApplicationError>> Handle(RegisterUserCommand command)
        {
          
            var hash = _hasher.Hash(command.Password);

            var userRes = Domain.Aggregate.User.User.Create(command.Email, hash);
            if (!userRes.IsSuccess)
            {
                return Result<Guid,ApplicationError>.Failure(ApplicationError.InvalidUserData);
            }
            var user = userRes.Value;
            await _userRepos.AddAsync(user);
            var save = await _unitOfWork.SaveChangesAsync();
            return Result<Guid, ApplicationError>.Success(user.Id); 
        }
    }
}
