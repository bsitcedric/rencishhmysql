using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenciModule
{
    class Query //Just Change The NameSpace
    {
        private string query;
        private Dictionary<string, object> parameters;

    
        public Query(string query)
        {
            this.query = query;
            parameters = new Dictionary<string, object>();
        }

        public string getQuery()
        {
            return query;
        }

        public Dictionary<string, object> getParameters()
        {
            return parameters;
        }
    }
}
