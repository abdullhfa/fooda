using AutoMapper;
using CodeFood.Data;
using CodeFood.Service.Data;
using CodeFood.Service.Interfaces;
using CodeFood.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFood.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminPolicy")]
public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAllProducts();
        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<Product, ProductViewModel>());

        var mapper = new Mapper(config);

        var model = mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

        return View(model);
    }
}
