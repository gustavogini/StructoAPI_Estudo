using FluentMigrator;

namespace Structo.Infrastructure.Migrations.Versions
{

    [Migration(DatabaseVersions.TABLE_USER, "Create table to save the user information")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("Active").AsBoolean().NotNullable()
                .WithColumn("Username").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Password").AsString(255).NotNullable();
                
        }
    }
}
