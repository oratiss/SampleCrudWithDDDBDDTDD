using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Financial");

            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "Financial",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    CreateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVoForBankAccount",
                schema: "Financial",
                columns: table => new
                {
                    BankAccountId = table.Column<long>(type: "bigint", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVoForBankAccount", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_CustomerVoForBankAccount_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "Financial",
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountVoForCustomer",
                schema: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountVoForCustomer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_BankAccountVoForCustomer_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCreatedEvent",
                schema: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true),
                    IsCallbackCompleted = table.Column<bool>(type: "bit", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCreatedEvent", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_CustomerCreatedEvent_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountVoForCustomer",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerCreatedEvent",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerVoForBankAccount",
                schema: "Financial");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "Financial");
        }
    }
}
