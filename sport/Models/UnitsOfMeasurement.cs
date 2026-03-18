using System;
using System.Collections.Generic;

namespace sport.Models;

public partial class UnitsOfMeasurement
{
    public int Id { get; set; }

    public string? UnitOfMeasurement { get; set; }

    public virtual ICollection<SportingGood> SportingGoods { get; set; } = new List<SportingGood>();
}
