using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;
namespace Infrastructure.Services;

public class CustomerServices : ICustomerServices
{
    private readonly string connectionString=DataContextHelper.GetConnectionString();
    public void AddCustomer(Customer Customers)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            insert into customers(firstname,lastname,email,phone)
            values (@firstname,@lastname,@email,@phone);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstname", Customers.FirstName);
            command.Parameters.AddWithValue("@lastname", Customers.LastName);
            command.Parameters.AddWithValue("@email", Customers.Email);
            command.Parameters.AddWithValue("@phone", Customers.Phone);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Successfully added Customer");
            }
            else
            {
                Console.WriteLine("Failed to add Customer");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Customer> GetAllCustomers()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "SELECT * FROM customers;";
            using var command = new NpgsqlCommand(query, connection);
            var reader = command.ExecuteReader();
            List<Customer> customers = new();
            while (reader.Read())
            {
                var customer = new Customer()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Email = reader.GetString(reader.GetOrdinal("email")),
                    Phone = reader.GetString(reader.GetOrdinal("phone"))
                };
                customers.Add(customer);
            }
            connection.Close();
            if (customers.Any())
            {
                Console.WriteLine(customers.Count+"-customers found!");
                return customers;
            }
            else
            {
                throw new Exception("No Customers Found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void UpdateCustomer(int Id, Customer Customers)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            select * from customers where id=@id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id);
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("No Customers Found");
            }
            reader.Close();
            const string query1 = @"
            update customers set firstname=@firstname,lastname=@lastname,email=@email,phone=@phone where id=@id;
";
            using var command1 = new NpgsqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@id",Id);
            command1.Parameters.AddWithValue("@firstname", Customers.FirstName);
            command1.Parameters.AddWithValue("@lastname", Customers.LastName);
            command1.Parameters.AddWithValue("@email", Customers.Email);
            command1.Parameters.AddWithValue("@phone", Customers.Phone);
            var result = command1.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Successfully updated Customer");
            }
            else
            {
                Console.WriteLine("Failed to update Customer");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DeleteCustomer(int Id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "delete from customers where id=@id;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Successfully deleted Customer");
            }
            else
            {
                throw new Exception("Failed to delete Customer");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}