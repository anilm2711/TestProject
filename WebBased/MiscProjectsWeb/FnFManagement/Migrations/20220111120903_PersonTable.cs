using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FnFManagement.Migrations
{
    public partial class PersonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameFirst = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NameMiddle = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NameLast = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AdhaarNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
