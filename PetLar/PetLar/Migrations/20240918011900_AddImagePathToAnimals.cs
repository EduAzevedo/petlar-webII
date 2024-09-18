﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetLar.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "ImagePath",
            table: "Animals",
            nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "ImagePath",
            table: "Animals");
        }
    }
}
