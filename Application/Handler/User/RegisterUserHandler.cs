using Domain.Aggregate.User;
using Application.Command.User;
using Application.Interface;
using Domain;

namespace Application.Handler.User
{
    public class RegisterUserHandler : IHandler
    {
        private readonly IUserRepository _userRepos;

        private readonly IPasswordHasher _hasher;
        private readonly IUnitOfWork _unitOfWork;
        public async Task<Result<Guid, ApplicationError>> Handle(IEntityCommand command)
        {
           var comCast = command as RegisterUserCommand;

            if (comCast == null) 
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.CommandCastError);
            }
            var hash = _hasher.Hash(comCast.Password);

            var userRes = Domain.Aggregate.User.User.Create(comCast.Email, hash);
            if (!userRes.IsSuccess)
            {
                return Result<Guid,ApplicationError>.Failure(ApplicationError.InvalidUserData);
            }
            var user = userRes.Value;
            await _userRepos.AddAsync(user);
            var save = _unitOfWork.SaveChangesAsync();
            return Result<Guid, ApplicationError>.Success(user.Id); 
        }
    }
}
