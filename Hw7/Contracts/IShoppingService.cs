
using Hw7.Entities;

namespace Hw7.Contracts
{
    public interface IShoppingService
    {
        public void ShowProductList();

        public void AddToShoppingList(int productId, int count);

        public void FinalShoppingList();

        public void ShowPreviousShoppingLists();

        public void CheckOut();
    }
}
