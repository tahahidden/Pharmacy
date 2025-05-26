using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccess.Data;

public partial class Shoppingcartitem
{
    public long Id { get; set; }

    public long ShoppingCartId { get; set; }

    public long MedicineId { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual Shoppingcart ShoppingCart { get; set; } = null!;
}
