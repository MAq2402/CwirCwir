using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CwirCwir.Migrations
{
    public partial class _17092017V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseLike_Responses_ResponseId",
                table: "ResponseLike");

            migrationBuilder.DropForeignKey(
                name: "FK_ResponseLike_AspNetUsers_UserId",
                table: "ResponseLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResponseLike",
                table: "ResponseLike");

            migrationBuilder.RenameTable(
                name: "ResponseLike",
                newName: "ResponseLikes");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseLike_UserId",
                table: "ResponseLikes",
                newName: "IX_ResponseLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseLike_ResponseId",
                table: "ResponseLikes",
                newName: "IX_ResponseLikes_ResponseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResponseLikes",
                table: "ResponseLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseLikes_Responses_ResponseId",
                table: "ResponseLikes",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseLikes_AspNetUsers_UserId",
                table: "ResponseLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseLikes_Responses_ResponseId",
                table: "ResponseLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_ResponseLikes_AspNetUsers_UserId",
                table: "ResponseLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResponseLikes",
                table: "ResponseLikes");

            migrationBuilder.RenameTable(
                name: "ResponseLikes",
                newName: "ResponseLike");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseLikes_UserId",
                table: "ResponseLike",
                newName: "IX_ResponseLike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseLikes_ResponseId",
                table: "ResponseLike",
                newName: "IX_ResponseLike_ResponseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResponseLike",
                table: "ResponseLike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseLike_Responses_ResponseId",
                table: "ResponseLike",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseLike_AspNetUsers_UserId",
                table: "ResponseLike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
