using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPlatforn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BidDbSetAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Auctions_AuctionId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Users_BidderUserId",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                table: "Bid");

            migrationBuilder.RenameTable(
                name: "Bid",
                newName: "Bids");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_BidderUserId",
                table: "Bids",
                newName: "IX_Bids_BidderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AuctionId",
                table: "Bids",
                newName: "IX_Bids_AuctionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_BidderUserId",
                table: "Bids",
                column: "BidderUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_BidderUserId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "Bid");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_BidderUserId",
                table: "Bid",
                newName: "IX_Bid_BidderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionId",
                table: "Bid",
                newName: "IX_Bid_AuctionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                table: "Bid",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Auctions_AuctionId",
                table: "Bid",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Users_BidderUserId",
                table: "Bid",
                column: "BidderUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
