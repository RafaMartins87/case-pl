using Catalog.API.Data;
using Catalog.API.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace Catalog.API.Repository
{
    public class RepositoryBase<T> : ControllerBase, IRepository<T> where T : class
    {
        protected Microsoft.EntityFrameworkCore.DbSet<T> dbSet;
        private readonly IUnitofWork _unitOfWork;

        public RepositoryBase(IUnitofWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
            dbSet = _unitOfWork.Context.Set<T>();
        }

        //Get Request
        public async Task<IEnumerable<T>> Get()
        {
            try
            {
                return dbSet.ToList();               

            }
            catch(Exception)
            {
                throw;
            }
        }

        //Get by Id Request
        public async Task<T> Get(int id)
        {
            try
            {

                return await dbSet.FindAsync(id);
                

            }
            catch (Exception)
            {
                throw;
            }
        }

        //Create Request
        public async Task<ActionResult<T>> Create(T entity)
        {
            dbSet.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        //Update Request
        public async Task<IActionResult> Update(int id, T entity)
        {
            
            var existingProduct = await dbSet.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _unitOfWork.Context.Entry(existingProduct).CurrentValues.SetValues(entity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        //Delete Request
        public async Task<IActionResult> Delete(int id)
        {
            var data = await dbSet.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            dbSet.Remove(data);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
