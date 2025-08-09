using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IProductServices
{
    void AddProduct(Product product);
    List<Product> GetAllProducts();
    void UpdateProduct(int Id , Product product);
    void DeleteProduct(int Id);
    List<Product> SearchProductByName(string Name);
    List<Product> GetProductsByCategory(int categoryId);
    List<Product> GetProductsBySeller(int sellerId);
    
}