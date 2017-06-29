using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Revento.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public class Event
    {
        public string[] EventTitle;

        public XDocument LoadXML(string filepath)
        {
            XDocument xdoc = XDocument.Load(filepath);

            return xdoc;
        }

        public string[] ParseXML(XDocument xdoc)
        {
            EventTitle = XMLProcesser.SendXML(xdoc, "title");

            Debug.Write("hoi");

            return EventTitle;
        }        
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            string XMLfile = Path.Combine(Package.Current.InstalledLocation.Path, "events.xml");

            Event evenement = new Event();
            XDocument loadedfile = evenement.LoadXML(XMLfile);

            string[] titles = evenement.ParseXML(loadedfile);

            this.InitializeComponent();

            ListViewEvenementen.ItemsSource = titles;

            //Initialize the ToggleSwitch for roaming settings
            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("FavorietEvenementToggle"))
                FavorietEvenementToggle.IsOn = (bool)ApplicationData.Current.RoamingSettings.Values["FavorietEvenementToggle"];
        }

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
            Debug.WriteLine("Selected: {0}", e.AddedItems[0]);
            TitelEvenement2.Text = e.AddedItems[0].ToString();
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
            emailMessage.To.Add(new EmailRecipient("follie96@live.nl"));
            string messageBody = "Geachte Revento,";
            emailMessage.Body = messageBody;
            string subjectBody = "Evenement aanmelden";
            emailMessage.Subject = subjectBody;

            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private async void Kaart_Click(object sender, RoutedEventArgs e)
        {
            // Locaties
            var uriLocation = new Uri("bingmaps:?where=1, Lupine, Hoogvliet");

            // Launch the Windows Maps app
            var launcherOptions = new Windows.System.LauncherOptions();
            launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
            var success = await Windows.System.Launcher.LaunchUriAsync(uriLocation, launcherOptions);

        }
    }
}
