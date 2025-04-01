using Domain.Concrete.Dtos.Order;
using Domain.Concrete.Entities.Order;
using Domain.Results;

namespace Business.Abstract;

public interface IOrderService
{
    Task<IDataResult<CreateOrderResponseDto>> CreateOrder(OrderInfoDto orderInfoDto);

    Task<IDataResult<List<Order>>> GetAll();

    Task<IDataResult<List<Order>>> GetAllOrdersByUserId(int userId);

    Task<IResult> DeleteOrder(int orderId);
}
