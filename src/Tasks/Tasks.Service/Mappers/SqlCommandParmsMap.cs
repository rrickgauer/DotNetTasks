using MySql.Data.MySqlClient;
using System.Collections;

namespace Tasks.Service.Mappers;

public class SqlCommandParmsMap : IEnumerable
{
    private Dictionary<string, object?> _parms = new();
    public Dictionary<string, object?> Parms => _parms;

    /// <summary>
    /// Constructor
    /// </summary>
    public SqlCommandParmsMap() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parms"></param>
    SqlCommandParmsMap(Dictionary<string, object?> parms)
    {
        _parms = parms;
    }

    /// <summary>
    /// Add new element
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Add(string key, object? value)
    {
        if (_parms.ContainsKey(key))
        {
            return false;
        }

        _parms.Add(key, value);
        return true;
    }

    /// <summary>
    /// Loop through parms
    /// </summary>
    /// <returns></returns>
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
