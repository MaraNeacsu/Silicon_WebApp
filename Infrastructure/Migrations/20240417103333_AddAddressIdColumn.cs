using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdressId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "AspNetUsers",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AdressId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AddressId");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine_1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "AspNetUsers",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AdressId");

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdressLine_1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdressLine_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AdressId",
                table: "AspNetUsers",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id");
        }
    }
}
