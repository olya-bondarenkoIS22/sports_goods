using System;
using System.Collections.Generic;

namespace sport.Models;

public partial class OrdersSportingGood
{
    public int Id { get; set; }

    public int IdOrder { get; set; }

    public int IdSportingGoods { get; set; }

    public int? Quantity { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual SportingGood IdSportingGoodsNavigation { get; set; } = null!;
}
