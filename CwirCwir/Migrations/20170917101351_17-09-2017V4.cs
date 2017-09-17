using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CwirCwir.Migrations
{
    public partial class _17092017V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "ResponseLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseLikes_PostId",
                table: "ResponseLikes",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseLikes_Posts_PostId",
                table: "ResponseLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseLikes_Posts_PostId",
                table: "ResponseLikes");

            migrationBuilder.DropIndex(
                name: "IX_ResponseLikes_PostId",
                table: "ResponseLikes");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "ResponseLikes");
        }
    }
}
