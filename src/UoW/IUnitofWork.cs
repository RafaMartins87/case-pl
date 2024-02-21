using Microsoft.EntityFrameworkCore;

namespace Catalog.API.UoW
{
    public interface IUnitofWork : IDisposable
    {
        DbContext Context { get; }
        public Task SaveChangesAsync();
    }
}
