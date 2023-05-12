using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepositBankingWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class initSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsStandardTermDeposit = table.Column<bool>(type: "bit", nullable: false),
                    IsInterestFixed = table.Column<bool>(type: "bit", nullable: false),
                    TimeDeposit = table.Column<bool>(type: "bit", nullable: false),
                    OverdraftPossability = table.Column<bool>(type: "bit", nullable: false),
                    CreditPossability = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyCompounding = table.Column<bool>(type: "bit", nullable: false),
                    TerminalCapitalization = table.Column<bool>(type: "bit", nullable: false),
                    ValidForClientsOnly = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    InterestPaymentType = table.Column<int>(type: "int", nullable: false),
                    OwnershipType = table.Column<int>(type: "int", nullable: false),
                    EffectiveAnnualInterestRate = table.Column<float>(type: "real", nullable: false),
                    WebLinkToOffer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionOfNegotiatedInterestRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinSum = table.Column<float>(type: "real", nullable: false),
                    MinSumDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSum = table.Column<float>(type: "real", nullable: false),
                    MaxSumDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposits_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Deposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "Deposits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DepositId",
                table: "Comments",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_ApplicationUserId",
                table: "Deposits",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
