using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IOrderServices
{
    void AddProductToOrder(int orderId, int productId, int qty);
    void GetOrdersByCustomer(int customerId);
    void DeleteOrder( int orderId);
    void GetOrderDetails(int orderId);
}



