using System;

namespace Extensions
{
    [Serializable]
    public class CustomKeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }
}