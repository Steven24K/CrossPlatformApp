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
        public static string category;

        public XDocument loadedfile;

        public MainPage()
        {                    
            LoadCategory("Alles");

            this.InitializeComponent();

            // Verander de ItemsSource naar de titles array en toon ze op het scherm
            ListViewEvenementen.ItemsSource = titles;
        }

        public void LoadCategory(string categoryclick)
        {
            // Laad het XML bestand in
            string XMLfile = Path.Combine(Package.Current.InstalledLocation.Path, "events.xml");

            // Parse het xml bestand en sorteer de informatie op titel, datum, beschrijving, website en adres
            Event evenement = new Event();
            loadedfile = evenement.LoadXML(XMLfile);

            if (categoryclick == "Alles")
            {
                titles = evenement.ParseXML("title");
                date = evenement.ParseXML("date");
                description = evenement.ParseXML("description");
                website = evenement.ParseXML("website");
                address = evenement.ParseXML("address");
            }
            else
            {
                titles = evenement.ParseXMLCategory("title", categoryclick);
                date = evenement.ParseXMLCategory("date", categoryclick);
                description = evenement.ParseXMLCategory("description", categoryclick);
                website = evenement.ParseXMLCategory("website", categoryclick);
                address = evenement.ParseXMLCategory("address", categoryclick);
            }
        }

        // Toon alle gegevens van een evenement wanneer er op een evenement wordt geklikt
        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = ListViewCategory.SelectedIndex;
            Debug.WriteLine(id);

            switch (id)
            {
                case 0:
                    CategorieTitel.Text = "Overzicht Evenementen";
                    category = "Alles";
                    Debug.WriteLine(category);
                    break;
                case 1:
                    CategorieTitel.Text = "Automotive";
                    category = "Automotive";
                    Debug.WriteLine(category);
                    break;
                case 2:
                    CategorieTitel.Text = "Film";
                    category = "Film";
                    break;
                case 3:
                    CategorieTitel.Text = "Kunst";
                    category = "Kunst";
                    break;
                case 4:
                    CategorieTitel.Text = "Literatuur";
                    category = "Literatuur";
                    break;
                case 5:
                    CategorieTitel.Text = "Musea";
                    category = "Musea";
                    break;
                case 6:
                    CategorieTitel.Text = "Muziek";
                    category = "Muziek";
                    break;
                case 7:
                    CategorieTitel.Text = "Overig";
                    category = "Overig";
                    break;
                case 8:
                    CategorieTitel.Text = "Sport";
                    category = "Sport";
                    break;
                case 9:
                    CategorieTitel.Text = "Stad";
                    category = "Stad";
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
