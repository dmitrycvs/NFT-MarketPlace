using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateHistoryLog_InventoryUIpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NftItemId",
                table: "Inventories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_NftItemId",
                table: "Inventories",
                column: "NftItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_NftItems_NftItemId",
                table: "Inventories",
                column: "NftItemId",
                principalTable: "NftItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_NftItems_NftItemId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_NftItemId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "NftItemId",
                table: "Inventories");
        }
    }
}
