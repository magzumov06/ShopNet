using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ISellerServices
{
    void AddSeller(Seller seller);
    List<Seller> GetAllSellers();
    void UpdateSeller(int Id, Seller seller);
    void DeleteSeller(int Id);
    void GetTopSellingProducts();
}