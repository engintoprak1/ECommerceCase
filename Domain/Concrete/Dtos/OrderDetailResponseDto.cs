using Domain.Abstract;

namespace Domain.Concrete.Dtos;

public class OrderDetailResponseDto : IDto
{
    public decimal TotalAmount { get; set; }
}
