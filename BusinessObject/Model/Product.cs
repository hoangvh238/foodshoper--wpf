using System;
using System.Collections.Generic;

namespace BusinessObject.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }

    public string Weight { get; set; } = null!;

    public int UnitsInStock { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
