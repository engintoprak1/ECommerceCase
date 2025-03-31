using Domain.Concrete.Dtos;
using Domain.Concrete.Entities;
using Domain.Results;

namespace Business.Abstract;

public interface IOrderDetailService
{
    Task<IDataResult<OrderDetailResponseDto>> CreateOrderDetails(List<OrderDetailDto> orderDetails, int orderId);

    Task<IDataResult<List<OrderDetail>>> GetOrderDetailsByOrderId(int orderId);
}
