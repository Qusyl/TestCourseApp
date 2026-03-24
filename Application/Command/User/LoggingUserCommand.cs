using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.User
{
    public class LoggingUserCommand : IEntityCommand
    {
        public string Email { get; }

        public string Password { get; }

    }
}
