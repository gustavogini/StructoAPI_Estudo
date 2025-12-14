using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Communication.Requests
{
    public class RequestChangePasswordJson
    {
        public string Password { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
