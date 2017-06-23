using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ReventoApp.Droid
{
    public interface IOption<T>
    {
        void Visit(Action<T> onSome, Action onNone);
        U visit<U>(Func<T,U> onSome, Func<U> onNone);
    }

    public class Some<T> : IOption<T>
    {
        private T Value;
        public Some(T value)
        {
            this.Value = value;
        }

        public void Visit(Action<T> onSome, Action onNone)
        {
            onSome(this.Value);
        }

        public U visit<U>(Func<T, U> onSome, Func<U> onNone)
        {
            return onSome(this.Value);
        }
    }

    public class None<T> : IOption<T>
    {
        public void Visit(Action<T> onSome, Action onNone)
        {
            onNone();
        }

        public U visit<U>(Func<T, U> onSome, Func<U> onNone)
        {
            return onNone();
        }
    }
}