using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Communication.Responses
{
    public class ResponseShortCompanyJson
    {
        public string Id { get; set; } = string.Empty; //Id Base
        public string CompanyName { get; set; } = string.Empty;//Razão Social
        public string Cnpj { get; set; } = string.Empty;//CNPJ
        public string Phone { get; set; } = string.Empty;//Telefone
        public string City { get; set; } = string.Empty; // Cidade
        public string State { get; set; } = string.Empty; //UF
    }
}
