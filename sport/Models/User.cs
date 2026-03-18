using System;
using System.Collections.Generic;

namespace sport.Models;

public partial class User
{
    public int Id { get; set; }

    public int IdRole { get; set; }

    public string? FullName { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
