using UnitTestingProject.Entities;

namespace UnitTestingProject.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int top=0);
        Product? GetProductById(int id);
        Product Add(Product product);
        Product Update(Product product);
        void Delete(int id);
    }
}
