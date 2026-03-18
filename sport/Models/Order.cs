using System;
using System.Collections.Generic;

namespace sport.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public int? IdDeliveryPointAddress { get; set; }

    public int IdUser { get; set; }

    public string? Code { get; set; }

    public int IdStatus { get; set; }

    public virtual AddressesOfPickUpPoint? AddressesOfPickUpPoint { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<OrdersSportingGood> OrdersSportingGoods { get; set; } = new List<OrdersSportingGood>();
}
