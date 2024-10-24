using Colors.Net;
using Colors.Net.StringColorExtensions;
using Hw7.Contracts;
using Hw7.Data;
using Hw7.Entities;
using Hw7.Enums;
using Hw7.Service;
using System.Security;

AuthenticationService authenticationService = new AuthenticationService();
ShoppingService shoppingService = new ShoppingService();
int GetIntInput(string outline)
{
    Console.Write(outline);
    string userInput = Console.ReadLine();
    return int.Parse(userInput);
}
void Start()
{
    do
    {
        Console.Clear();
        ColoredConsole.WriteLine("******* Welcome To Setareh's Online Shop ********".Magenta());
        Console.WriteLine("1. Sign Up");
        Console.WriteLine("2. Already Have Account (Login)");
        Console.WriteLine("0. Exit");

        int option = GetIntInput("\nPlease enter an option: ");
        switch (option)
        {
            case 1:
                Register();
                break;
            case 2:
                Login();
                break;
            case 0:
                ColoredConsole.WriteLine("\nExiting Account...".Yellow());
                return;
            default:
                ColoredConsole.Write("\nInvalid option. Please try again...".Yellow());
                Console.ReadKey();
                break;
        }
    } while (true);
}

#region Start Methods
void Register()
{
    ColoredConsole.Write("\nPlease enter your Phone Number: ".Yellow());
    string phoneNumber = Console.ReadLine();

    ColoredConsole.Write($"\nPlease enter a {"safe".DarkRed()} Password: ".Yellow());
    string password = Console.ReadLine();

    if (!IsValidPassword(password))
    {
        ColoredConsole.WriteLine("Password requirements not met. Please try again!".Red());
        ColoredConsole.WriteLine($"\n(longer than {"8 characters".Yellow()}\n at least {"one uppercase".Yellow()} and {"one lowercase".Yellow()} letter and {"one special character".Yellow()})");
        Console.ReadKey();
        return;
    }
    else
    {
        ColoredConsole.WriteLine("\nYou want to Register as : ".Yellow());
        int roleValue = GetIntInput("Enter 0 for Admin, 1 for Customer: ");
        RoleEnum role = (RoleEnum)roleValue;
        authenticationService.Register(phoneNumber, password, role);
        ColoredConsole.WriteLine("\nYour Account got seccssuflly created!".Green());
        ColoredConsole.WriteLine("For entering your account please login...".Green());
        Console.ReadKey();
    }
}
void Login()
{
    ColoredConsole.Write("\nPlease enter your Phone Number: ".Yellow());
    string email = Console.ReadLine();

    ColoredConsole.Write("\nPlease enter your Password: ".Yellow());
    string password = Console.ReadLine();

    if (authenticationService.Login(email, password))
    {
        if (Storage.OnlineUser.Role == RoleEnum.Customer) { CustomerMenu(); }
        else if (Storage.OnlineUser.Role == RoleEnum.Admin) { AdminMenu(); }
    }
    else
    {
        ColoredConsole.WriteLine("\nCouldnt find any Account with this info".Red());
        ColoredConsole.WriteLine("\nPlease try to sign up first!".Red());
        Console.ReadKey();
    }
}
bool IsValidPassword(string password)
{
    if (password.Length < 8)
    {
        return false;
    }
    bool hasUpper = false;
    bool hasLower = false;
    bool hasSpecial = false;
    char[] specialCharacters = "!@#$%^&*()_+-=[]{}|;':\",.<>?/`~".ToCharArray();

    foreach (char c in password)
    {
        if (char.IsUpper(c)) hasUpper = true;

        if (char.IsLower(c)) hasLower = true;

        if (Array.Exists(specialCharacters, element => element == c)) hasSpecial = true;
    }
    return hasUpper && hasLower && hasSpecial;
}

#endregion
void CustomerMenu()
{
    do
    {
        Console.Clear();
        ColoredConsole.WriteLine("********* Welcome Back To Your Acccount **********".Magenta());
        Console.WriteLine("1. Shopping ");
        Console.WriteLine("2. Shopping History");
        Console.WriteLine("3. Finalize My Shopping Cart");
        Console.WriteLine("4. Log Out");


        int option = GetIntInput("\nPlease enter an option: ");

        switch (option)
        {
            case 1:
                ShoppingPage();
                break;
            case 2:
                ShoppingHistory();
                break;
            case 3:
                CheckOut();
                break;
            case 4:
                LogOut();
                break;
            default:
                ColoredConsole.Write("\nInvalid option. Please try again...".Yellow());
                Console.ReadKey();
                break;
        }
    } while (true);

}
#region CustomerMenu Methods
void ShoppingPage()
{
    Console.Clear();
    Console.WriteLine("\nList of products Available in Market: ");
    ColoredConsole.WriteLine("-----------------------------------".Cyan());
    shoppingService.ShowProductList();
    int option = GetIntInput("\nWhich item You Want To Buy ? ");
    int count = GetIntInput("How Many ? ");
    shoppingService.AddToShoppingList(option, count);
    ColoredConsole.WriteLine("Press any key to continue...".Yellow());
    Console.ReadKey();
}

