using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface IProductService
    {
        Task<bool> ValidateProductAsync(Product product);
    }
}
