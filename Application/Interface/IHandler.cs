using Domain;
using System;


namespace Application.Interface
{
    public interface IHandler
    {
        Task<Result<Guid, ApplicationError>> Handle(IEntityCommand command);
    }
}
