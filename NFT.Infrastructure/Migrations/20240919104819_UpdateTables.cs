using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvgDealPrice",
                table: "NftItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                table: "NftItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FloorPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Volume = table.Column<decimal>(type: "numeric", nullable: false),
                    Supply = table.Column<int>(type: "integer", nullable: false),
                    NumberOfSale = table.Column<int>(type: "integer", nullable: false),
                    MarketCapital = table.Column<decimal>(type: "numeric", nullable: false),
                    SocialLink = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "historyLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserSellerId = table.Column<Guid>(type: "uuid", nullable: true),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserBuyerId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DealPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    NftItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historyLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_historyLogs_NftItems_NftItemId",
                        column: x => x.NftItemId,
                        principalTable: "NftItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historyLogs_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historyLogs_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NftItems_CollectionId",
                table: "NftItems",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_historyLogs_BuyerId",
                table: "historyLogs",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_historyLogs_NftItemId",
                table: "historyLogs",
                column: "NftItemId");

            migrationBuilder.CreateIndex(
                name: "IX_historyLogs_SellerId",
                table: "historyLogs",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_NftItems_Collections_CollectionId",
                table: "NftItems",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NftItems_Collections_CollectionId",
                table: "NftItems");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "historyLogs");

            migrationBuilder.DropIndex(
                name: "IX_NftItems_CollectionId",
                table: "NftItems");

            migrationBuilder.DropColumn(
                name: "AvgDealPrice",
                table: "NftItems");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "NftItems");
        }
    }
}
