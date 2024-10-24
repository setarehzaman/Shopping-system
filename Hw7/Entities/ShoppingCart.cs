
using Hw7.Data;

namespace Hw7.Entities;

public class ShoppingCart
{
    public int Id { get; set; } = Storage.AllShoppingCarts.Count + 1;
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public decimal TotalPrice => Items.Sum(item => item.Price);
    public bool IsConfirm { get; set; } = false;
    public DateTime OrderDate { get; set; }
    public int UserId { get; set; }
}
public class ShoppingCartItem
{
    public ShoppingCartItem(Product product, int count)
    {
        Product = product;
        Count = count;
        ItemId = _itemIdCounter++;
        Price = Count * Product.Price;
    }

    private static int _itemIdCounter = 1;
    public int ItemId { get; set; }
    public Product Product { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }

}