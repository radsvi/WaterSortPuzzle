using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public partial class EmptyTubes : ObservableObject
    {
        public EmptyTubes(AppPreferences appPreferences)
        {
            this.appPreferences = appPreferences;
        }
        private readonly AppPreferences appPreferences;

        private int counter;
        public int Counter
        {
            get { return counter; }
            private set
            {
                if (value != counter && value <= appPreferences.MaximumExtraTubes)
                {
                    counter = value;
                    OnPropertyChanged();
                }
            }
        }
        public void IncrementCounter()
        {
            Counter++;
        }
        public void ResetCounter()
        {
            Counter = 0;
        }
    }
}
