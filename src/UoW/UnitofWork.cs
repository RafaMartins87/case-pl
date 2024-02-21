using Catalog.API.Data;
using Catalog.API.UoW;
using Microsoft.EntityFrameworkCore;

public class UnitofWork : IUnitofWork
{
    private readonly AppDbContext _context;
    private bool _disposed = false;
    public DbContext Context => _context;

    public UnitofWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}