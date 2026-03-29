using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.User
{
    public class LoggingUserCommand
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
