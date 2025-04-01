using Domain.Concrete.Dtos.Order;
using Domain.Concrete.Entities.Order;
using Domain.Results;

namespace Business.Abstract;

public interface IOrderDetailService
{
    Task<IDataResult<OrderDetailResponseDto>> CreateOrderDetails(List<OrderDetailDto> orderDetails, int orderId);

    Task<IDataResult<List<OrderDetail>>> GetOrderDetailsByOrderId(int orderId);
}
