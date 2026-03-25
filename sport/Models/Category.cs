using sport.Models;
using System;
using System.Collections.Generic;

namespace sport;

public partial class Category
{
    public int Id { get; set; }

    public string? Category1 { get; set; }

    public virtual ICollection<SportingGood> SportingGoods { get; set; } = new List<SportingGood>();

    public override string ToString()
    {
        return Category1 ?? string.Empty;
    }
}
