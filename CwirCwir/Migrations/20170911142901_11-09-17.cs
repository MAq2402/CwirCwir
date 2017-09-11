using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CwirCwir.Migrations
{
    public partial class _110917 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Likes_LikeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LikeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Likes");

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LikeId",
                table: "AspNetUsers",
                column: "LikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Likes_LikeId",
                table: "AspNetUsers",
                column: "LikeId",
                principalTable: "Likes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
