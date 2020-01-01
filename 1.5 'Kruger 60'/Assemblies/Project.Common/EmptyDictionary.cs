// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.Collections;
using System.Collections.Generic;

namespace IpTviewr.Common
{
    public class EmptyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Implementation of IEnumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            yield break;
        } // GetEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } // IEnumerable.GetEnumerator

        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,TValue>>

        public void Add(KeyValuePair<TKey, TValue> item) => throw new NotSupportedException();

        public void Clear() => throw new NotSupportedException();

        public bool Contains(KeyValuePair<TKey, TValue> item) => false;

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            // no-op
        } // CopyTo

        public bool Remove(KeyValuePair<TKey, TValue> item) => throw new NotSupportedException();

        public int Count => 0;
        public bool IsReadOnly => true;

        #endregion

        #region Implementation of IDictionary<TKey,TValue>

        public bool ContainsKey(TKey key) => false;

        public void Add(TKey key, TValue value) => throw new NotSupportedException();

        public bool Remove(TKey key) => throw new NotSupportedException();

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            return false;
        } // TryGetValue

        public TValue this[TKey key]
        {
            get => throw new KeyNotFoundException();
            set => throw new NotSupportedException();
        } // this[TKey key]

        public ICollection<TKey> Keys => Array.Empty<TKey>();
        public ICollection<TValue> Values => Array.Empty<TValue>();

        #endregion
    } // EmptyDictionary
} // namespace
