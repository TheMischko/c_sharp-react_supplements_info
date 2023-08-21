namespace SupplementsServer.API.Helpers.CsvParser; 

public class CsvResult {
    private Dictionary<string, object> _values;

    public CsvResult() {
        _values = new Dictionary<string, object>();
    }

    public object? GetValue(string key) {
        if (_values.ContainsKey(key))
            return _values[key];
        return null;
    }

    public List<string> GetKeys() {
        return _values.Keys.ToList();
    }

    public void AddValue(string key, object value) {
        if (_values.ContainsKey(key)) {
            _values[key] = value;
            return;
        }
        _values.Add(key, value);
    }

    public bool HasSameKeys(CsvResult target) {
        List<string> selfKeys = GetKeys();
        foreach (string key in target.GetKeys()) {
            if (!selfKeys.Contains(key)) return false;
        }
        return true;
    }
}