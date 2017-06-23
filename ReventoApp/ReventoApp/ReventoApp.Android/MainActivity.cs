using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace ReventoApp.Droid
{
	[Activity (Label = "ReventoApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            
           
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            
            // Get our button from the layout resource,
            // and attach an event to it
            //To Create a new button
            ViewFactory View_Factory = new ConcreteViewFactory();
            ViewAdapter ViewCreator = new ViewAdapter(this);

            View Text; 
            View _Button, AnotherButton;
            _Button = ViewCreator.setView(Resource.Id.FirstButton);
            Text = ViewCreator.setView(Resource.Id.textView);
            AnotherButton = ViewCreator.setView(Resource.Id.AnotherButton);

            AnotherButton.Enabled = false;
            _Button.Click += (object sender, EventArgs e) => { AnotherButton.Enabled = true; _Button.Enabled = false; };
            AnotherButton.Click += (object sender, EventArgs e) => { _Button.Enabled = true; AnotherButton.Enabled = false; };

            //View_Factory.Create(1, this, Resource.Id.FirstButton).Visit((v)=> {
            //    _Button = v; _Button.Enabled =true;
            //    _Button.Click += (object sender, EventArgs e) => { _Button.Enabled = false; };

            //    View_Factory.Create(1, this, Resource.Id.AnotherButton).Visit((v1) => { AnotherButton = v1; AnotherButton.Enabled = true; AnotherButton.Click += (object sender, EventArgs e) => { _Button.Enabled = true; }; }, () => { });
            //    View_Factory.Create(2, this, Resource.Id.textView).Visit((v2) => { Text = v2; }, () => { });
            //},()=> { });
        }
	}
}


