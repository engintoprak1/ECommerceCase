using Domain.Abstract;

namespace Domain.Concrete.Dtos.Order;

public class OrderDetailResponseDto : IDto
{
    public decimal TotalAmount { get; set; }
}
