using Domain.Abstract;

namespace Domain.Concrete.Entities;

public sealed class Book : Entity
{
    public string Title { get; set; } = default!;
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
