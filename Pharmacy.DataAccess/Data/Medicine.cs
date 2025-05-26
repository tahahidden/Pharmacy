using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccess.Data;

public partial class Medicine
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? Price { get; set; }

    public int? WarningLevel { get; set; }

    public virtual ICollection<Medicinesinventory> Medicinesinventories { get; set; } = new List<Medicinesinventory>();

    public virtual ICollection<Shoppingcartitem> Shoppingcartitems { get; set; } = new List<Shoppingcartitem>();
}
