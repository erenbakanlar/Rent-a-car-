using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Infrastructure.Migrations
{
    public partial class SeedCarData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Model", "Year", "PricePerDay", "Status", "Category", "ImageUrl", "Seats", "FuelType", "Transmission" },
                values: new object[,]
                {
                    // Premium
                    { 1,  "BMW",        "7 Series",     2024, 4500m, 0, 3, "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=600&q=80", 5, "Benzin",   "Otomatik" },
                    { 2,  "Mercedes",   "S-Class",      2024, 5200m, 0, 3, "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=600&q=80", 5, "Hibrit",   "Otomatik" },
                    { 3,  "Audi",       "A8",           2023, 4200m, 0, 3, "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=600&q=80", 5, "Dizel",    "Otomatik" },
                    { 4,  "Porsche",    "Cayenne",      2024, 6000m, 0, 3, "https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=600&q=80", 5, "Hibrit",   "Otomatik" },
                    // SUV
                    { 5,  "Toyota",     "Land Cruiser", 2023, 2800m, 0, 2, "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=600&q=80", 7, "Dizel",    "Otomatik" },
                    { 6,  "Nissan",     "Qashqai",      2023, 1600m, 0, 2, "https://images.unsplash.com/photo-1609521263047-f8f205293f24?w=600&q=80", 5, "Benzin",   "Otomatik" },
                    { 7,  "Hyundai",    "Tucson",       2024, 1750m, 0, 2, "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=600&q=80", 5, "Hibrit",   "Otomatik" },
                    { 8,  "Peugeot",    "3008",         2023, 1500m, 0, 2, "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d?w=600&q=80", 5, "Dizel",    "Otomatik" },
                    // Van
                    { 9,  "Ford",       "Tourneo",      2023, 2200m, 0, 4, "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=600&q=80", 9, "Dizel",    "Manuel"   },
                    { 10, "Volkswagen", "Caravelle",    2022, 2400m, 0, 4, "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?w=600&q=80", 9, "Dizel",    "Otomatik" },
                    // Compact
                    { 11, "Volkswagen", "Golf",         2023, 1100m, 0, 1, "https://images.unsplash.com/photo-1471444928139-48c5bf5173f8?w=600&q=80", 5, "Benzin",   "Manuel"   },
                    { 12, "Toyota",     "Corolla",      2024, 1050m, 0, 1, "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?w=600&q=80", 5, "Hibrit",   "Otomatik" },
                    { 13, "Renault",    "Megane",       2023,  950m, 0, 1, "https://images.unsplash.com/photo-1580273916550-e323be2ae537?w=600&q=80", 5, "Dizel",    "Manuel"   },
                    // Economy
                    { 14, "Renault",    "Clio",         2023,  750m, 0, 0, "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=600&q=80", 5, "Benzin",   "Manuel"   },
                    { 15, "Fiat",       "Egea",         2024,  700m, 0, 0, "https://images.unsplash.com/photo-1590362891991-f776e747a588?w=600&q=80", 5, "Benzin",   "Manuel"   },
                    { 16, "Hyundai",    "i20",          2023,  720m, 0, 0, "https://images.unsplash.com/photo-1494976388531-d1058494cdd8?w=600&q=80", 5, "Benzin",   "Manuel"   },
                    { 17, "Opel",       "Corsa",        2023,  680m, 0, 0, "https://images.unsplash.com/photo-1502877338535-766e1452684a?w=600&q=80", 5, "Benzin",   "Manuel"   },
                    { 18, "Peugeot",    "208",          2024,  710m, 0, 0, "https://images.unsplash.com/photo-1568605117036-5fe5e7bab0b7?w=600&q=80", 5, "Elektrik", "Otomatik" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Cars", keyColumn: "Id", keyValues: new object[]
                { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
        }
    }
}
