using Domain.Abstract;

namespace Domain.Concrete.Entities.Order;

public sealed class Order : Entity
{
    public int UserID { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime OrderDate { get; set; }
}
