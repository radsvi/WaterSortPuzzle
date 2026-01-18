namespace WaterSortPuzzle.Models
{
    internal class CollisionDictionary<TKey, TValue> where TKey : struct //: IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, List<TValue>> data = [];
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