using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TakeHomeAssignmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Packaging_Hierarchies",
                columns: new[] { "ChildPackagingId", "ParentPackagingId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 3 },
                    { 7, 3 },
                    { 8, 4 },
                    { 9, 5 },
                    { 10, 6 },
                    { 11, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                table: "Packaging_Hierarchies",
                keyColumns: new[] { "ChildPackagingId", "ParentPackagingId" },
                keyValues: new object[] { 11, 7 });
        }
    }
}
