namespace WaterSortPuzzle.Models
{
    internal class CollisionDictionary<TKey, TValue> //: IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, List<TValue>> data = new();
        public bool ContainsKey(TKey key)
        {
            return this.data.ContainsKey(key);
        }
        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                data[key].Add(value);
            }
            else
            {
                data.Add(key, new List<TValue>() { value });
            }
        }
        public List<TValue> this[TKey key]
        {
            get => data[key];
        }

        public Dictionary<TKey, List<TValue>> DebugData
        {
            get { return data; }
        }
    }
}