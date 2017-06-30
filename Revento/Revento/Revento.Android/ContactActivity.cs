using System;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Revento.Droid
{
    [Activity(Label = "Contact")]
    public class ContactForm : Activity
    {
        private TextView _dateDisplay;
        private Button _dateSelect;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Contact);

            _dateDisplay = FindViewById<TextView>(Resource.Id.date_display);
            _dateSelect = FindViewById<Button>(Resource.Id.date_button);
            _dateSelect.Click += DateSelect_OnClick;

            // Toon een backbutton in de actionbar
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate(DateTime time) { _dateDisplay.Text = time.ToLongDateString(); });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        // Override de functionaliteit van de backbutton zodat het teruggaat naar de mainactivity
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }

    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        private Action<DateTime> _dateSelectHandler = delegate { };

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle bundle)
        {
            DateTime currently = DateTime.Now;

            DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month, currently.Day);

            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int month, int day)
        {
            DateTime selectedDate = new DateTime(year, month + 1, day);

            Log.Debug(TAG, selectedDate.ToLongDateString());

            _dateSelectHandler(selectedDate);
        }
    }
}
