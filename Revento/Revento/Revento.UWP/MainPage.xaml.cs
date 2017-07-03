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

        public string[] ParseXMLCategory(string attribute, string category)
        {
            string[] _event = XMLProcesser.SendXMLCategory(xdoc, attribute, category);

            return _event;
        }
    }

    public sealed partial class MainPage : Page
    {
        public string[] titles, date, description, website, address;
        public int index;
        public string adres;

        public XDocument loadedfile;

        public MainPage()
        {
            string XMLfile = Path.Combine(Package.Current.InstalledLocation.Path, "events.xml");

            Event evenement = new Event();
            loadedfile = evenement.LoadXML(XMLfile);

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

        private void Automotive_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Auto");
            // CategorieTitel.Text = "Categorie: Automotive";

            // XMLProcesser.SendXMLCategory(xmldocument)
            XMLProcesser.SendXMLCategory(loadedfile, "title", "Automotive");
        }

        private void EvenementenSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = ListViewEvenementen.SelectedIndex;
            
            TitelEvenement2.Text = titles[id];
            DatumEvenement.Text = date[id];
            BeschrijvingEvenement.Text = description[id];
            AdresEvenement.Text = address[id];
            WebsiteEvenement2.Content = website[id];
            Uri uri = new Uri(website[id]);
            WebsiteEvenement2.NavigateUri = uri;
            adres = address[id];
        }

        private async void Contact_Click(object sender, RoutedEventArgs e)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("reventoevents@gmail.com"));
            string messageBody = "Geachte Revento, \n \n" + "Titel van evenement: \n" + "Datum van evenement: \n" + "Beschrijving van evenement: \n" + "Adres van evenement: \n" + "Website van evenement: \n";
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
