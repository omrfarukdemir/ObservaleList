using System;
using System.Collections.Generic;

namespace ObservableList
{
    public class NotifyListChangedEventArgs<T> : EventArgs
    {
        public T Item { get; set; }
        public IEnumerable<T> Items { get; set; }
        public NotifyListChangedType Action { get; set; }

        public NotifyListChangedEventArgs(T item, IEnumerable<T> items, NotifyListChangedType action)
        {
            Item = item;
            Items = items;
            Action = action;
        }

        public void ForEachItems(Action<T> action)
        {
            foreach (var item in Items)
            {
                action.Invoke(item);
            }
        }
    }
}