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

        // Laad het XDocument op de locatie van het bestand
        public XDocument LoadXML(string filepath)
        {
            this.xdoc = XDocument.Load(filepath);

            return xdoc;
        }

        // Haal de elementen uit het XDocument en stop ze in een array
        public string[] ParseXML(string attribute)
        {
            string[] _event = XMLProcesser.SendXML(xdoc, attribute);

            return _event;
        }

        // Haal de elementen (inclusief categorie) uit het XDocument en stop ze in een array
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
        private Button button;

        public XDocument loadedfile;

        public MainPage()
        {
            // Laad het XML bestand in
            string XMLfile = Path.Combine(Package.Current.InstalledLocation.Path, "events.xml");

            // Parse het xml bestand en sorteer de informatie op titel, datum, beschrijving, website en adres
            Event evenement = new Event();
            loadedfile = evenement.LoadXML(XMLfile);

            this.titles = evenement.ParseXML("title");
            this.date = evenement.ParseXML("date");
            this.description = evenement.ParseXML("description");
            this.website = evenement.ParseXML("website");
            this.address = evenement.ParseXML("address");

            this.InitializeComponent();

            // Verander de ItemsSource naar de titles array en toon ze op het scherm
            ListViewEvenementen.ItemsSource = titles;            
        }

        // Handle de klik wanneer er op een evenement wordt geklikt
        private void Evenement_CLick(object sender, RoutedEventArgs e)
        {
            CategorieTitel.Text = "Overzicht Evenementen";
        }   

        // Toon alle gegevens van een evenement wanneer er op een evenement wordt geklikt
        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = ListViewCategory.SelectedIndex;

            switch (id)
            {
                case 0:
                    CategorieTitel.Text = "Automotive";
                    break;
                case 1:
                    CategorieTitel.Text = "Film";
                    break;
                case 2:
                    CategorieTitel.Text = "Kunst";
                    break;
                case 3:
                    CategorieTitel.Text = "Literatuur";
                    break;
                case 4:
                    CategorieTitel.Text = "Musea";
                    break;
                case 5:
                    CategorieTitel.Text = "Muziek";
                    break;
                case 6:
                    CategorieTitel.Text = "Overig";
                    break;
                case 7:
                    CategorieTitel.Text = "Sport";
                    break;
                case 8:
                    CategorieTitel.Text = "Stad";
                    break;
            }
        }

        // Toon alle gegevens van een evenement wanneer er op een evenement wordt geklikt
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

        // Handle de mail functionaliteit
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

        // Handle de kaart functionaliteit
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
