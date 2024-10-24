using Hw7.Enums;

namespace Hw7.Entities
{
    public class User 
    {
        public User() { }
        public User(int id, string phoneNumber, string password,RoleEnum role)
        {
            UserId = id;
            PhoneNumber = phoneNumber;
            Password = password;
            Role = role;
        }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public RoleEnum Role { get; set; }

        public string Address { get; set; }
        public List<ShoppingCart> PreviousShoppingCarts { get; set; } = new List<ShoppingCart>();
        public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();
    }
}
