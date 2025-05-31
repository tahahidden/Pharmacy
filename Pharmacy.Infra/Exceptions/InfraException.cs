using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Infra.Exceptions
{
    public class InfraException : Exception
    {
        public int _type;
        public InfraException(string message, int type) : base(message)
        {
            _type = type;
        }
    }
}