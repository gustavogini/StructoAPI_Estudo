using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Communication.Requests
{
    public class RequestRegisterUserJson
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public class Build : RequestRegisterUserJson //builder para facilitar a criação do objeto na hora de usar no código de teste
        {
        }
    }
}
