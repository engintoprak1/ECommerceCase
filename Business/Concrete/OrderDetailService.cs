using Business.Abstract;
using DataLayer.Abstract;
using DataLayer.Concrete;
using Domain.Concrete.Dtos;
using Domain.Concrete.Entities;
using Domain.Results;

namespace Business.Concrete;

public class OrderDetailService(IOrderDetailRepository orderDetailRepository, IBookRepository bookRepository) : IOrderDetailService
{
    public async Task<IDataResult<List<OrderDetail>>> GetOrderDetailsByOrderId(int orderId)
    {
        List<OrderDetail> orderDetails = await orderDetailRepository.GetAll(x => x.OrderID == orderId);

        if (!orderDetails.Any())
        {
            return new ErrorDataResult<List<OrderDetail>>("Order detail not found.");
        }

        return new SuccessDataResult<List<OrderDetail>>(orderDetails);
    }

    public async Task<IDataResult<OrderDetailResponseDto>> CreateOrderDetails(List<OrderDetailDto> orderDetails, int orderId)
    {
        decimal totalAmount = 0;
        foreach (var item in orderDetails)
        {
            Book book = await bookRepository.GetByIdAsync(item.BookId);

            if (book == null)
            {
                return new ErrorDataResult<OrderDetailResponseDto>("Book not found.");
            }

            if (book.Stock < 1)
            {
                return new ErrorDataResult<OrderDetailResponseDto>("Stock is not enough.");
            }


            OrderDetail orderDetail = new OrderDetail()
            {
                BookID = item.BookId,
                OrderID = orderId,
                Quantity = item.Quantity,
                SubTotal = book.Price * item.Quantity
            };

            totalAmount += orderDetail.SubTotal;

            //bu işlem performansı düşürebilir. fakat bu senaryoda bir problem olmaz.
            await orderDetailRepository.AddAsync(orderDetail);
        }

        return new SuccessDataResult<OrderDetailResponseDto>(new OrderDetailResponseDto { TotalAmount = totalAmount });
    }
}
