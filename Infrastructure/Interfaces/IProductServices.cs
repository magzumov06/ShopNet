using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IProductServices
{
    void AddProduct(Product product);
    List<Product> GetAllProducts();
    void UpdateProduct(int Id , Product product);
    void DeleteProduct(int Id);
    void SearchProductByName(string Name);
    void GetProductsByCategory(int categoryId);
    void GetProductsBySeller(int sellerId);
    
}