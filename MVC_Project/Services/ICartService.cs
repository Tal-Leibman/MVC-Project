using MVC_Project.Models;

namespace MVC_Project.Services
{
    public interface ICartService
    {
        Cart GetCart();
        bool RemoveProduct(int id);
        void ClearAllProducts();
        bool AddToCart(long id);
    }
}
