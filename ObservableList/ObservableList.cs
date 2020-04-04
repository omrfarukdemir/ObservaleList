using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ObservableList
{
    public class ObservableList<T> : IList<T>
    {
        private readonly List<T> _collection;

        public event EventHandler<NotifyListChangedEventArgs<T>> ListChanged;
        public ObservableList(int capasity)
        {
            _collection = new List<T>(capasity);
        }

        public ObservableList()
        {
            _collection = new List<T>();
        }
        public T this[int index] { get => _collection[index]; set => _collection[index] = value; }

        public int Count => _collection.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {

            _collection.Add(item);

            var @event = new NotifyListChangedEventArgs<T>(item, null, NotifyListChangedType.Add);

            OnListChanged(@event);
        }

        public void AddRange(T[] items)
        {
            _collection.AddRange(items);

            var @event = new NotifyListChangedEventArgs<T>(default(T), items, NotifyListChangedType.AddRange);

            OnListChanged(@event);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(T item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _collection.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _collection.Insert(index, item);

            var @event = new NotifyListChangedEventArgs<T>(item, null, NotifyListChangedType.Insert);

            OnListChanged(@event);
        }

        public bool Remove(T item)
        {

            if (_collection.Remove(item))
            {
                var @event = new NotifyListChangedEventArgs<T>(item, null, NotifyListChangedType.Remove);

                OnListChanged(@event);

                return true;
            }

            return false;
        }

        public void RemoveRange(int index, int count)
        {
            var removedItems = _collection.Skip(index).Take(count).ToList();

            //Array.Copy(_collection.ToArray(), index, removedItems.ToArray(), 0, count);

            _collection.RemoveRange(index, count);

            var @event = new NotifyListChangedEventArgs<T>(default(T), removedItems, NotifyListChangedType.RemoveRange);

            OnListChanged(@event);
        }

        public void RemoveAt(int index)
        {
            var removedItem = _collection[index];

            _collection.RemoveAt(index);

            var @event = new NotifyListChangedEventArgs<T>(removedItem, null, NotifyListChangedType.Remove);

            OnListChanged(@event);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        protected void OnListChanged(NotifyListChangedEventArgs<T> e)
        {
            Volatile.Read(ref ListChanged)?.Invoke(this, e);

            //ListChanged.Invoke(this,e);
        }

       
    }
}