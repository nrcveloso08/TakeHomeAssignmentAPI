using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TakeHomeAssignmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packagings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packagings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwoFactorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorRecoveryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packaging_Hierarchies",
                columns: table => new
                {
                    ParentPackagingId = table.Column<int>(type: "int", nullable: false),
                    ChildPackagingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packaging_Hierarchies", x => new { x.ParentPackagingId, x.ChildPackagingId });
                    table.ForeignKey(
                        name: "FK_Packaging_Hierarchies_Packagings_ChildPackagingId",
                        column: x => x.ChildPackagingId,
                        principalTable: "Packagings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Packaging_Hierarchies_Packagings_ParentPackagingId",
                        column: x => x.ParentPackagingId,
                        principalTable: "Packagings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackagingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Packagings_PackagingId",
                        column: x => x.PackagingId,
                        principalTable: "Packagings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Packagings",
                columns: new[] { "Id", "Dimensions", "Material", "Type", "Weight" },
                values: new object[,]
                {
                    { 1, "1x1x1 cm", "Material 1", "Bag", 0.5 },
                    { 2, "2x2x2 cm", "Material 2", "Box", 1.0 },
                    { 3, "3x3x3 cm", "Material 3", "Bag", 1.5 },
                    { 4, "4x4x4 cm", "Material 4", "Box", 2.0 },
                    { 5, "5x5x5 cm", "Material 5", "Bag", 2.5 },
                    { 6, "6x6x6 cm", "Material 6", "Box", 3.0 },
                    { 7, "7x7x7 cm", "Material 7", "Bag", 3.5 },
                    { 8, "8x8x8 cm", "Material 8", "Box", 4.0 },
                    { 9, "9x9x9 cm", "Material 9", "Bag", 4.5 },
                    { 10, "10x10x10 cm", "Material 10", "Box", 5.0 },
                    { 11, "11x11x11 cm", "Material 11", "Bag", 5.5 },
                    { 12, "12x12x12 cm", "Material 12", "Box", 6.0 },
                    { 13, "13x13x13 cm", "Material 13", "Bag", 6.5 },
                    { 14, "14x14x14 cm", "Material 14", "Box", 7.0 },
                    { 15, "15x15x15 cm", "Material 15", "Bag", 7.5 },
                    { 16, "16x16x16 cm", "Material 16", "Box", 8.0 },
                    { 17, "17x17x17 cm", "Material 17", "Bag", 8.5 },
                    { 18, "18x18x18 cm", "Material 18", "Box", 9.0 },
                    { 19, "19x19x19 cm", "Material 19", "Bag", 9.5 },
                    { 20, "20x20x20 cm", "Material 20", "Box", 10.0 },
                    { 21, "21x21x21 cm", "Material 21", "Bag", 10.5 },
                    { 22, "22x22x22 cm", "Material 22", "Box", 11.0 },
                    { 23, "23x23x23 cm", "Material 23", "Bag", 11.5 },
                    { 24, "24x24x24 cm", "Material 24", "Box", 12.0 },
                    { 25, "25x25x25 cm", "Material 25", "Bag", 12.5 },
                    { 26, "26x26x26 cm", "Material 26", "Box", 13.0 },
                    { 27, "27x27x27 cm", "Material 27", "Bag", 13.5 },
                    { 28, "28x28x28 cm", "Material 28", "Box", 14.0 },
                    { 29, "29x29x29 cm", "Material 29", "Bag", 14.5 },
                    { 30, "30x30x30 cm", "Material 30", "Box", 15.0 },
                    { 31, "31x31x31 cm", "Material 31", "Bag", 15.5 },
                    { 32, "32x32x32 cm", "Material 32", "Box", 16.0 },
                    { 33, "33x33x33 cm", "Material 33", "Bag", 16.5 },
                    { 34, "34x34x34 cm", "Material 34", "Box", 17.0 },
                    { 35, "35x35x35 cm", "Material 35", "Bag", 17.5 },
                    { 36, "36x36x36 cm", "Material 36", "Box", 18.0 },
                    { 37, "37x37x37 cm", "Material 37", "Bag", 18.5 },
                    { 38, "38x38x38 cm", "Material 38", "Box", 19.0 },
                    { 39, "39x39x39 cm", "Material 39", "Bag", 19.5 },
                    { 40, "40x40x40 cm", "Material 40", "Box", 20.0 },
                    { 41, "41x41x41 cm", "Material 41", "Bag", 20.5 },
                    { 42, "42x42x42 cm", "Material 42", "Box", 21.0 },
                    { 43, "43x43x43 cm", "Material 43", "Bag", 21.5 },
                    { 44, "44x44x44 cm", "Material 44", "Box", 22.0 },
                    { 45, "45x45x45 cm", "Material 45", "Bag", 22.5 },
                    { 46, "46x46x46 cm", "Material 46", "Box", 23.0 },
                    { 47, "47x47x47 cm", "Material 47", "Bag", 23.5 },
                    { 48, "48x48x48 cm", "Material 48", "Box", 24.0 },
                    { 49, "49x49x49 cm", "Material 49", "Bag", 24.5 },
                    { 50, "50x50x50 cm", "Material 50", "Box", 25.0 },
                    { 51, "51x51x51 cm", "Material 51", "Bag", 25.5 },
                    { 52, "52x52x52 cm", "Material 52", "Box", 26.0 },
                    { 53, "53x53x53 cm", "Material 53", "Bag", 26.5 },
                    { 54, "54x54x54 cm", "Material 54", "Box", 27.0 },
                    { 55, "55x55x55 cm", "Material 55", "Bag", 27.5 },
                    { 56, "56x56x56 cm", "Material 56", "Box", 28.0 },
                    { 57, "57x57x57 cm", "Material 57", "Bag", 28.5 },
                    { 58, "58x58x58 cm", "Material 58", "Box", 29.0 },
                    { 59, "59x59x59 cm", "Material 59", "Bag", 29.5 },
                    { 60, "60x60x60 cm", "Material 60", "Box", 30.0 },
                    { 61, "61x61x61 cm", "Material 61", "Bag", 30.5 },
                    { 62, "62x62x62 cm", "Material 62", "Box", 31.0 },
                    { 63, "63x63x63 cm", "Material 63", "Bag", 31.5 },
                    { 64, "64x64x64 cm", "Material 64", "Box", 32.0 },
                    { 65, "65x65x65 cm", "Material 65", "Bag", 32.5 },
                    { 66, "66x66x66 cm", "Material 66", "Box", 33.0 },
                    { 67, "67x67x67 cm", "Material 67", "Bag", 33.5 },
                    { 68, "68x68x68 cm", "Material 68", "Box", 34.0 },
                    { 69, "69x69x69 cm", "Material 69", "Bag", 34.5 },
                    { 70, "70x70x70 cm", "Material 70", "Box", 35.0 },
                    { 71, "71x71x71 cm", "Material 71", "Bag", 35.5 },
                    { 72, "72x72x72 cm", "Material 72", "Box", 36.0 },
                    { 73, "73x73x73 cm", "Material 73", "Bag", 36.5 },
                    { 74, "74x74x74 cm", "Material 74", "Box", 37.0 },
                    { 75, "75x75x75 cm", "Material 75", "Bag", 37.5 },
                    { 76, "76x76x76 cm", "Material 76", "Box", 38.0 },
                    { 77, "77x77x77 cm", "Material 77", "Bag", 38.5 },
                    { 78, "78x78x78 cm", "Material 78", "Box", 39.0 },
                    { 79, "79x79x79 cm", "Material 79", "Bag", 39.5 },
                    { 80, "80x80x80 cm", "Material 80", "Box", 40.0 },
                    { 81, "81x81x81 cm", "Material 81", "Bag", 40.5 },
                    { 82, "82x82x82 cm", "Material 82", "Box", 41.0 },
                    { 83, "83x83x83 cm", "Material 83", "Bag", 41.5 },
                    { 84, "84x84x84 cm", "Material 84", "Box", 42.0 },
                    { 85, "85x85x85 cm", "Material 85", "Bag", 42.5 },
                    { 86, "86x86x86 cm", "Material 86", "Box", 43.0 },
                    { 87, "87x87x87 cm", "Material 87", "Bag", 43.5 },
                    { 88, "88x88x88 cm", "Material 88", "Box", 44.0 },
                    { 89, "89x89x89 cm", "Material 89", "Bag", 44.5 },
                    { 90, "90x90x90 cm", "Material 90", "Box", 45.0 },
                    { 91, "91x91x91 cm", "Material 91", "Bag", 45.5 },
                    { 92, "92x92x92 cm", "Material 92", "Box", 46.0 },
                    { 93, "93x93x93 cm", "Material 93", "Bag", 46.5 },
                    { 94, "94x94x94 cm", "Material 94", "Box", 47.0 },
                    { 95, "95x95x95 cm", "Material 95", "Bag", 47.5 },
                    { 96, "96x96x96 cm", "Material 96", "Box", 48.0 },
                    { 97, "97x97x97 cm", "Material 97", "Bag", 48.5 },
                    { 98, "98x98x98 cm", "Material 98", "Box", 49.0 },
                    { 99, "99x99x99 cm", "Material 99", "Bag", 49.5 },
                    { 100, "100x100x100 cm", "Material 100", "Box", 50.0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PackagingId", "Price" },
                values: new object[,]
                {
                    { 1, "Product 1", 2, 10.99m },
                    { 2, "Product 2", 3, 21.98m },
                    { 3, "Product 3", 4, 32.97m },
                    { 4, "Product 4", 5, 43.96m },
                    { 5, "Product 5", 6, 54.95m },
                    { 6, "Product 6", 7, 65.94m },
                    { 7, "Product 7", 8, 76.93m },
                    { 8, "Product 8", 9, 87.92m },
                    { 9, "Product 9", 10, 98.91m },
                    { 10, "Product 10", 11, 109.90m },
                    { 11, "Product 11", 12, 120.89m },
                    { 12, "Product 12", 13, 131.88m },
                    { 13, "Product 13", 14, 142.87m },
                    { 14, "Product 14", 15, 153.86m },
                    { 15, "Product 15", 16, 164.85m },
                    { 16, "Product 16", 17, 175.84m },
                    { 17, "Product 17", 18, 186.83m },
                    { 18, "Product 18", 19, 197.82m },
                    { 19, "Product 19", 20, 208.81m },
                    { 20, "Product 20", 21, 219.80m },
                    { 21, "Product 21", 22, 230.79m },
                    { 22, "Product 22", 23, 241.78m },
                    { 23, "Product 23", 24, 252.77m },
                    { 24, "Product 24", 25, 263.76m },
                    { 25, "Product 25", 26, 274.75m },
                    { 26, "Product 26", 27, 285.74m },
                    { 27, "Product 27", 28, 296.73m },
                    { 28, "Product 28", 29, 307.72m },
                    { 29, "Product 29", 30, 318.71m },
                    { 30, "Product 30", 31, 329.70m },
                    { 31, "Product 31", 32, 340.69m },
                    { 32, "Product 32", 33, 351.68m },
                    { 33, "Product 33", 34, 362.67m },
                    { 34, "Product 34", 35, 373.66m },
                    { 35, "Product 35", 36, 384.65m },
                    { 36, "Product 36", 37, 395.64m },
                    { 37, "Product 37", 38, 406.63m },
                    { 38, "Product 38", 39, 417.62m },
                    { 39, "Product 39", 40, 428.61m },
                    { 40, "Product 40", 41, 439.60m },
                    { 41, "Product 41", 42, 450.59m },
                    { 42, "Product 42", 43, 461.58m },
                    { 43, "Product 43", 44, 472.57m },
                    { 44, "Product 44", 45, 483.56m },
                    { 45, "Product 45", 46, 494.55m },
                    { 46, "Product 46", 47, 505.54m },
                    { 47, "Product 47", 48, 516.53m },
                    { 48, "Product 48", 49, 527.52m },
                    { 49, "Product 49", 50, 538.51m },
                    { 50, "Product 50", 51, 549.50m },
                    { 51, "Product 51", 52, 560.49m },
                    { 52, "Product 52", 53, 571.48m },
                    { 53, "Product 53", 54, 582.47m },
                    { 54, "Product 54", 55, 593.46m },
                    { 55, "Product 55", 56, 604.45m },
                    { 56, "Product 56", 57, 615.44m },
                    { 57, "Product 57", 58, 626.43m },
                    { 58, "Product 58", 59, 637.42m },
                    { 59, "Product 59", 60, 648.41m },
                    { 60, "Product 60", 61, 659.40m },
                    { 61, "Product 61", 62, 670.39m },
                    { 62, "Product 62", 63, 681.38m },
                    { 63, "Product 63", 64, 692.37m },
                    { 64, "Product 64", 65, 703.36m },
                    { 65, "Product 65", 66, 714.35m },
                    { 66, "Product 66", 67, 725.34m },
                    { 67, "Product 67", 68, 736.33m },
                    { 68, "Product 68", 69, 747.32m },
                    { 69, "Product 69", 70, 758.31m },
                    { 70, "Product 70", 71, 769.30m },
                    { 71, "Product 71", 72, 780.29m },
                    { 72, "Product 72", 73, 791.28m },
                    { 73, "Product 73", 74, 802.27m },
                    { 74, "Product 74", 75, 813.26m },
                    { 75, "Product 75", 76, 824.25m },
                    { 76, "Product 76", 77, 835.24m },
                    { 77, "Product 77", 78, 846.23m },
                    { 78, "Product 78", 79, 857.22m },
                    { 79, "Product 79", 80, 868.21m },
                    { 80, "Product 80", 81, 879.20m },
                    { 81, "Product 81", 82, 890.19m },
                    { 82, "Product 82", 83, 901.18m },
                    { 83, "Product 83", 84, 912.17m },
                    { 84, "Product 84", 85, 923.16m },
                    { 85, "Product 85", 86, 934.15m },
                    { 86, "Product 86", 87, 945.14m },
                    { 87, "Product 87", 88, 956.13m },
                    { 88, "Product 88", 89, 967.12m },
                    { 89, "Product 89", 90, 978.11m },
                    { 90, "Product 90", 91, 989.10m },
                    { 91, "Product 91", 92, 1000.09m },
                    { 92, "Product 92", 93, 1011.08m },
                    { 93, "Product 93", 94, 1022.07m },
                    { 94, "Product 94", 95, 1033.06m },
                    { 95, "Product 95", 96, 1044.05m },
                    { 96, "Product 96", 97, 1055.04m },
                    { 97, "Product 97", 98, 1066.03m },
                    { 98, "Product 98", 99, 1077.02m },
                    { 99, "Product 99", 100, 1088.01m },
                    { 100, "Product 100", 1, 1099.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packaging_Hierarchies_ChildPackagingId",
                table: "Packaging_Hierarchies",
                column: "ChildPackagingId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PackagingId",
                table: "Products",
                column: "PackagingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packaging_Hierarchies");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Packagings");
        }
    }
}
