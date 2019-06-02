namespace MVC_Project.Services
{
    public interface ICartService
    {
        bool RemoveProduct(int id);
        void ClearAllProducts();
        bool AddToCart(long id);
    }
}
