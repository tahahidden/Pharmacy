using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.DataAccess.Exceptions
{
    public class NotFoundException : Exception
    {
        public int _type;
        public NotFoundException(string message, int type) : base(message)
        {
            _type = type;
        }
    }
}