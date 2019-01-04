using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicInside.DataAccessLayer.Migrations
{
    public partial class addslidetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slide",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Source = table.Column<string>(nullable: true),
                    ValidityFrom = table.Column<DateTime>(nullable: false),
                    ValidityTo = table.Column<DateTime>(nullable: false),
                    Section = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    AltText = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slide", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slide");
        }
    }
}
