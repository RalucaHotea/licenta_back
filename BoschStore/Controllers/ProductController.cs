using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BoschStore.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;
        private readonly IUserService userService;

        public ProductController(IProductService _productService, IUserService _userService)
        {
            productService = _productService;
            userService = _userService;
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] ProductDto product)
        {
            if (product == null)
            {
                ModelState.AddModelError(string.Empty, "Product Object sent from client is null");
                return BadRequest("Product object is null");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Product object sent from client is invalid");
                return BadRequest("Invalid Product Object");
            }
            await productService.AddProductAsync(product);

            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<ActionResult> GetProductById([FromQuery] int productId)
        {
            var product = await productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductsByCategoryId")]
        public async Task<ActionResult> GeProductsByCategoryId([FromQuery] int categoryId)
        {
            var product = await productService.GetProductsByCategoryIdAsync(categoryId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductsBySubcategoryId")]
        public async Task<ActionResult> GetProductBySubcategoryId([FromQuery] int subcategoryId)
        {
            var product = await productService.GetProductsBySubcategoryIdAsync(subcategoryId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProductsAsync();
            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productToUpdate)
        {

            if (productToUpdate == null)
            {
                return NotFound("The product was not found!");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid product object sent from client");
                return BadRequest("Invalid product object");
            }
            await productService.UpdateProductAsync(productToUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {
            if (productId == 0)
            {
                ModelState.AddModelError(string.Empty, "Product object sent from client is null");
                return BadRequest("Product object is null");
            }
            await productService.DeleteProductAsync(productId);
            return Ok();
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult> GetAllCategories()
        {
            var categories = await productService.GetAllCategoriesAsync();
            if (!categories.Any())
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetAllSubcategoriesByCategoryId")]
        public async Task<ActionResult> GetAllByCategoryIdSubcategories([FromQuery] int categoryId)
        {
            var subcategories = await productService.GetAllSubcategoriesByCategoryIdAsync(categoryId);
            if (!subcategories.Any())
            {
                return NotFound();
            }
            return Ok(subcategories);
        }

        [HttpGet]
        [Route("GetAllSubcategories")]
        public async Task<ActionResult> GetAllSubcategories()
        {
            var subcategories = await productService.GetAllSubcategoriesAsync();
            if (!subcategories.Any())
            {
                return NotFound();
            }
            return Ok(subcategories);
        }

        [HttpPut("UploadImage/{productTitle}")]
        public async Task<IActionResult> UploadImages(string productTitle)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images", productTitle);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName).Replace(" ", String.Empty);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName).Replace(" ", String.Empty);
                    var dbPath = Path.Combine(folderName, fileName);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    file.CopyTo(stream);
                }
                else
                {
                    return BadRequest();
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        [Route("DeleteImageByImageName")]
        public async Task<IActionResult> DeleteImage([FromQuery] string imageName)
        {

            var folderName = Path.Combine("Resources", "Images");
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(folderPath, imageName);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

                return Ok();
            }

            return BadRequest("Could not delete the file!");
        }

        [HttpPost]
        [Route("AddItemToCart")]
        public async Task<ActionResult> AddDraft([FromBody] CartItemDto item)
        {
            if (item == null)
            {
                ModelState.AddModelError(string.Empty, "Product Object sent from client is null");
                return BadRequest("Product object is null");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Product object sent from client is invalid");
                return BadRequest("Invalid Product Object");
            }
            await productService.CreateCartItemAsync(item);

            return Ok(item);
        }

        [HttpPut]
        [Route("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemDto itemToUpdate)
        {
            if (itemToUpdate == null)
            {
                return NotFound("The product was not found!");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid product object sent from client");
                return BadRequest("Invalid product object");
            }
            await productService.UpdateCartItemAsync(itemToUpdate);
            return Ok();
        }

        [HttpGet]
        [Route("GetCartItemsByUserId")]
        public async Task<ActionResult> GetCartItemsByUserId([FromQuery] int userId)
        {
            var items = await productService.GetCartItemsByUserIdAsync(userId);
            return Ok(items);
        }

        [HttpGet]
        [Route("GetCartItemById")]
        public async Task<ActionResult> GetCartItemById([FromQuery] int itemId)
        {
            var item = await productService.GetCartItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound("The item was not found!");
            }
            return Ok(item);
        }


        [HttpDelete]
        [Route("DeleteCartItem")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int itemId)
        {
            if (itemId == 0)
            {
                ModelState.AddModelError(string.Empty, "Item object sent from client is null");
                return BadRequest("Item object is null");
            }
            await productService.DeleteCartItemAsync(itemId);
            return Ok();
        }

        [HttpGet]
        [Route("GetUserByUserUsername")]
        public async Task<ActionResult> GetUserByUserUsername([FromQuery] string username)
        {
            var user = await userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

    }
}
