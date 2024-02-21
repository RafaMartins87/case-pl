using Catalog.API.Models;
using Catalog.API.UoW;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;

namespace Catalog.API.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IUnitofWork unitOfwork) : base(unitOfwork)
        {
        }

    }
}
