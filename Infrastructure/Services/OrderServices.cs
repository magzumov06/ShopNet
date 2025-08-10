using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;
namespace Infrastructure.Services;

public class OrderServices:IOrderServices
{
    private readonly string connectionString=DataContextHelper.GetConnectionString();
    public void AddProductToOrder(int orderId, int productId, int qty)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        const string query = @"
        INSERT INTO  order_items (order_id, product_id, quantity,price)
        select @order_id, @product_id, @qty, p.price;
        from products p
        where p.id=@product_id;
";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@product_id", productId);
        command.Parameters.AddWithValue("@order_id", orderId);
        command.Parameters.AddWithValue("@qty", qty);
        var result = command.ExecuteNonQuery();
        connection.Close();
        if (result > 0)
        {
            Console.WriteLine("Product added!");
        }
        else
        {
            Console.WriteLine("Product not added!");
        }
    }

    public void GetOrdersByCustomer(int customerId)
    {
        
    }

    public void DeleteOrder(int orderId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        const string query = @"
        DELETE FROM order_item where order_id=@order_id;
";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@order_id", orderId);
        var  result = command.ExecuteNonQuery();
        connection.Close();
        if (result > 0)
        {
            Console.WriteLine("Order deleted!");
        }
        else
        {
            Console.WriteLine("Order not found!");
        }
    }

    public void GetOrderDetails(int orderId)
    {
        
    }
}