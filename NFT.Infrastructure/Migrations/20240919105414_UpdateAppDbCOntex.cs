using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppDbCOntex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_historyLogs_NftItems_NftItemId",
                table: "historyLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_historyLogs_Users_BuyerId",
                table: "historyLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_historyLogs_Users_SellerId",
                table: "historyLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_historyLogs",
                table: "historyLogs");

            migrationBuilder.RenameTable(
                name: "historyLogs",
                newName: "HistoryLogs");

            migrationBuilder.RenameIndex(
                name: "IX_historyLogs_SellerId",
                table: "HistoryLogs",
                newName: "IX_HistoryLogs_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_historyLogs_NftItemId",
                table: "HistoryLogs",
                newName: "IX_HistoryLogs_NftItemId");

            migrationBuilder.RenameIndex(
                name: "IX_historyLogs_BuyerId",
                table: "HistoryLogs",
                newName: "IX_HistoryLogs_BuyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryLogs",
                table: "HistoryLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_NftItems_NftItemId",
                table: "HistoryLogs",
                column: "NftItemId",
                principalTable: "NftItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_Users_BuyerId",
                table: "HistoryLogs",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_Users_SellerId",
                table: "HistoryLogs",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_NftItems_NftItemId",
                table: "HistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_Users_BuyerId",
                table: "HistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_Users_SellerId",
                table: "HistoryLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryLogs",
                table: "HistoryLogs");

            migrationBuilder.RenameTable(
                name: "HistoryLogs",
                newName: "historyLogs");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryLogs_SellerId",
                table: "historyLogs",
                newName: "IX_historyLogs_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryLogs_NftItemId",
                table: "historyLogs",
                newName: "IX_historyLogs_NftItemId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryLogs_BuyerId",
                table: "historyLogs",
                newName: "IX_historyLogs_BuyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_historyLogs",
                table: "historyLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_historyLogs_NftItems_NftItemId",
                table: "historyLogs",
                column: "NftItemId",
                principalTable: "NftItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_historyLogs_Users_BuyerId",
                table: "historyLogs",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_historyLogs_Users_SellerId",
                table: "historyLogs",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
