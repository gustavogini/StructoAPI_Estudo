using Structo.Domain.Entities;

namespace Structo.Communication.Requests
{
    public class RequestFilterCompanyJson
    {
        public string CompanyName { get; set; } = string.Empty;//Razão Social
        public string Cnpj { get; set; } = string.Empty;//CNPJ
        public string City { get; set; } = string.Empty; // Cidade

    }
}
