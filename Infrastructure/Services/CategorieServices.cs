using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;
namespace Infrastructure.Services;

public class CategorieServices: ICategorieServices
{
    private readonly string connectionString=DataContextHelper.GetConnectionString();
    public void AddCategories(Categorie categories)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
        insert into categories(name, description)
        values (@name, @description);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", categories.Name);
            command.Parameters.AddWithValue("@description", categories.Description);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Category Added");
            }
            else
            {
                Console.WriteLine("Category Not Added");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Categorie> GetAllCategories()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
        select * from categories;
";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            List<Categorie> categories = new();
            while (reader.Read())
            {
                var category = new Categorie()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Description = reader.GetString(reader.GetOrdinal("description"))
                };
                categories.Add(category);
            }
            connection.Close();
            if (categories.Any())
            {
                Console.WriteLine(categories.Count+"-category found!");
                return categories;
            }
            else
            {
                throw new Exception("No categories found");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<Categorie>();
        }
    }

    public void UpdateCategories(int Id, Categorie categories)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            select * from categories where id=@id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id);
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("No categories found");
            }

            const string query1 = @"
            update categories set name=@name,description=@description where id=@id;
";
            using var command1 = new NpgsqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@id", Id);
            command1.Parameters.AddWithValue("@name", categories.Name);
            command1.Parameters.AddWithValue("@description", categories.Description);
            var result = command1.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("categories updated");
            }
            else
            {
                Console.WriteLine("categories Not updated");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void DeleteCategories(int Id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
        delete from categories where id=@id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id); 
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Category Deleted");
            }
            else
            {
                Console.WriteLine("Category Not Deleted");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}