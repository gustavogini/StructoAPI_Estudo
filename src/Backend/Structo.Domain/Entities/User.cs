using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
