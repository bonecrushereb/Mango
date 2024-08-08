using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //Show all Products
          public async Task<IActionResult> ProductIndex()
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

        //Create Product
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if(ModelState.IsValid)
            {
                 ResponseDto response = await _productService.CreateProductAsync(productDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"]="Product Created Successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else 
                {
                    TempData["error"]=response.Message;
                }

            }
            return View();
        }

        //Delete Product
        public async Task<IActionResult> ProductDelete(int productId)
        {
                 ResponseDto response = await _productService.GetProductByIdAsync(productId);

                if (response != null && response.IsSuccess)
                {
                    ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(productDto);
                }
                else 
                {
                    TempData["error"]=response.Message;
                }
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
                 ResponseDto response = await _productService.DeleteProductAsync(productDto.ProductId);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"]="Product Deleted Successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else 
                {
                    TempData["error"]=response.Message;
                }
                return View(productDto);
        }

        //Edit Product
        public async Task<IActionResult> ProductEdit(int productId)
        {
                 ResponseDto response = await _productService.GetProductByIdAsync(productId);

                if (response != null && response.IsSuccess)
                {
                    ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(productDto);
                }
                else 
                {
                    TempData["error"]=response.Message;
                }
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
                 ResponseDto response = await _productService.UpdateProductAsync(productDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"]="Product Updated Successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else 
                {
                    TempData["error"]=response.Message;
                }
                return View(productDto);
        }
    }
}