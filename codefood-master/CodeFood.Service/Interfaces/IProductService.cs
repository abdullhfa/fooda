using CodeFood.Data;

namespace CodeFood.Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(Guid id);
        Product? GetProductByName(string name);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid id);
    }
}
