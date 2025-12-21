using FluentMigrator;
using Structo.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Structo.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_COMPANIES, "Create table to save the Customers or End Users information")]
    public class Version0000002 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Companies")
                .WithColumn("CompanyIdentifier").AsGuid().NotNullable() //ok
                .WithColumn("CompanyName").AsString(255).NotNullable()
                .WithColumn("Cnpj").AsString(255).NotNullable()
                .WithColumn("CompanyStartDate").AsString(2000).NotNullable()
                .WithColumn("LegalNature").AsString(2000).NotNullable()
                .WithColumn("Size").AsString(2000).NotNullable()
                .WithColumn("RegistrationStatus").AsString(2000).NotNullable()
                .WithColumn("RegistrationSituationDate").AsString(2000).NotNullable()
                .WithColumn("MainCnae").AsString(2000).NotNullable()
                .WithColumn("TaxRegime").AsString(2000).NotNullable()
                .WithColumn("StateRegistration").AsString(2000).NotNullable()
                .WithColumn("Phone").AsString(50).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Address").AsString(255).NotNullable()
                .WithColumn("Number").AsInt32().NotNullable()
                .WithColumn("AddressComplement").AsString(255).NotNullable()
                .WithColumn("District").AsString(255).NotNullable()
                .WithColumn("City").AsString(255).NotNullable()
                .WithColumn("State").AsString(50).NotNullable()
                .WithColumn("ZipCode").AsString(50).NotNullable();


        }
    }
}
