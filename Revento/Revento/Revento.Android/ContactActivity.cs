using System;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Content;
using Android.Widget;

namespace Revento.Droid
{
    //Show the contact activity, this activity contains an input form and is connected to email
    [Activity(Label = "Contact")]
    public class ContactForm : Activity
    {
        private TextView _dateDisplay;
        private Button _dateSelect, _submit;
        private EditText _eventTitle, _eventDescription, _eventAddress, _eventWebsite;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Contact);
            
            // Zoek onze items om een evenement te submitten
            _eventTitle = FindViewById<EditText>(Resource.Id.EventTitle);
            _dateDisplay = FindViewById<TextView>(Resource.Id.date_display);
            _eventDescription = FindViewById<EditText>(Resource.Id.EventDescription);
            _eventAddress = FindViewById<EditText>(Resource.Id.EventAddress);
            _eventWebsite = FindViewById<EditText>(Resource.Id.EventWebsite);

            /// <summary>
            /// Dit is om onze datum te selecteren
            /// </summary>

            _dateSelect = FindViewById<Button>(Resource.Id.date_button);
            _dateSelect.Click += DateSelect_OnClick;

            _submit = FindViewById<Button>(Resource.Id.submit);
            _submit.Click += SubmitButonOnclick;

            // Toon een backbutton in de actionbar
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        private void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) { _dateDisplay.Text = time.ToLongDateString(); });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void SubmitButonOnclick(object sender, EventArgs e)
        {
            if (_eventTitle.Text.Length > 0 && _dateDisplay.Text.Length > 0 && _eventDescription.Text.Length > 0 && _eventAddress.Text.Length > 0 && _eventWebsite.Text.Length > 0)
            {
                //XmlWriter.AddEvent(this, "events.xml", _eventTitle.Text, _dateDisplay.Text, _eventDescription.Text, _eventAddress.Text, _eventWebsite.Text);
                var email = new Intent(Android.Content.Intent.ActionSend);
                email.PutExtra(Android.Content.Intent.ExtraEmail,
                new string[] { "reventoevents@gmail.com" });

                email.PutExtra(Android.Content.Intent.ExtraSubject, "Evenement insturen");

                email.PutExtra(Android.Content.Intent.ExtraText,
                "Geachte redactie van Revento, \n \n" + "Titel van evenement: " + _eventTitle.Text + "\nDatum van evenement: " + _dateDisplay.Text +
                "\n" + "Beschrijving van evenement: " + _eventDescription.Text + "\nAdres van evenement: " + _eventAddress.Text + "\nWebsite van het evenement: " + _eventWebsite.Text);
                email.SetType("message/rfc822");
                try
                {
                    StartActivity(email);
                    Finish();
                }
                catch
                {
                    Toast.MakeText(this, "Sorry can't send email", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Please fill in all fields", ToastLength.Short).Show();
            }
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
