using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ICategorieServices
{
    void AddCategories(Categorie categories);
    List<Categorie> GetAllCategories();
    void UpdateCategories(int Id, Categorie categories);
    void DeleteCategories(int Id);
}