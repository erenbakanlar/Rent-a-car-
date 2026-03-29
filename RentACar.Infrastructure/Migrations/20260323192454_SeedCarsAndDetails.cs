using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCarsAndDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Transmission",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Category", "FuelType", "ImageUrl", "Model", "PricePerDay", "Seats", "Status", "Transmission", "Year" },
                values: new object[,]
                {
                    { 1, "BMW", 3, "Benzin", "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=600&q=80", "7 Series", 4500m, 5, 0, "Otomatik", 2024 },
                    { 2, "Mercedes", 3, "Hibrit", "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=600&q=80", "S-Class", 5200m, 5, 0, "Otomatik", 2024 },
                    { 3, "Audi", 3, "Dizel", "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=600&q=80", "A8", 4200m, 5, 0, "Otomatik", 2023 },
                    { 4, "Porsche", 3, "Hibrit", "https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=600&q=80", "Cayenne", 6000m, 5, 0, "Otomatik", 2024 },
                    { 5, "Toyota", 2, "Dizel", "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=600&q=80", "Land Cruiser", 2800m, 7, 0, "Otomatik", 2023 },
                    { 6, "Nissan", 2, "Benzin", "https://images.unsplash.com/photo-1609521263047-f8f205293f24?w=600&q=80", "Qashqai", 1600m, 5, 0, "Otomatik", 2023 },
                    { 7, "Hyundai", 2, "Hibrit", "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=600&q=80", "Tucson", 1750m, 5, 0, "Otomatik", 2024 },
                    { 8, "Peugeot", 2, "Dizel", "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d?w=600&q=80", "3008", 1500m, 5, 0, "Otomatik", 2023 },
                    { 9, "Ford", 4, "Dizel", "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=600&q=80", "Tourneo", 2200m, 9, 0, "Manuel", 2023 },
                    { 10, "Volkswagen", 4, "Dizel", "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?w=600&q=80", "Caravelle", 2400m, 9, 0, "Otomatik", 2022 },
                    { 11, "Volkswagen", 1, "Benzin", "https://images.unsplash.com/photo-1471444928139-48c5bf5173f8?w=600&q=80", "Golf", 1100m, 5, 0, "Manuel", 2023 },
                    { 12, "Toyota", 1, "Hibrit", "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?w=600&q=80", "Corolla", 1050m, 5, 0, "Otomatik", 2024 },
                    { 13, "Renault", 1, "Dizel", "https://images.unsplash.com/photo-1580273916550-e323be2ae537?w=600&q=80", "Megane", 950m, 5, 0, "Manuel", 2023 },
                    { 14, "Renault", 0, "Benzin", "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=600&q=80", "Clio", 750m, 5, 0, "Manuel", 2023 },
                    { 15, "Fiat", 0, "Benzin", "https://images.unsplash.com/photo-1590362891991-f776e747a588?w=600&q=80", "Egea", 700m, 5, 0, "Manuel", 2024 },
                    { 16, "Hyundai", 0, "Benzin", "https://images.unsplash.com/photo-1494976388531-d1058494cdd8?w=600&q=80", "i20", 720m, 5, 0, "Manuel", 2023 },
                    { 17, "Opel", 0, "Benzin", "https://images.unsplash.com/photo-1502877338535-766e1452684a?w=600&q=80", "Corsa", 680m, 5, 0, "Manuel", 2023 },
                    { 18, "Peugeot", 0, "Elektrik", "https://images.unsplash.com/photo-1568605117036-5fe5e7bab0b7?w=600&q=80", "208", 710m, 5, 0, "Otomatik", 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.AlterColumn<string>(
                name: "Transmission",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
