using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Infra.Utils
{
    public class DbParameter
    {
        // ---------------------------------------------------------------------------------------------
        // Private members
        // ---------------------------------------------------------------------------------------------

        private List<KeyValuePair<string, object>> m_parameters = null;

        // ---------------------------------------------------------------------------------------------
        // Constructor
        // ---------------------------------------------------------------------------------------------

        public DbParameter()
        {
            this.m_parameters = new List<KeyValuePair<string, object>>();
        }

        public DbParameter(string paramName, object paramValue)
        {
            this.m_parameters = new List<KeyValuePair<string, object>>();
            this.m_parameters.Add(new KeyValuePair<string, object>(paramName.Trim(), paramValue));
        }

        // ---------------------------------------------------------------------------------------------
        // Public methods
        // ---------------------------------------------------------------------------------------------

        public void clear()
        {
            this.m_parameters.Clear();
        }

        public void add(string paramName, object paramValue)
        {
            this.m_parameters.Add(new KeyValuePair<string, object>(paramName.Trim(), paramValue));
        }

        // ---------------------------------------------------------------------------------------------
        // Properties
        // ---------------------------------------------------------------------------------------------

        public List<KeyValuePair<string, object>> List
        {
            get { return this.m_parameters; }
        }
    }
}
