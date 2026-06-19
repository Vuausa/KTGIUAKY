using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KTGIUAKY.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DishCategories_BCS240047",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Các món ăn nhẹ khai vị", "Món khai vị" },
                    { 2, "Các món ăn chính", "Món chính" },
                    { 3, "Các món tráng miệng ngọt", "Món tráng miệng" }
                });

            migrationBuilder.InsertData(
                table: "Dishes_BCS240047",
                columns: new[] { "Id", "Description", "DishCategoryId", "IsAvailable", "Name", "PreparationTime", "Price" },
                values: new object[,]
                {
                    { 1, "Salad rau tươi trộn dầu giấm", 1, true, "Salad rau trộn", 10, 35000m },
                    { 2, "Súp cua thơm ngon", 1, true, "Súp cua", 15, 45000m },
                    { 3, "Bò nướng lá lốt thơm lừng", 2, true, "Bò nướng lá lốt", 25, 120000m },
                    { 4, "Cơm chiên với tôm, mực", 2, true, "Cơm chiên hải sản", 20, 85000m },
                    { 5, "Kem dâu tươi mát lạnh", 3, false, "Kem dâu", 5, 25000m }
                });

            migrationBuilder.InsertData(
                table: "DishImages_BCS240047",
                columns: new[] { "Id", "DishId", "ImageUrl", "IsThumbnail" },
                values: new object[,]
                {
                    { 1, 1, "/images/salad.jpg", true },
                    { 2, 2, "/images/supcua.jpg", true },
                    { 3, 3, "/images/bonuong.jpg", true },
                    { 4, 4, "/images/comchien.jpg", true },
                    { 5, 5, "/images/kemdau.jpg", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DishImages_BCS240047",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DishImages_BCS240047",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DishImages_BCS240047",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DishImages_BCS240047",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DishImages_BCS240047",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dishes_BCS240047",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dishes_BCS240047",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dishes_BCS240047",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dishes_BCS240047",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dishes_BCS240047",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DishCategories_BCS240047",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DishCategories_BCS240047",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DishCategories_BCS240047",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
