using Domain.Concrete.Entities;
using Domain.Results;
using System.Linq.Expressions;

namespace DataLayer.Abstract;

public interface IGenericRepository<T> where T : Entity
{
    Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
    Task<T> GetByIdAsync(int id);
    Task<IResult> AddAsync(T entity);
    Task<IResult> UpdateAsync(T entity);
    Task<IResult> DeleteAsync(int id);
}
