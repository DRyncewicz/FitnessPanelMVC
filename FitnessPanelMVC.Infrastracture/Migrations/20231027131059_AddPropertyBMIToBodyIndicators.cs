﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPanelMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyBMIToBodyIndicators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BMIs");

            migrationBuilder.CreateTable(
                name: "BodyIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PAL = table.Column<int>(type: "int", nullable: false),
                    PPM = table.Column<double>(type: "float", nullable: false),
                    CPM = table.Column<double>(type: "float", nullable: false),
                    WHtR = table.Column<double>(type: "float", nullable: false),
                    NMC = table.Column<double>(type: "float", nullable: false),
                    HipCircumference = table.Column<int>(type: "int", nullable: false),
                    AbdominalCircumference = table.Column<int>(type: "int", nullable: false),
                    BAI = table.Column<double>(type: "float", nullable: false),
                    BeW = table.Column<double>(type: "float", nullable: false),
                    BMI = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyIndicators", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyIndicators");

            migrationBuilder.CreateTable(
                name: "BMIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbdominalCircumference = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    BAI = table.Column<double>(type: "float", nullable: false),
                    BeW = table.Column<double>(type: "float", nullable: false),
                    CPM = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    HipCircumference = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    NMC = table.Column<double>(type: "float", nullable: false),
                    PAL = table.Column<int>(type: "int", nullable: false),
                    PPM = table.Column<double>(type: "float", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WHtR = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BMIs", x => x.Id);
                });
        }
    }
}
