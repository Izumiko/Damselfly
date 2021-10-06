﻿using Damselfly.Core.Utils;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Damselfly.Core.Migrations
{
    public partial class RemoveHashColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Logging.Log("Migrating hashes to new table...");
            // Copy data
            const string sql = @"INSERT INTO Hashes (ImageId, MD5ImageHash)
                                SELECT i.ImageId, i.Hash FROM ImageMetaData i
                                WHERE i.ImageID not in
                                    (SELECT imageid FROM hashes);";
            migrationBuilder.Sql(sql);
            Logging.Log("Hash migration complete. Dropping column...");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "ImageMetaData");

            Logging.Log("Column dropped.");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "82b60d90-9f24-4a12-a246-bc6e1f529102");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "adc5f5b1-cc1c-4827-abe8-0c7c5fdbbc68");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3d168dfe-134e-4802-a6c0-923f450b99c9");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "ImageMetaData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fa3a79e1-2a02-4fde-b26a-1498bd911931");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f9bb3be3-1fb4-4a1a-bbcb-1d71ca3e198c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e2f1656c-d1eb-4db3-b3a0-efffab6ff171");
        }
    }
}
