using Domain.Abstract;
using System.Text.Json.Serialization;

namespace Domain.Concrete.Dtos.Order;

public class OrderDetailDto : IDto
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}
