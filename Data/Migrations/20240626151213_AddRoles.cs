using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCKontorExpert.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Admin role
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                           VALUES (NEWID(), 'Admin', 'ADMIN', NEWID())");

            // Insert Customer role
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                           VALUES (NEWID(), 'Customer', 'CUSTOMER', NEWID())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete Admin role
            migrationBuilder.Sql(@"DELETE FROM AspNetRoles WHERE Name = 'Admin'");

            // Delete Customer role
            migrationBuilder.Sql(@"DELETE FROM AspNetRoles WHERE Name = 'Customer'");
        }

    }
}
