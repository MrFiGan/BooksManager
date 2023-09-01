using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAPI.Migrations
{
    public partial class InitialConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    PublicationYear = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetBooksByPaginationAndSorting
                    @PageNumber INT,
                    @PageSize INT,
                    @SearchText NVARCHAR(100),
                    @SortBy NVARCHAR(50),
                    @SortOrder BIT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @StartRowIndex INT = (@PageNumber - 1) * @PageSize;
                    DECLARE @EndRowIndex INT = @StartRowIndex + @PageSize;

                    DECLARE @SortColumn NVARCHAR(50) = 
                        CASE
                            WHEN @SortBy = 'Title' OR @SortBy IS NULL THEN 'Title'
                            WHEN @SortBy = 'Author' THEN 'Author'
                            WHEN @SortBy = 'ISBN' THEN 'ISBN'
                            WHEN @SortBy = 'PublicationYear' THEN 'PublicationYear'
                            WHEN @SortBy = 'Quantity' THEN 'Quantity'
                            WHEN @SortBy = 'CategoryId' THEN 'CategoryId'
                            ELSE 'Title' -- Default sorting column
                        END;

                    DECLARE @SortDirection NVARCHAR(4) = 
                        CASE
                            WHEN @SortOrder = 1 THEN 'ASC'
                            ELSE 'DESC'
                        END;

                    DECLARE @DynamicSql NVARCHAR(MAX);

                    SET @DynamicSql = '
                        SELECT 
                            Id,
                            Title,
                            Author,
                            ISBN,
                            PublicationYear,
                            Quantity,
                            CategoryId
                        FROM (
                            SELECT 
                                Id,
                                Title,
                                Author,
                                ISBN,
                                PublicationYear,
                                Quantity,
                                CategoryId,
                                ROW_NUMBER() OVER (ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortDirection + ') AS RowNum
                            FROM Books
                            WHERE
                                @SearchText IS NULL OR
                                Title LIKE ''%'' + @SearchText + ''%'' OR
                                Author LIKE ''%'' + @SearchText + ''%''
                        ) AS Result
                        WHERE RowNum > @StartRowIndex AND RowNum <= @EndRowIndex
                        ORDER BY RowNum;';

                    EXEC sp_executesql @DynamicSql,
                        N'@SearchText NVARCHAR(100), @StartRowIndex INT, @EndRowIndex INT',
                        @SearchText, @StartRowIndex, @EndRowIndex;
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
