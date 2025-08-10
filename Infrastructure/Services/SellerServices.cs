using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;
namespace Infrastructure.Services;

public class SellerServices: ISellerServices
{
    private readonly string connectionString=DataContextHelper.GetConnectionString(); 
    public void AddSeller(Seller seller)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            insert into sellers (firstname, lastname, shop_name, email)
            values (@firstname, @lastname, @shop_name, @email);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue(@"firstname", seller.FirstName);
            command.Parameters.AddWithValue(@"lastname", seller.LastName);
            command.Parameters.AddWithValue(@"shop_name", seller.ShopName);
            command.Parameters.AddWithValue(@"email", seller.Email);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Seller added successfully");
            }
            else
            {
                Console.WriteLine("Failed to add seller");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Seller> GetAllSellers()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
        select * from sellers;
";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            List<Seller> sellers = new();
            while (reader.Read())
            {
                var seller = new Seller()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    ShopName = reader.GetString(reader.GetOrdinal("shop_name")),
                    Email = reader.GetString(reader.GetOrdinal("email"))
                };
                sellers.Add(seller);
            }
            connection.Close();
            if (sellers.Any())
            {
                Console.WriteLine(sellers.Count+"- seller found!");
                return sellers;
            }
            else
            {
                throw new("No sellers found!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void UpdateSeller(int Id, Seller seller)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
        select * from sellers where id = @id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue(@"id", Id);
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new("No seller found!");
            }
            reader.Close();
            const string query1 = @" 
        update sellers set firstname = @firstname, lastname = @lastname , shop_name = @shop_name, email = @email where id = @id;
";
            using var command1 = new NpgsqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@firstname", seller.FirstName);
            command1.Parameters.AddWithValue("@lastname", seller.LastName);
            command1.Parameters.AddWithValue("@shop_name", seller.ShopName);
            command1.Parameters.AddWithValue("@email", seller.Email);
            var result = command1.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Seller updated successfully");
            }
            else
            {
                Console.WriteLine("Failed to update seller");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void DeleteSeller(int Id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        const string query = @"
        delete from sellers where id = @id;
";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue(@"id", Id);
        var result = command.ExecuteNonQuery();
        connection.Close();
        if (result > 0)
        {
            Console.WriteLine("Seller deleted successfully");
        }
        else
        {
            Console.WriteLine("seller not deleted");
        }
    }


}