using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class itsps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookTable",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    BookImage = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    DiscountPrice = table.Column<float>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Rating = table.Column<float>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTable", x => x.BookId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTable");
        }
    }
}
