using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elFinder.AspNet
{
    public class ConnectorResult
    {
        private readonly object _value;

        public ConnectorResult(object value)
        {
            _value = value;
        }

        public object Value
        {
            get { return Succeeded ? _value : new { error = _value }; }
        }

        public bool Succeeded
        {
            get { return !(_value is string || _value is IEnumerable<string>); }
        }
    }
}
