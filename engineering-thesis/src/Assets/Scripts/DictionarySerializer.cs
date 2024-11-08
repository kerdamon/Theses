using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DictionarySerializer<TValue> : MonoBehaviour, IEnumerable<KeyValuePair<string, TValue>>
{
    [SerializeField] public float maxValue;
    [SerializeField] private List<string> keys;
    [SerializeField] private List<TValue> values;


    public TValue this[string key]
    {
        get => values[keys.IndexOf(key)];
        set => values[keys.IndexOf(key)] = value;
    }

    public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
    {
        return new DictionarySerializerEnumerator<TValue>(keys, values);
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract bool IsMaxOrGreater(string value);
}

class DictionarySerializerEnumerator<TValue> : IEnumerator<KeyValuePair<string, TValue>>
{
    private List<string> _keys;
    private List<TValue> _values;
    private int _index = -1;

    public DictionarySerializerEnumerator(List<string> keys, List<TValue> values)
    {
        _keys = keys;
        _values = values;
    }

    public bool MoveNext()
    {
        _index++;
        return _index < _keys.Count;
    }

    public void Reset()
    {
        _index = -1;
    }

    object IEnumerator.Current => Current;

    public KeyValuePair<string, TValue> Current => new KeyValuePair<string, TValue>(_keys[_index], _values[_index]);
    public void Dispose()
    {
    }
}