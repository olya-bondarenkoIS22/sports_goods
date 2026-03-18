using System;
using System.Collections.Generic;

namespace sport.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Manufacturer1 { get; set; } = null!;

    public virtual ICollection<SportingGood> SportingGoods { get; set; } = new List<SportingGood>();
}
