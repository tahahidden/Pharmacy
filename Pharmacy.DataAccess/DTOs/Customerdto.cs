using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.DataAccess.DTOs
{
    public class Customerdto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string NationalCode { get; set; }
    }
}