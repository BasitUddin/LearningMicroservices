using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class usermanagement2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "AspNetIdentity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactId",
                schema: "AspNetIdentity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletionFromApp",
                schema: "AspNetIdentity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DriverStatus",
                schema: "AspNetIdentity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                schema: "AspNetIdentity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "QId",
                schema: "AspNetIdentity",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "AspNetIdentity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                schema: "AspNetIdentity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "DeletionFromApp",
                schema: "AspNetIdentity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DriverStatus",
                schema: "AspNetIdentity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                schema: "AspNetIdentity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QId",
                schema: "AspNetIdentity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
