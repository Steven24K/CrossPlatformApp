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
        public string category;
        public int eventID, categoryID;

        public XDocument loadedfile;

        public MainPage()
        {
            LoadCategory("Alles");
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

                InitializeComponent();

                ListViewEvenementen.ItemsSource = titles;
            }
            else
            {
                titles = evenement.ParseXMLCategory("title", categoryclick);
                date = evenement.ParseXMLCategory("date", categoryclick);
                description = evenement.ParseXMLCategory("description", categoryclick);
                website = evenement.ParseXMLCategory("website", categoryclick);
                address = evenement.ParseXMLCategory("address", categoryclick);

                InitializeComponent();

                ListViewEvenementen.ItemsSource = titles;
            }
        }

        // Toon alle gegevens van een evenement wanneer er op een evenement wordt geklikt
        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int categoryID = ListViewCategory.SelectedIndex;
            Debug.WriteLine(categoryID);

            switch (categoryID)
            {
                case 0:
                    CategorieTitel.Text = "Overzicht Evenementen";
                    category = "Alles";
                    LoadCategory(category);
                    eventID += 1;
                    break;
                case 1:
                    CategorieTitel.Text = "Automotive";
                    category = "Automotive";
                    LoadCategory(category);
                    eventID += 1;
                    break;
                case 2:
                    CategorieTitel.Text = "Film";
                    category = "Film";
                    LoadCategory(category);
                    break;
                case 3:
                    CategorieTitel.Text = "Kunst";
                    category = "Kunst";
                    LoadCategory(category);
                    break;
                case 4:
                    CategorieTitel.Text = "Literatuur";
                    category = "Literatuur";
                    LoadCategory(category);
                    break;
                case 5:
                    CategorieTitel.Text = "Musea";
                    category = "Musea";
                    LoadCategory(category);
                    break;
                case 6:
                    CategorieTitel.Text = "Muziek";
                    category = "Muziek";
                    LoadCategory(category);
                    break;
                case 7:
                    CategorieTitel.Text = "Overig";
                    category = "Overig";
                    LoadCategory(category);
                    break;
                case 8:
                    CategorieTitel.Text = "Sport";
                    category = "Sport";
                    LoadCategory(category);
                    break;
                case 9:
                    CategorieTitel.Text = "Stad";
                    category = "Stad";
                    LoadCategory(category);
                    break;
            }            
        }

        // Toon alle gegevens van een evenement wanneer er op een evenement wordt geklikt
        private void EvenementenSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            eventID = ListViewEvenementen.SelectedIndex;

            TitelEvenement2.Text = titles[eventID];
            DatumEvenement.Text = date[eventID];
            BeschrijvingEvenement.Text = description[eventID];
            AdresEvenement.Text = address[eventID];
            WebsiteEvenement2.Content = website[eventID];
            Uri uri = new Uri(website[eventID]);
            WebsiteEvenement2.NavigateUri = uri;
            adres = address[eventID];

            eventID = 0;
            categoryID = 0;
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
