using Hw7.Data;
using Hw7.Contracts;
using Hw7.Entities;
using Colors.Net.StringColorExtensions;
using Colors.Net;
using System.Xml;
using Hw7.Enums;

namespace Hw7.Service
{
    public class ShoppingService : IShoppingService
    {

        public void ShowProductList()
        {
            foreach (var p in Storage.Products)
            {
                Console.WriteLine($"{p.ProductId}-{p.Name}-{p.Description}-{p.Price}, Quantity: {p.Quantity}");
                ColoredConsole.WriteLine("-----------------------------------".Cyan());
            }
        }
        public void AddToShoppingList(int productId, int count)
        {
            bool addToCart = false;

            var product = Storage.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product.Quantity >= count)
            {

                if (Storage.OnlineUser.ShoppingCart.Items.Any(i => i.ItemId == productId))
                {
                    var itemToUpdate = Storage.OnlineUser.ShoppingCart.Items.FirstOrDefault(i => i.ItemId == productId);
                    itemToUpdate.Count += count;
                    addToCart = true;
                }
                if (addToCart == false)
                {
                    Storage.OnlineUser.ShoppingCart.Items.Add(new ShoppingCartItem(product, count));
                    Console.WriteLine($"\n{count} of {product.Name} added to your cart.");
                }
            }
            else
            {
                ColoredConsole.WriteLine("\nSorry! Not enough Quantity...".Red());
                ColoredConsole.WriteLine($"We Only have {product.Quantity} of {product.Name}-{product.Description} left ...".Red());
            }
        }
        public void FinalShoppingList()
        {
            if (Storage.OnlineUser != null)
            {
                ColoredConsole.WriteLine("\nFinalizing your shopping list...".Yellow());
                Console.WriteLine("\nProducts in your Shopping Cart :");

                foreach (var item in Storage.OnlineUser.ShoppingCart.Items)
                {

                    ColoredConsole.WriteLine("-----------------------------------".Cyan());
                    Console.WriteLine($"{item.Product.Name}-{item.Product.Description}: {item.Count} x {item.Product.Price} = {item.Price}");

                }
                ColoredConsole.WriteLine($"\nTotal Price: {Storage.OnlineUser.ShoppingCart.TotalPrice}".Cyan()); 
            }
        }
        public void CheckOut()
        {
            Storage.OnlineUser.ShoppingCart.OrderDate = DateTime.Now;
            Storage.OnlineUser.ShoppingCart.IsConfirm = false;

            foreach (var item in Storage.OnlineUser.ShoppingCart.Items)
            {
                foreach (var product in Storage.Products)
                {
                    if (item.Product.ProductId == product.ProductId)
                    {
                        product.Quantity -= item.Count;
                    }
                }
            }

            Storage.OnlineUser.ShoppingCart.UserId = Storage.OnlineUser.UserId;
            Storage.AllShoppingCarts.Add(Storage.OnlineUser.ShoppingCart);  
            Storage.OnlineUser.PreviousShoppingCarts.Add(Storage.OnlineUser.ShoppingCart);
            Storage.OnlineUser.ShoppingCart = new ShoppingCart();
        }
        public void ShowPreviousShoppingLists()     
        {
            if (Storage.OnlineUser.ShoppingCart is null)
            {
                ColoredConsole.WriteLine("\nYou have no previous shopping lists!".Red());
                return;
            }

            Console.WriteLine("\nPrevious Shopping Lists:");
            foreach (var shoppingCart in Storage.OnlineUser.PreviousShoppingCarts)
            {
                ColoredConsole.WriteLine($"\nShopping List Total Price: {shoppingCart.TotalPrice}".Cyan());
                Console.WriteLine($"\t{shoppingCart.OrderDate}");
                Console.WriteLine("\nProducts Are :");
                foreach (var item in shoppingCart.Items)
                {
                    Console.WriteLine($"{item.Product.Name}-{item.Product.Description}: {item.Count} x {item.Product.Price} = {item.Price}");
                    ColoredConsole.WriteLine("-----------------------------------".Cyan());
                }
            }
        }

