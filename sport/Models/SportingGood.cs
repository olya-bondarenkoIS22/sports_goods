using System;
using System.Collections.Generic;

namespace sport.Models;

public partial class SportingGood
{
    public int Id { get; set; }

    public string Article { get; set; } = null!;

    public int IdCategory { get; set; }

    public string? Name { get; set; }

    public int IdManufacturer { get; set; }

    public int IdSupplier { get; set; }

    public int? Price { get; set; }

    public int? IdUnitOfMeasurement { get; set; }

    public int? Discount { get; set; }

    public int? QuantityInStock { get; set; }

    public string? Description { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!;

    public virtual Supplier IdSupplierNavigation { get; set; } = null!;

    public virtual UnitsOfMeasurement? IdUnitOfMeasurementNavigation { get; set; }

    public virtual ICollection<OrdersSportingGood> OrdersSportingGoods { get; set; } = new List<OrdersSportingGood>();
}
