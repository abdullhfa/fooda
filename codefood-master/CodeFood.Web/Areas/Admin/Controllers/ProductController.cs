using AutoMapper;
using CodeFood.Data;
using CodeFood.Service.Interfaces;
using CodeFood.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFood.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Create()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductViewModel model, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() =>
                {
                    if (imageFile is not null)
                    {
                        model.ImagePath = imageFile.FileName;
                    }

                    var config = new MapperConfiguration(
                        cfg => cfg.CreateMap<ProductViewModel, Product>());

                    var mapper = new Mapper(config);

                    var entity = mapper.Map<ProductViewModel, Product>(model);

                    _productService.CreateProduct(entity);
                });

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Edit(Guid id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductViewModel model, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() =>
                {
                    if (imageFile is not null)
                    {
                        model.ImagePath = imageFile.FileName;
                    }

                    var config = new MapperConfiguration(
                        cfg => cfg.CreateMap<ProductViewModel, Product>());

                    var mapper = new Mapper(config);

                    var entity = mapper.Map<ProductViewModel, Product>(model);

                    _productService.UpdateProduct(entity);
                });

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Task.Run(() =>
            {
                _productService.DeleteProduct(id);
            });

            return RedirectToAction("Index", "Home");
        }
    }
}
