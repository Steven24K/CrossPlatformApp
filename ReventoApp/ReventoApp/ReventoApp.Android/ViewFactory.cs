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
    public abstract class ViewFactory
    {
        public abstract IOption<View> Create(int id, Activity activity, int resource);
    }

    public class ConcreteViewFactory : ViewFactory
    {
        public override IOption<View> Create(int id, Activity activity, int resource)
        {
            switch (id)
            {
                case 1:
                    return new Some<View>(activity.FindViewById<Button>(resource));
                case 2:
                    return new Some<View>(activity.FindViewById<TextView>(resource));
                case 3:
                    return new Some<View>(activity.FindViewById<TextClock>(resource));
                default:
                    return new None<View>();
            }
        }
    }
}