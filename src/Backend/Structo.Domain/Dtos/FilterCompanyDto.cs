using Structo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Dtos
{
    public record FilterCompanyDto //aqui é inserido o que será recebido para filtro. //essa classe foi criada para?
    {
        public string? CompanyFantasyName { get; init; } //Nome Fantasia
        public string? CompanyName { get; init; }//Razão Social
        public string Cnpj { get; init; } = string.Empty;//CNPJ
        public string Phone { get; init; } = string.Empty;//Telefone
        public string City { get; init; } = string.Empty; // Cidade

    }
}
