using Domain.Abstract;
using System.Text.Json.Serialization;

namespace Domain.Concrete.Dtos.Order;

public sealed class OrderInfoDto : IDto
{
    [JsonIgnore]
    public int UserId { get; set; } = 1; //kullanıcı zaten giriş yapmış olacağı için default bir değer verdim.
    public List<OrderDetailDto> Details { get; set; } = new List<OrderDetailDto>();
    public DateTime OrderCreatedDate { get; set; } = DateTime.Now;
}
