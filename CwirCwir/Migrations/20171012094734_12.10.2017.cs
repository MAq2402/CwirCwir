using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CwirCwir.Migrations
{
    public partial class _12102017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Sharings_SharingId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Sharings_SharingId",
                table: "Responses");

            migrationBuilder.DropTable(
                name: "Sharings");

            migrationBuilder.DropIndex(
                name: "IX_Responses_SharingId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Likes_SharingId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "SharingId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "SharingId",
                table: "Likes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SharingId",
                table: "Responses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SharingId",
                table: "Likes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sharings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: false),
                    SharingDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sharings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sharings_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sharings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responses_SharingId",
                table: "Responses",
                column: "SharingId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SharingId",
                table: "Likes",
                column: "SharingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sharings_PostId",
                table: "Sharings",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Sharings_UserId",
                table: "Sharings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Sharings_SharingId",
                table: "Likes",
                column: "SharingId",
                principalTable: "Sharings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Sharings_SharingId",
                table: "Responses",
                column: "SharingId",
                principalTable: "Sharings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
