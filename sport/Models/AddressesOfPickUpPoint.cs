using sport.Models;
using System;
using System.Collections.Generic;

namespace sport;

public partial class AddressesOfPickUpPoint
{
    public int Id { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        return Address.ToString();
    }
}
