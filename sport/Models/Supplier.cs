using System;
using System.Collections.Generic;

namespace sport.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? Supplier1 { get; set; }

    public virtual ICollection<SportingGood> SportingGoods { get; set; } = new List<SportingGood>();

    public override string ToString()
    {
        return Supplier1 ?? string.Empty;
    }
}
