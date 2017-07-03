using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Xaml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Revento.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public class ListViewDataItem : ListViewItem
    {
        
    } 

    public class Event
    {
        private XDocument xdoc;

        public XDocument LoadXML(string filepath)
        {
            this.xdoc = XDocument.Load(filepath);

            return xdoc;
        }

        public string[] ParseXML(string attribute)
        {
            string[] _event = XMLProcesser.SendXML(xdoc, attribute);

            return _event;
        }
    }

    public sealed partial class MainPage : Page
    {
        public string[] titles, date, description, website, address;
        public int index;
        public string adres;

        public MainPage()
        {
            string XMLfile = Path.Combine(Package.Current.InstalledLocation.Path, "events.xml");

            Event evenement = new Event();
            XDocument loadedfile = evenement.LoadXML(XMLfile);

            this.titles = evenement.ParseXML("title");
            this.date = evenement.ParseXML("date");
            this.description = evenement.ParseXML("description");
            this.website = evenement.ParseXML("website");
            this.address = evenement.ParseXML("address");

            this.InitializeComponent();

            ListViewEvenementen.ItemsSource = titles;

            
            //Initialize the ToggleSwitch for roaming settings
            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("FavorietEvenementToggle"))
                FavorietEvenementToggle.IsOn = (bool)ApplicationData.Current.RoamingSettings.Values["FavorietEvenementToggle"];
        }

        public string[] Title { get { return titles; } set { titles = value; } }
        public string[] Date { get { return date; } set { date = value; } }
        public string[] Description { get { return description; } set { description = value; } }
        public string[] Website {  get { return website; } set { website = value; } }
        public string[] Address { get { return address; } set { address = value; } }

        private void EvenementenSelectionChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ListItem");
        }

        private void Evenement_CLick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Overzicht Evenementen");
            CategorieTitel.Text = "Overzicht Evenementen";
        }

        private void Favoriet_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Overzicht Favorieten");
            CategorieTitel.Text = "Overzicht Favorieten";
        }

        private void BlackList_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Overzicht BlackList");
            CategorieTitel.Text = "Overzicht BlackList";
        }

        private void EvenementenSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = ListViewEvenementen.SelectedIndex;
            
            //Debug.WriteLine("Selected: {0}", e.AddedItems[i]);
            TitelEvenement2.Text = titles[id];
            DatumEvenement.Text = date[id];
            BeschrijvingEvenement.Text = description[id];
            AdresEvenement.Text = address[id];
            WebsiteEvenement2.Content = website[id];
            Uri uri = new Uri(website[id]);
            WebsiteEvenement2.NavigateUri = uri;
            adres = address[id];
            
            
        }

        private void Favoriet_Toggled(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.RoamingSettings.Values["FavorietEvenementToggle"] = FavorietEvenementToggle.IsOn;

            if (FavorietEvenementToggle.IsOn)
            {
                FavorietEvenement.Text = "Opgeslagen als favoriet";
            }
            else
            {
                FavorietEvenement.Text = "Favoriet";
            }
        }

        private async void Contact_Click(object sender, RoutedEventArgs e)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("reventoevents@gmail.com"));
            string messageBody = "Geachte Revento,";
            emailMessage.Body = messageBody;
            string subjectBody = "Evenement aanmelden";
            emailMessage.Subject = subjectBody;

            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private async void Kaart_Click(object sender, RoutedEventArgs e)
        {

            // Locaties
            var uriLocation = new Uri("bingmaps:?where=" + adres);

            // Launch the Windows Maps app
            var launcherOptions = new Windows.System.LauncherOptions();
            launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
            var success = await Windows.System.Launcher.LaunchUriAsync(uriLocation, launcherOptions);

        }
    }
}
