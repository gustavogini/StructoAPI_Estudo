using Structo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Communication.Responses
{
    public class ResponseCompanyJson
    {
        public string Id { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;//Razão Social
        public string Cnpj { get; set; } = string.Empty;//CNPJ
        public string CompanyFantasyName { get; set; } = string.Empty; //Nome Fantasia
        public DateTime? CompanyStartDate { get; set; } = null; //DataAbertura
        public string LegalNature { get; set; } = string.Empty; //Natureza Jurídica
        public string Size { get; set; } = string.Empty; //Porte
        public string RegistrationStatus { get; set; } = string.Empty; //Situação Cadastral
        public DateTime? RegistrationSituationDate { get; set; } = null; //DataSituacaoCadastral
        public string MainCnae { get; set; } = string.Empty; //CNAE Principal
        public string TaxRegime { get; set; } = string.Empty; //RegimeTributario
        public string StateRegistration { get; set; } = string.Empty; //InscricaoEstadual
        public string Phone { get; set; } = string.Empty;//Telefone
        public string Email { get; set; } = string.Empty;//Email
        public string Address { get; set; } = string.Empty; //Logradouro
        public int Number { get; set; } = 0; //Número
        public string AddressComplement { get; set; } = string.Empty; //Complemento
        public string District { get; set; } = string.Empty; // Bairro
        public string City { get; set; } = string.Empty; // Cidade
        public string State { get; set; } = string.Empty; //UF
        public string ZipCode { get; set; } = string.Empty; //CEP
        public IList<Employee> Employees { get; set; } = [];
    }
}
