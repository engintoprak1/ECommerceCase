using Application.Abstract;
using Business.Abstract;
using DataLayer.Abstract;
using Domain.Concrete.Dtos;
using Domain.Concrete.Entities;
using Domain.Results;

namespace Application.Concrete;

public class OrderService(IOrderRepository orderRepository, IOrderDetailService orderDetailService) : IOrderService
{
    public async Task<IDataResult<CreateOrderResponseDto>> CreateOrder(OrderInfoDto orderInfoDto)
    {
        Order createForOrder = new Order
        {
            UserID = orderInfoDto.UserId,
            Status = "Created.",
            OrderDate = orderInfoDto.OrderCreatedDate,
        };

        await orderRepository.AddAsync(createForOrder);

        IDataResult<OrderDetailResponseDto> orderDetails = await orderDetailService.CreateOrderDetails(orderInfoDto.Details, createForOrder.Id);

        createForOrder.TotalAmount = orderDetails.Data.TotalAmount;

        await orderRepository.UpdateAsync(createForOrder);

        return new SuccessDataResult<CreateOrderResponseDto>(new CreateOrderResponseDto() { OrderId = createForOrder.Id, TotalAmount = createForOrder.TotalAmount });
    }
    

    public async Task<IDataResult<List<Order>>> GetAllOrdersByUserId(int userId)
    {
        List<Order> userOrders = await orderRepository.GetAll(x => x.UserID == userId);

        if (!userOrders.Any())
        {
            return new ErrorDataResult<List<Order>>("Order not found.");
        }

        return new SuccessDataResult<List<Order>>(userOrders);
    }

    public async Task<IResult> DeleteOrder(int orderId)
    {
        return await orderRepository.DeleteAsync(orderId); ;
    }
}
