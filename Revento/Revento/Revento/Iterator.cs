using System;
using System.Collections.Generic;
using System.Text;

namespace Revento
{
    public interface IOption<T>
    {
        void Visit(Action<T> onSome, Action onNone);
        U Visit<U>(Func<T, U> onSome, Func<U> onNone);
    }

    public class Some<T> : IOption<T>
    {
        private T Value;
        public Some(T value) { this.Value = value; }
        public void Visit(Action<T> onSome, Action onNone) { onSome(Value); }
        public U Visit<U>(Func<T, U> onSome, Func<U> onNone) { return onSome(Value); }
    }

    public class None<T> : IOption<T>
    {
        public void Visit(Action<T> onSome, Action onNone) { onNone(); }
        public U Visit<U>(Func<T, U> onSome, Func<U> onNone) { return onNone(); }
    }

    public interface Itterator<T>
    {
        IOption<T> GetNext();
        bool hasNext();
        void Add(T item);
        T[] GetCollection();
        int GetAmountOfItems { get; }
    }

    public class ArrayItterator<T> : Itterator<T>
    {
        private T[] _Array;
        private int Current, Size, AmountOfItems;
        public ArrayItterator()
        {
            this.Size = 100;
            this._Array = new T[Size];
            AmountOfItems = 0;
            this.Current = -1;
        }

        public void Add(T item)
        {
            if (AmountOfItems >= Size)
            {
                Size *= 2;
                T[] new_array = new T[Size];
                for (int c = 0; c < _Array.Length; c++)
                {
                    new_array[c] = _Array[c];
                }
                _Array = new_array;
            }
            _Array[AmountOfItems] = item;
            AmountOfItems++;
        }

        public IOption<T> GetNext()
        {
            Current += 1;
            if (hasNext()) return new Some<T>(_Array[Current]);
            return new None<T>();
        }

        public bool hasNext()
        {
            if (Current < _Array.Length | _Array[Current+1] != null) return true;
            return false;
        }

        public T[] GetCollection()
        {
            return _Array;
        }

        public int GetAmountOfItems { get { return this.AmountOfItems; } }
       
    }
}
