namespace Domain.Concrete.Dtos.Order;

public class CreateOrderResponseDto
{
    public int OrderId { get; set; }
    public decimal TotalAmount { get; set; }
}
