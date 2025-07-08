using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzle.Models
{
    public sealed class TrulyObservableCollection<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged
    {
        public TrulyObservableCollection()
        {
            CollectionChanged += FullObservableCollectionCollectionChanged;
        }

        public TrulyObservableCollection(IEnumerable<T> pItems) : this()
        {
            foreach (var item in pItems)
            {
                this.Add(item);
            }
        }

        private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                //if (e.NewItems.Count <= 1)
                //{
                //    ((INotifyPropertyChanged)e.NewItems).PropertyChanged += ItemPropertyChanged;
                //}
                //else
                //{
                //    foreach (Object item in e.NewItems)
                //    {
                //        ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
                //    }
                //}
                foreach (Object item in e.NewItems)
                {
                    //if (item is not null)
                        ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
                    //OnPropertyChanged("nameof(item)");
                }
            }
            if (e.OldItems != null)
            {
                //if (e.NewItems.Count <= 1)
                //{
                //    ((INotifyPropertyChanged)e.OldItems).PropertyChanged -= ItemPropertyChanged;
                //}
                //else
                //{
                //    foreach (Object item in e.OldItems)
                //    {
                //        ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
                //    }
                //}
                foreach (Object item in e.OldItems)
                {
                    //if (item is not null)
                        ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
            OnCollectionChanged(args);
        }
    }
}
