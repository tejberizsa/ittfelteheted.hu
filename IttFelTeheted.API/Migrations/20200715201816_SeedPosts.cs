using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IttFelTeheted.API.Migrations
{
    public partial class SeedPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Migrations\PostSeed.sql");
            var sqlCommand = File.ReadAllText(sqlFile);
            migrationBuilder.Sql(sqlCommand);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete FROM public.\"Posts\"");
        }
    }
}
