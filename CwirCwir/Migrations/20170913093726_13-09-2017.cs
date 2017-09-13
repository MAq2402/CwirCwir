using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CwirCwir.Migrations
{
    public partial class _13092017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Sharings");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Responses");

            migrationBuilder.AddColumn<int>(
                name: "ResponseId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SharingId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ResponseId",
                table: "Likes",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SharingId",
                table: "Likes",
                column: "SharingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Responses_ResponseId",
                table: "Likes",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Sharings_SharingId",
                table: "Likes",
                column: "SharingId",
                principalTable: "Sharings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Responses_ResponseId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Sharings_SharingId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_ResponseId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_SharingId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ResponseId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "SharingId",
                table: "Likes");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Sharings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Responses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
