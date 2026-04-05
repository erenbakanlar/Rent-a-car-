using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Infrastructure.Migrations
{
    public partial class SeedCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Economy
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Fiat", "Egea", 2023, 450m, 0, 0, "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=800", 5, "Benzin", "Manuel" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Renault", "Clio", 2023, 420m, 0, 0, "https://images.unsplash.com/photo-1583121274602-3e2820c69888?w=800", 5, "Benzin", "Manuel" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Hyundai", "i20", 2023, 440m, 0, 0, "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=800", 5, "Benzin", "Otomatik" });

            // Compact
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Volkswagen", "Golf", 2023, 650m, 0, 1, "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=800", 5, "Dizel", "Otomatik" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Ford", "Focus", 2023, 620m, 0, 1, "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=800", 5, "Benzin", "Otomatik" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Toyota", "Corolla", 2023, 680m, 0, 1, "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?w=800", 5, "Hibrit", "Otomatik" });

            // SUV
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Nissan", "Qashqai", 2023, 850m, 0, 2, "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?w=800", 5, "Dizel", "Otomatik" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Peugeot", "3008", 2023, 880m, 0, 2, "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", 5, "Dizel", "Otomatik" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Hyundai", "Tucson", 2023, 900m, 0, 2, "https://images.unsplash.com/photo-1609521263047-f8f205293f24?w=800", 5, "Hibrit", "Otomatik" });

            // Premium
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "BMW", "3 Series", 2024, 1500m, 0, 3, "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=800", 5, "Benzin", "Otomatik" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Mercedes", "C-Class", 2024, 1600m, 0, 3, "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=800", 5, "Dizel", "Otomatik" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Audi", "A4", 2024, 1550m, 0, 3, "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", 5, "Dizel", "Otomatik" });

            // Van
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Volkswagen", "Transporter", 2023, 950m, 0, 4, "https://images.unsplash.com/photo-1527786356703-4b100091cd2c?w=800", 9, "Dizel", "Manuel" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Ford", "Transit Custom", 2023, 920m, 0, 4, "https://images.unsplash.com/photo-1464219789935-c2d9d9aba644?w=800", 9, "Dizel", "Manuel" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[] { "Mercedes", "Vito", 2023, 980m, 0, 4, "https://images.unsplash.com/photo-1506521781263-d8422e82f27a?w=800", 8, "Dizel", "Otomatik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Cars");
        }
    }
}
