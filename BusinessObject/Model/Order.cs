using System;
using System.Collections.Generic;

namespace BusinessObject.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public decimal? Freight { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public int MemberId { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
