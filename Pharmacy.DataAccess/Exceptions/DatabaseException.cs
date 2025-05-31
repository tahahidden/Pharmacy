using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.DataAccess.Exceptions
{
    public class DatabaseException : Exception
    {
        public int _type;
        public DatabaseException(string message, int type) : base(message)
        {
            _type = type;
        }
    }
}