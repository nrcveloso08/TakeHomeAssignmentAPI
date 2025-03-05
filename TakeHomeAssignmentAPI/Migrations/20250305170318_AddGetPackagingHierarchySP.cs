using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeHomeAssignmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGetPackagingHierarchySP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetPackagingHierarchy
                AS
                BEGIN
                    WITH PackagingHierarchy AS (
                        SELECT 
                            P.Id AS PackagingID, 
                            P.Material, 
                            P.Dimensions, 
                            P.Type, 
                            P.Weight, 
                            PH.ParentPackagingID 
                        FROM Packagings P
                        LEFT JOIN Packaging_Hierarchies PH 
                            ON P.Id = PH.ChildPackagingID
                        WHERE PH.ChildPackagingID IS NULL 

                        UNION ALL

                        SELECT 
                            P.Id AS PackagingID, 
                            P.Material, 
                            P.Dimensions, 
                            P.Type, 
                            P.Weight, 
                            PH.ParentPackagingID
                        FROM Packagings P
                        JOIN Packaging_Hierarchies PH 
                            ON P.Id = PH.ChildPackagingID
                        JOIN PackagingHierarchy H
                            ON H.PackagingID = PH.ParentPackagingID
                    )
                    SELECT 
                        PH.PackagingID,
                        PH.Material,
                        PH.Dimensions,
                        PH.Type,
                        PH.Weight,
                        PH.ParentPackagingID,
                        PR.Id AS ProductID,
                        PR.Name AS ProductName,                        
                        PR.Price
                    FROM PackagingHierarchy PH
                    LEFT JOIN [Products] PR 
                        ON PH.PackagingID = PR.PackagingID;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetPackagingHierarchy;");
        }
    }
}
