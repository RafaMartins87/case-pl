using Catalog.API.Models;
using Catalog.API.Repository;
using Catalog.API.Services;
using Catalog.API.UoW;
using Microsoft.AspNetCore.Mvc;
using IUnitofWork = Catalog.API.UoW.IUnitofWork;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        IRepository<Product> _productRepository;
        private readonly IProductService _service;

        public ProductController(IUnitofWork unitOfwork, IRepository<Product> productRepository, IProductService service)
        {
            _unitOfWork = unitOfwork;
            _productRepository = productRepository;
            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Product>>> Products() 
        {
            try
            {
                IEnumerable<Product> teste = await _productRepository.Get();

                return Ok(teste);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Product>> Product(int id)
        {
            ValidateId(id);

            return await _productRepository.Get(id);
        }

        private void ValidateId(int id)
        {
            if (id.Equals(null))
            {
                BadRequest();
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<Product>> Product(string? name)
        {

            ValidateName(name);
            
            IEnumerable<Product> listaProdutos = await _productRepository.Get();

            return listaProdutos.Where(product => product.NomeProduto == name).FirstOrDefault();

        }

        private void ValidateName(string name)
        {
            if (name.Equals(null))
            {
                BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            //Validate(product);
            bool produtoEhValido = await _service.ValidateProductAsync(product);

            if (produtoEhValido)
            {
                await _productRepository.Create(product);

                return Created();
            }
            

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id.Equals(null))
            {
                BadRequest();
            }

            var products = await _productRepository.Delete(id);
            return products;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id.Equals(null) || id != product.Id)
            {
                BadRequest();
            }

            var products = await _productRepository.Update(id, product);
            return products;
        }
    }
}
