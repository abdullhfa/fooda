using AutoMapper;
using CodeFood.Data;
using CodeFood.Service.Interfaces;
using CodeFood.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFood.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductViewModel>());

            var mapper = new Mapper(config);

            var model = mapper
                .Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return View(model);
        }

        public IActionResult ShowSingleProduct(Guid id)
        {
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductViewModel>());

            var mapper = new Mapper(config);

            var model = mapper.Map<Product, ProductViewModel>(_productService.GetProductById(id));
            return View(model);
        }
    }
}
