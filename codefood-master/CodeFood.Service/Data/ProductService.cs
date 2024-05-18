using CodeFood.Data;
using CodeFood.Repository.Interfaces;
using CodeFood.Service.Interfaces;

namespace CodeFood.Service.Data
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAllEntities();
        }

        public Product GetProductById(Guid id)
        {
            return _repository.GetEntity(id);
        }

        public Product? GetProductByName(string name)
        {
            return GetAllProducts().FirstOrDefault(p => p.Name == name);
        }

        public void CreateProduct(Product product)
        {
            _repository.CreateEntity(product);
        }

        public void UpdateProduct(Product product)
        {
            _repository.UpdateEntity(product);
        }

        public void DeleteProduct(Guid id)
        {
            var product = _repository.GetEntity(id);
            _repository.DeleteEntity(product);
        }
    }
}
