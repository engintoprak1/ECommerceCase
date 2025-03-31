using DataLayer.Abstract;
using DataLayer.Context;
using Domain.Concrete.Entities;
using Domain.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Concrete;

public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : Entity
{
    public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
    {
        
        if (filter == null) 
        { 
            return await dbContext.Set<T>().ToListAsync();
        }
        else
        {
            return await dbContext.Set<T>().Where(filter).ToListAsync();
        }
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id); ;
    }

    public async Task<IResult> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return new SuccessResult("Eklendi.");
    }

    public async Task<IResult> UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        await dbContext.SaveChangesAsync();
        return new SuccessResult("Güncellendi.");
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return new ErrorResult("Kayıt bulunamadı.");
        }

        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
        return new SuccessResult("Kayıt başarıyla silindi.");
    }


}
