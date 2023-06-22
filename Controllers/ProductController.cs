using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Session4_BoilerPlate.AddDbContext;
using Session4_BoilerPlate.Models;
using Session4_BoilerPlate.Repository;

namespace Session4_BoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*[Route("api/{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]*/
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            if (products == null)
            {
                return NotFound("No products found.");
            }



            return Ok(products);


        }


        [HttpPost]
        public IActionResult CreateProduct(CreateProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var addedProduct = _mapper.Map<Product>(productDTO);
                _productRepository.AddProduct(addedProduct);
                return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.ProductId }, addedProduct);
            }

            return BadRequest(ModelState);
        }




        [HttpGet("{id}")]
        //[ApiVersion("2.0")] orr we can create seperate method for diff versions
        public IActionResult GetProduct(int id)
        {
            Product product = _productRepository.GetProductById(id);

            /*if (product == null)
            {
                return NotFound();
            }*/

            var products = _mapper.Map<GetProductDTO>(product);

            return Ok(product);
        }



        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, CreateProductDTO productDTO)
        {
            var existingProduct = _productRepository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            _mapper.Map(productDTO, existingProduct);

            if (_productRepository.UpdateProduct(existingProduct))
            {
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the product.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productRepository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            if (_productRepository.DeleteProduct(existingProduct))
            {
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the product.");
        }





    }
}
