using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ICustomerServices
{
    void AddCustomer(Customer Customers);
    List<Customer> GetAllCustomers();
    void UpdateCustomer(int Id,Customer Customers);
    void DeleteCustomer(int Id);
}