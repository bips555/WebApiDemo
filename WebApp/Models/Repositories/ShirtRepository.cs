using WebApp.Models;

namespace WebApp.Models.Repositories
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt{ShirtId=1,Size=12,Brand="A",Price=12,Gender="Men",Color="Blue"},
            new Shirt{ShirtId=2,Size=11,Brand="B",Price=22,Gender="Women",Color="Red"},
            new Shirt{ShirtId=3,Size=14,Brand="C",Price=32,Gender="Women",Color="Green"},
            new Shirt{ShirtId=4,Size=13,Brand="D",Price=42,Gender="Men",Color="White"},
            new Shirt{ShirtId=5,Size=15,Brand="E",Price=52,Gender="Women",Color="Black"},
        };
        public static bool ShirtExists(int id)
        {
            return shirts.Any(x => x.ShirtId == id);
        }
        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(x => x.ShirtId == id);
        }
        public static List<Shirt> GetShirts()
        {
            return shirts;
        }
        public static void AddShirt(Shirt shirt)
        {
            int maxId = shirts.Max(x => x.ShirtId);
            shirt.ShirtId = maxId + 1;
            shirts.Add(shirt);
        }
        public static void UpdateShirt(Shirt shirt)
        {

            var shirtToUpdate = shirts.First(x => x.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Gender = shirt.Gender;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Color = shirt.Color;

        }
        public static void DeleteShirt(int shirtId)
        {
            var shirt = GetShirtById(shirtId);
            if (shirt != null)
            {
                shirts.Remove(shirt);
            }
        }
        public static Shirt? GetShirtByProperties(string? brand, string? color, string? gender, int? size)
        {
            return shirts.FirstOrDefault(x => !string.IsNullOrEmpty(brand) &&
            !string.IsNullOrEmpty(x.Brand) &&
            x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrEmpty(color) &&
            !string.IsNullOrEmpty(x.Color) &&
            x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrEmpty(gender) &&
            !string.IsNullOrEmpty(x.Gender) &&
            x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
            x.Size.HasValue &&
            size.HasValue &&
            size.Value == x.Size.Value

            );
        }

    }
}
