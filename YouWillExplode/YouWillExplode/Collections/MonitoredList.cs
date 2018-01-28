namespace YouWillExplode.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="List{T}"/> which exposes a <see cref="Added"/> event.
    /// </summary>
    /// <typeparam name="T">Type contained in the list.</typeparam>
    internal sealed class MonitoredList<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        private List<T> list = new List<T>();

        public event Action<T> Added, Removed;

        public event Action Cleared;

        public int Count => this.list.Count;

        public bool IsFixedSize => ((IList)this.list).IsFixedSize;

        public bool IsReadOnly => ((IList)this.list).IsReadOnly;

        public bool IsSynchronized => ((ICollection)this.list).IsSynchronized;

        public object SyncRoot => ((ICollection)this.list).SyncRoot;

        public T this[int index] => ((IReadOnlyList<T>)this.list)[index];

        object IList.this[int index]
        {
            get
            {
                return ((IList)this.list)[index];
            }

            set
            {
                object oldValue = ((IList)this.list)[index];
                ((IList)this.list)[index] = value;

                // Setting a value to itself also counts as a change
                this.Removed?.Invoke((T)oldValue);
                this.Added?.Invoke((T)value);
            }
        }

        T IList<T>.this[int index]
        {
            get
            {
                return ((IList<T>)this.list)[index];
            }

            set
            {
                T oldValue = this.list[index];
                this.list[index] = value;

                // Setting a value to itself also counts as a change
                this.Removed?.Invoke(oldValue);
                this.Added?.Invoke(value);
            }
        }

        public void Add(T item)
        {
            this.list.Add(item);
            this.Added?.Invoke(item);
        }

        int IList.Add(object value)
        {
            int result = ((IList)this.list).Add(value);
            this.Added?.Invoke((T)value);
            return result;
        }

        public void Clear()
        {
            this.list.Clear();
            this.Cleared?.Invoke();
        }

        public bool Contains(T item) =>
            this.list.Contains(item);

        bool IList.Contains(object value) =>
            ((IList)this.list).Contains(value);

        void ICollection.CopyTo(Array array, int index) =>
            ((ICollection)this.list).CopyTo(array, index);

        public void CopyTo(T[] array, int arrayIndex) =>
            this.list.CopyTo(array, arrayIndex);

        IEnumerator IEnumerable.GetEnumerator() =>
            this.list.GetEnumerator();

        public IEnumerator<T> GetEnumerator() =>
            this.list.GetEnumerator();

        public int IndexOf(T item) =>
            this.list.IndexOf(item);

        int IList.IndexOf(object value) =>
            ((IList)this.list).IndexOf(value);

        public void Insert(int index, T item)
        {
            T oldValue = this.list[index];
            this.list.Insert(index, item);

            this.Removed?.Invoke(oldValue);
            this.Added?.Invoke(item);
        }

        void IList.Insert(int index, object value)
        {
            object oldValue = ((IList)this.list)[index];
            ((IList)this.list).Insert(index, value);

            this.Removed?.Invoke((T)oldValue);
            this.Added?.Invoke((T)value);
        }

        public bool Remove(T item)
        {
            bool result = this.list.Remove(item);
            if (result)
            {
                // Only remove if removal succeeded
                this.Removed?.Invoke(item);
            }

            return result;
        }

        void IList.Remove(object value)
        {
            ((IList)this.list).Remove(value);
            this.Removed?.Invoke((T)value);
        }

        public void RemoveAt(int index)
        {
            T value = this.list[index];
            this.list.RemoveAt(index);
            this.Removed?.Invoke(value);
        }
    }
}