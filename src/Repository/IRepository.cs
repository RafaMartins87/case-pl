using Microsoft.AspNetCore.Mvc;
using Catalog.API.Models;

namespace Catalog.API.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> Get(int id);
        
        public Task<ActionResult<T>> Create(T entity);
        public Task<IActionResult> Update(int id, T entity);
        public Task<IActionResult> Delete(int id);
    }
}
