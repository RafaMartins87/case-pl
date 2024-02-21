using Catalog.API.Models;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Services
{
    public class ProductService : IProductService
    {
        private IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            
            _productRepository = productRepository;
        }

        public async Task<bool> ValidateProductAsync(Product product)
        {
            if (product == null) { return false ; }

            Product produtoExistente = await _productRepository.Get(product.Id);

            bool produtoJahExistente = produtoExistente == null ? false : true;
            if (!produtoJahExistente)
            {
                return true ;
            }
            bool ehMesmoCodigoDeBarra = produtoExistente.CodigoBarras == product.CodigoBarras ? true : false;

            if (produtoJahExistente && ehMesmoCodigoDeBarra)
            {
                return false;
            }

            return true;
        }
    }
}
