namespace Domain.Concrete.Dtos;

public class CreateOrderResponseDto
{
    public int OrderId { get; set; }
    public decimal TotalAmount { get; set; }
}
