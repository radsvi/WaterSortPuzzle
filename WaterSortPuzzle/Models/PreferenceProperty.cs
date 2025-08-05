namespace WaterSortPuzzle.Models
{
    public class PreferenceProperty<T>
    {
        private readonly string key;
        private readonly T defaultValue;

        public PreferenceProperty(string key, T defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public T Value
        {
            get => Preferences.Default.Get(key, defaultValue);
            set => Preferences.Default.Set(key, value);
        }
    }
}