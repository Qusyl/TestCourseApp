using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class AuthResult
    {
        public string Token { get; }

        public AuthResult(string token)
        {
            Token = token;
        }
    }
}
