using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.NetFixGateway.Common
{
    public interface IConfigurationStore : IDisposable
    {        
        void Save();
        object GetValue(string key);
        void SetValue(string key, object value);

    }
}
