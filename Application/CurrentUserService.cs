using Application.Interface;
using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _htppAccess;

        public CurrentUserService(IHttpContextAccessor htppAccess)
        {
            _htppAccess = htppAccess;
        }
        public Result<Guid, ApplicationError> GetUserId()
        {
            var user = _htppAccess.HttpContext?.User;
            var id = user?.FindFirst("id")?.Value;

            if(id == null)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.NotAuthorized);
            }

            return Result<Guid, ApplicationError>.Success(Guid.Parse(id));
        }
    }
}
