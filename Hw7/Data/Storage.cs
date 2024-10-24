using Hw7.Entities;
using Hw7.Enums;

namespace Hw7.Data
{
    public static class Storage
    {
        public static User OnlineUser { get; set; }
        public static List<ShoppingCart> AllShoppingCarts { get; set; } = new List<ShoppingCart>();


        public static List<User> Users = new List<User>()
        {
            new User()
            {
                UserId = 1,
                FullName = "Setareh Zaman",
                Password = "123",
                PhoneNumber = "091200",
                Role = RoleEnum.Customer
            },
            new User()
            {
                UserId = 2,
                FullName = "seza",
                Password = "456",
                PhoneNumber = "091211",
                Role = RoleEnum.Customer
            },
            new User()
            {
                UserId = 3,
                FullName = "Admin",
                Password = "123",
                PhoneNumber ="123",
                Role = RoleEnum.Admin
            }
        };


        public static List<Product> Products = new List<Product>()
        {
            new Product()
            {
                ProductId = 1,
                Name = "Asuz",
                Description = "Zenbook",
                Category = CategoryEnum.LapTop,
                Price = 55000000,
            },

            new Product ()
            {
                ProductId = 2,
                Name = "Asuz",
                Description = "Vivobook",
                Category= CategoryEnum.LapTop,
                Price = 38000000
           },

            new Product ()
            {
                ProductId = 3,
                Name = "Lenovo",
                Description = "Customize",
                Category = CategoryEnum.LapTop,
                Price = 30000000
            },

            new Product()
            {
                ProductId= 4,
                Name = "hp",
                Description = "Anthlon",
                Category= CategoryEnum.LapTop,
                Price = 15000000
            },

            new Product()
            {
                ProductId = 5,
                Name = "MacBook Pro",
                Description = "chip M1",
                Category= CategoryEnum.LapTop,
                Price = 70000000
            },
            new Product()

            {
                ProductId = 6,
                Name = "MacBook Air",
                Description = "chip M2",
                Category= CategoryEnum.LapTop,
                Price = 48000000
            },

            new Product()
            {
                ProductId = 7,
                Name = "acer",
                Description = "Nitro V 15",
                Category= CategoryEnum.LapTop,
                Price = 50000000
            }
        };
    }

}

