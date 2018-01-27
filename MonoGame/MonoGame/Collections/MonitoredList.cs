namespace MonoGame.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="List{T}"/> which exposes a <see cref="Changed"/> event.
    /// </summary>
    /// <typeparam name="T">Type contained in the list.</typeparam>
    internal sealed class MonitoredList<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        private List<T> list = new List<T>();

        public event Action Changed;

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
                ((IList)this.list)[index] = value;

                // Setting a value to itself also counts as a change
                this.Changed?.Invoke();
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
                ((IList<T>)this.list)[index] = value;

                // Setting a value to itself also counts as a change
                this.Changed?.Invoke();
            }
        }

        public void Add(T item)
        {
            this.list.Add(item);
            this.Changed?.Invoke();
        }

        int IList.Add(object value)
        {
            int result = ((IList)this.list).Add(value);
            this.Changed?.Invoke();
            return result;
        }

        public void Clear()
        {
            this.list.Clear();
            this.Changed?.Invoke();
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        bool IList.Contains(object value)
        {
            return ((IList)this.list).Contains(value);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)this.list).CopyTo(array, index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        int IList.IndexOf(object value)
        {
            return ((IList)this.list).IndexOf(value);
        }

        public void Insert(int index, T item)
        {
            this.list.Insert(index, item);
            this.Changed?.Invoke();
        }

        void IList.Insert(int index, object value)
        {
            ((IList)this.list).Insert(index, value);
            this.Changed?.Invoke();
        }

        public bool Remove(T item)
        {
            bool result = this.list.Remove(item);
            this.Changed?.Invoke();
            return result;
        }

        void IList.Remove(object value)
        {
            ((IList)this.list).Remove(value);
            this.Changed?.Invoke();
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
            this.Changed?.Invoke();
        }
    }
}