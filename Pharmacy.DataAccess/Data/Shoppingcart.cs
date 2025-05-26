using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccess.Data;

public partial class Shoppingcart
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Shoppingcartitem> Shoppingcartitems { get; set; } = new List<Shoppingcartitem>();
}
