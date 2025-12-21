namespace Structo.Communication.Responses
{
    public class ResponseRegisteredCompanyJson //essa classe detalha a estrutura de dados para respostas enviadas pela API relacionadas a empresas cadastradas
    {
        public string Id { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

    }
}
