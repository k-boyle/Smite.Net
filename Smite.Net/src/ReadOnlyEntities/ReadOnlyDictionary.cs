﻿using System.Collections;
using System.Collections.Generic;

namespace Smite.Net.ReadOnlyEntities
{
    public sealed class ReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dict;

        internal ReadOnlyDictionary(IDictionary<TKey, TValue> dict)
        {
            _dict = dict;
        }

        public TValue this[TKey key] => _dict[key];

        public IEnumerable<TKey> Keys => _dict.Keys;

        public IEnumerable<TValue> Values => _dict.Values;

        public int Count => _dict.Count;

        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
    }
}
