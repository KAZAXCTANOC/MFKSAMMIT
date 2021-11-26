using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Manager
{
    public interface IDataBaseControler
    {
        Task<IEnumerable<T>> GetItems<T>(string pointName, Dictionary<string, string> getParams = null);
        Task<string> SendCommand(string pointName, Dictionary<string, string> getParams = null);
    }
}
