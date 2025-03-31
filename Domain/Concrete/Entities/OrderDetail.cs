﻿namespace Domain.Concrete.Entities;

public class OrderDetail : Entity
{
    public int OrderID { get; set; }
    public int BookID { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}
