using MySql.Data.MySqlClient;
using System.Collections;

namespace Tasks.Mappers
{
    public class SqlCommandParmsMap : IEnumerable
    {
        private Dictionary<string, object?> _parms = new();
        public Dictionary<string, object?> Parms => _parms;

        public SqlCommandParmsMap() { }

        SqlCommandParmsMap(Dictionary<string, object?> parms)
        {
            _parms = parms;
        }

        public bool Add(string key, object? value)
        {
            if (_parms.ContainsKey(key))
            {
                return false;
            }

            _parms.Add(key, value);
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            foreach(var x in _parms)
            {
                yield return x;
            }    
        }

        /// <summary>
        /// Add the current parms to the specified command
        /// </summary>
        /// <param name="command"></param>
        public void AddParmsToCommand(MySqlCommand command)
        {
            foreach (var parm in _parms)
            {
                command.Parameters.AddWithValue(parm.Key, parm.Value);
            }
        }
    }
}
