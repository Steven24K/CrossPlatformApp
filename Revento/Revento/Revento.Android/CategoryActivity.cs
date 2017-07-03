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
using System.IO;
using System.Xml.Linq;

namespace Revento.Droid
{

    [Activity(Label = "Category")]
    public class CategoryActivity : ListActivity
    {
        private string title = MainActivity.category;
        string[] EventTitle, EventDate, EventDescription, EventAddress, EventWebsite;
        private string category = MainActivity.category;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Category);

            this.Title = title;

            // Toon een backbutton in de actionbar
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            // Open het xml bestand vanuit de assets folder
            Stream xmldoc = Assets.Open("events.xml");

            // Lees het xml bestand door van voor naar achteren -> je krijgt hierbij de xml tags
            StreamReader reader = new StreamReader(xmldoc);
            string readertext = reader.ReadToEnd();

            // Maak van de gelezen xml een XDocument object
            XDocument xdoc = XDocument.Parse(readertext);

            // Laat onze XMLProcessor alle informatie uit het xml bestand halen
            EventTitle = XMLProcesser.SendXMLCategory(xdoc, "title", category);
            EventDate = XMLProcesser.SendXMLCategory(xdoc, "date", category);
            EventDescription = XMLProcesser.SendXMLCategory(xdoc, "description", category);
            EventAddress = XMLProcesser.SendXMLCategory(xdoc, "address", category);
            EventWebsite = XMLProcesser.SendXMLCategory(xdoc, "website", category);

            // Toon de titel en datum van het evenement in de lijst van het homescreen
            this.ListAdapter = new MainScreenAdapter(EventTitle, EventDate, this);
        }

        protected override void OnListItemClick(ListView lv, View v, int position, long id)
        {
            var t = EventTitle[position];

            var NextActivity = new Intent(this, typeof(DetailsActivity));

            // Voeg alle items vanuit het xml bestand toe aan het detailedview activity
            NextActivity.PutExtra("title", EventTitle[id]);
            NextActivity.PutExtra("date", EventDate[id]);
            NextActivity.PutExtra("description", EventDescription[id]);
            NextActivity.PutExtra("address", EventAddress[id]);
            NextActivity.PutExtra("website", EventWebsite[id]);

            StartActivity(NextActivity);
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
}