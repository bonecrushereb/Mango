using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using Mango.Web.Models;

namespace Mango.Web.Controllers;

public class HomeController : Controller
{
        private readonly IProductService _productService;
    public HomeController(IProductService productService)
    {
            _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
            List<ProductDto> list = new();

            ResponseDto response = await _productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                list= JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else 
            {
                TempData["error"]=response.Message;
            }

            return View(list);
    }

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
    {
            ProductDto productDto = new();

            ResponseDto response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else 
            {
                TempData["error"]=response.Message;
            }

            return View(productDto);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
