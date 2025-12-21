using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Communication.Responses
{
    public class ResponseEmployeeJson
    {
        public string Id { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty; //Cargo
        public string EmployeePhone { get; set; } = string.Empty;//Telefone
    }
}
