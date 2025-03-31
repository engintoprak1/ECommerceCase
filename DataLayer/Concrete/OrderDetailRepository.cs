using DataLayer.Abstract;
using DataLayer.Context;
using Domain.Concrete.Entities;

namespace DataLayer.Concrete;

public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
