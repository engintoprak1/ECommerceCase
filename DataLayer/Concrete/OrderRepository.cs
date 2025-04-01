using DataLayer.Abstract;
using DataLayer.Context;
using Domain.Concrete.Entities.Order;

namespace DataLayer.Concrete;

public class OrderRepository : GenericRepository<Order>,IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
