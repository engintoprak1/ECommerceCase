using DataLayer.Abstract;
using DataLayer.Context;
using Domain.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Concrete;

public class BookRepository : GenericRepository<Book>,IBookRepository
{
    public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
