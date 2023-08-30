using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_Id типа товара",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Наличие",
                table: "Products",
                newName: "Наличие, шт.");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id типа товара",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<decimal>(
                name: "Цена, руб.",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_Id типа товара",
                table: "Products",
                column: "Id типа товара",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_Id типа товара",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Цена, руб.",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Наличие, шт.",
                table: "Products",
                newName: "Наличие");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id типа товара",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_Id типа товара",
                table: "Products",
                column: "Id типа товара",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
