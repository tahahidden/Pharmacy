using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccess.Data;

public partial class Medicinesinventory
{
    public long Id { get; set; }

    public long MedicineId { get; set; }

    public long? Count { get; set; }

    public DateTime ExpirationDate { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