void ShoppingHistory()
{
    Console.Clear();
    shoppingService.ShowPreviousShoppingLists();
    ColoredConsole.WriteLine("Press any key to continue...".Yellow());
    Console.ReadKey();
}

void CheckOut()
{
    Console.Clear();
    shoppingService.FinalShoppingList();
    Console.WriteLine("\nDo You Want to Finish Shopping (y/n)");
    var result = Console.ReadLine();
    if (result == "y")
    {
        shoppingService.CheckOut();
        Console.WriteLine("Conecting to The Bank....");
        Console.ReadKey();
        CustomerMenu();
    }
    else if (result == "n")
    {
        CustomerMenu();
    }
}
void LogOut()
{
    authenticationService.LogOut();
    Start();
}
#endregion
Start();


void AdminMenu()
{
    do
    {
      Console.Clear();
        ColoredConsole.WriteLine("****** Welcome To Admin Menu *******".Magenta());
        Console.WriteLine("1. View All Products");
        Console.WriteLine("2. Add A New Product ");
        Console.WriteLine("3. Remove A Product");
        Console.WriteLine("4. Changing Quantity");
        Console.WriteLine("5. Confirming Carts");
        Console.WriteLine("6. Customer's ShoppingCarts");
        Console.WriteLine("7. Log Out");

        int option = GetIntInput("\nPlease enter an option: ");

        switch (option)
        {
            case 1:
                PrintAllProducts();
                break;
            case 2:
                AddProduct();
                break;
            case 3:
                RemoveProduct();
                break;
            case 4:
                UpdateQuantity();
                break;
            case 5:
                Confirming();
                break;
            case 6:
                AdminShoppingHistory();
                break;
            case 7:
                LogOut();
                break;
            default:
                ColoredConsole.Write("\nInvalid option. Please try again...".Yellow());
                Console.ReadKey();
                break;
        }   
    } while (true);
}

void PrintAllProducts()
{
    Console.Clear();
    Console.WriteLine("\nList of products Available in Market: ");
    ColoredConsole.WriteLine("-----------------------------------".Cyan());
    shoppingService.ShowProductList();
    Console.ReadKey();
}

void RemoveProduct()
{
    Console.Clear();
    Console.WriteLine("\nList of products Available in Market: ");
    ColoredConsole.WriteLine("-----------------------------------".Cyan());
    shoppingService.ShowProductList();
    int id = GetIntInput("\nPlease Enter the Id of the Product You Wanna Remove : ");
    shoppingService.RemoveProduct(id);
    ColoredConsole.WriteLine("It Got Succssesfully Removed..".Green());
    ColoredConsole.WriteLine("Press any key to continue...".Yellow());
    Console.ReadKey();

}
void AddProduct()
{
    shoppingService.AddProduct();
    ColoredConsole.WriteLine("Press any key to continue...".Yellow());
    Console.ReadKey();
}

void UpdateQuantity()
{
    Console.Clear();
    Console.WriteLine("\nList of products Available in Market: ");
    ColoredConsole.WriteLine("-----------------------------------".Cyan());
    shoppingService.ShowProductList();
    int id = GetIntInput("\nPlease Enter the Id of the Product You Wanna Update : ");
    int newQuantity = GetIntInput("New Quantity : ");
    shoppingService.UpdateQuantity(id, newQuantity);
    ColoredConsole.WriteLine("Press any key to continue...".Yellow());
    Console.ReadKey();
}
 void Confirming()
{
    shoppingService.ConfirmingOrder();
    ColoredConsole.WriteLine("Press any key to continue...".Yellow());
    Console.ReadKey();
}

void AdminShoppingHistory()
{
    shoppingService.ShoppingHistory();
    ColoredConsole.WriteLine("Press any key to continue...".Yellow());
    Console.ReadKey();
}