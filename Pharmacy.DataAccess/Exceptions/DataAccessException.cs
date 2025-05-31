using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.DataAccess.Exceptions
{
    public class DataAccessException : Exception
    {
        public int _type;
        public DataAccessException(string message, int type) : base(message)
        {
            _type = type;
        }
    }
}