        public void AddProduct()
        {
            Console.WriteLine("\nName : ");
            string name = Console.ReadLine();
            Console.WriteLine("Description : ");
            string description = Console.ReadLine();
            Console.WriteLine("Category : ");
            Console.WriteLine("1.Laptop 2.Mobile 3.Tablet");
            int option = int.Parse(Console.ReadLine());
            CategoryEnum category = (CategoryEnum)option;
            int count = Storage.Products.Count + 1;
            Storage.Products.Add(new() { ProductId = count, Name = name, Description = description, Category = category });
            ColoredConsole.WriteLine("The Product Got Seccssusfully Added...".Green());
        }
        public void RemoveProduct(int productId)
        {
            var removingProduct = Storage.Products.First(p => p.ProductId == productId);
            Storage.Products.Remove(removingProduct);
        }
        public void UpdateQuantity(int productId, int newQuantity)
        {
            var updatingProduct = Storage.Products.First(p => p.ProductId == productId);
            updatingProduct.Quantity = newQuantity;
            ColoredConsole.WriteLine("Succssful!".Green());
        }
        public void ConfirmingOrder()
        {
            foreach (var shoppingCart in Storage.AllShoppingCarts)
            {
                if(shoppingCart.IsConfirm == false)
                {
                    Console.WriteLine($"\n The ShoppingCart ( {shoppingCart.Id} )");
                    foreach (var item in shoppingCart.Items)
                    {
                        Console.WriteLine($"{item.Product.Name}-{item.Product.Description}: {item.Count} x {item.Product.Price} = {item.Price}");
                        ColoredConsole.WriteLine("-----------------------------------".Cyan());
                    }
                    ColoredConsole.WriteLine($"Shopping List Total Price: {shoppingCart.TotalPrice}".Cyan());
                } 
            }
            Console.WriteLine("Which Shopping Cart you wanna make Confirm? (id)");
            int option = int.Parse(Console.ReadLine());
            var checkedShoppingCart = Storage.AllShoppingCarts.First(x => x.Id == option);
            checkedShoppingCart.IsConfirm = true;
        }
        public void ShoppingHistory()
        {
            foreach (var shoppingCart in Storage.AllShoppingCarts)
            {
                Console.WriteLine($"\nThe Customer ( {shoppingCart.UserId} ) has : ");

                foreach (var item in shoppingCart.Items)
                {
                    Console.WriteLine($"{item.Product.Name}-{item.Product.Description}: {item.Count} x {item.Product.Price} = {item.Price}");
                    ColoredConsole.WriteLine("-----------------------------------".Cyan());
                }
                ColoredConsole.WriteLine($"Shopping List Total Price: {shoppingCart.TotalPrice}".Cyan());
            }
        }
        public void ShoppingHistoryLinq()
        {
            var groupedCarts = Storage.AllShoppingCarts
              .GroupBy(cart => cart.UserId) // Group shopping carts by user ID
              .Select(group => new // Create anonymous object for each group
              {
                  UserId = group.Key,
                  Items = group.Select(cart => $"{cart.Items.Select(item => $"{item.Product.Name}-{item.Product.Description}: {item.Count} x {item.Product.Price}").Aggregate((a, b) => a + "\n" + b)}"), // Combine item details with newline
                  TotalPrice = group.Sum(cart => cart.TotalPrice)
              });

            foreach (var cartGroup in groupedCarts)
            {
                Console.WriteLine($"\nThe Customer ( {cartGroup.UserId} ) has : ");
                Console.WriteLine(cartGroup.Items); // Print combined item details
                ColoredConsole.WriteLine("-----------------------------------".Cyan());
                ColoredConsole.WriteLine($"Shopping List Total Price: {cartGroup.TotalPrice}".Cyan());
            }
        }
    }
}


