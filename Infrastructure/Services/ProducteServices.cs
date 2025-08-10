using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class ProducteServices:IProductServices
{
    private readonly string connectionString = DataContextHelper.GetConnectionString();
    public void AddProduct(Product product)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            insert into products (name,price,quantity,categoryId,sellerId)
            values  (@name,@price,@quantity,@categoryId,@sellerId);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@quantity", product.Quantity);
            command.Parameters.AddWithValue("@categoryId", product.CategoryId);
            command.Parameters.AddWithValue("@sellerId", product.SellerId);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Product added");
            }
            else
            {
                Console.WriteLine("Product not added");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Product> GetAllProducts()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "select * from products";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            List<Product> products = new();
            while (reader.Read())
            {
                var product = new Product()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("categoryId")),
                    SellerId = reader.GetInt32(reader.GetOrdinal("sellerId")),
                };
                products.Add(product);
            }
            connection.Close();
            if (products.Any())
            {
                Console.WriteLine(products.Count+"- product found!");
                return products;
            }
            else
            {
                throw new Exception("No products found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void UpdateProduct(int Id, Product product)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
        select * from products where id = @id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id);
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("Product not found");
            }
            reader.Close();
            const string query1 = @"
        update products set name=@name,price=@price,quantity=@quantity,categoryId=@categoryId,sellerId=@sellerId where id=@id;
";
            using var command1 = new NpgsqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@id", Id);
            command1.Parameters.AddWithValue("@name", product.Name);
            command1.Parameters.AddWithValue("@price", product.Price);
            command1.Parameters.AddWithValue("@quantity", product.Quantity);
            command1.Parameters.AddWithValue("@categoryId", product.CategoryId);
            command1.Parameters.AddWithValue("@sellerId", product.SellerId);
            var result = command1.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Product updated");
            }
            else
            {
                Console.WriteLine("Product not updated");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void DeleteProduct(int Id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            delete from products 
            where id = @id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Product deleted");
            }
            else
            {
                Console.WriteLine("Product not deleted");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Product>  SearchProductByName(string Name)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "select * from products where name like @name;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("name", $"%{Name}%");
            using var reader = command.ExecuteReader();
            var products = new List<Product>();
            while (reader.Read())
            {
                var product = new Product()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("categoryId")),
                    SellerId = reader.GetInt32(reader.GetOrdinal("sellerId")),
                };
                products.Add(product);
            }
            connection.Close();
            if (products.Count > 0)
            {
                return products;
            }
            else
            {
                Console.WriteLine("No products found");
                return new List<Product>();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Product> GetProductsByCategory(int categoryId)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "select * from products where categoryId = @categoryId;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@categoryId", categoryId);
            using var reader = command.ExecuteReader();
            var products = new List<Product>();
            while (reader.Read())
            {
                var product = new Product()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("categoryId")),
                    SellerId = reader.GetInt32(reader.GetOrdinal("sellerId"))
                };
                products.Add(product);
            }
            connection.Close();
            if (products.Count > 0)
            {
                return products;
            }
            else
            {
                Console.WriteLine("No products found");
                return new List<Product>();   
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Product> GetProductsBySeller(int sellerId)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "select * from products where sellerId = @sellerId;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@sellerId", sellerId);
            using var reader = command.ExecuteReader();
            var products = new List<Product>();
            while (reader.Read())
            {
                var product = new Product()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("categoryId")),
                    SellerId = reader.GetInt32(reader.GetOrdinal("sellerId"))
                };
                products.Add(product);
            }
            connection.Close();
            if (products.Count > 0)
            {
                return products;
            }
            else
            {
                Console.WriteLine("No products found");
                return new List<Product>();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}