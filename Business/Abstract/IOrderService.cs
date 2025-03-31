using Domain.Concrete.Dtos;
using Domain.Concrete.Entities;
using Domain.Results;

namespace Application.Abstract;

public interface IOrderService
{
    Task<IDataResult<CreateOrderResponseDto>> CreateOrder(OrderInfoDto orderInfoDto);

    Task<IDataResult<List<Order>>> GetAllOrdersByUserId(int userId);

    Task<IResult> DeleteOrder(int orderId);
}
