using System.Xml;  
using Android.Content; 
using Android.Widget;
using System.IO;
 
namespace Revento.Droid
{
    public class XmlWriter
    {
        /// <summary>
        /// Method is used to add events to an XML file, this works in a console app, the only problem is that
        /// the stream that is used is not writeable. The Stream needs to have acces to Assets folder, but it is readonly.
        /// When this method is called it will Try to save the new content to the xml file, else it will show a Toast message.
        /// </summary>
        /// <param name="context">The activity from where the Asset folder is opened, usely its "this"</param>
        /// <param name="file">The file from in the Assets folder in wich it needs to write.</param>
        /// <param name="title">The events title</param>
        /// <param name="date">The event date</param>
        /// <param name="description">The event description</param>
        /// <param name="adress">The event adress</param>
        /// <param name="website">The event website, in the app this becomes a clickable hyperlink</param>
        public static void AddEvent(Context context,string file, string title, string date, string description, string adress, string website)
        {
            XmlDocument xd = new XmlDocument();
            
            Stream XmlDoc = context.Assets.Open(file);
            
            xd.Load(XmlDoc);
            
            XmlNode nl = xd.SelectSingleNode("resources");
            XmlDocument xd2 = new XmlDocument();
            xd2.LoadXml("<events><title>" + title + "</title><date>" + date + "</date><description>" + description + "</description><adress>" + adress + "</adress><website>" + website + "</website></events>");
            XmlNode n = xd.ImportNode(xd2.FirstChild, true);
            nl.AppendChild(n);
            try
            {
                //Het bestand openen lukt alleen bij opslaan komt er melding dat dat de toegang tot het bestand is gewigerd
                //xd.Save(XmlDoc);
                File.Copy(file,file,false);
                Toast.MakeText(context, "Added event succesfully", ToastLength.Short).Show();
            }
            catch
            {
                Toast.MakeText(context,"Sorry can't add new item", ToastLength.Short).Show();
            }
        }
    }
}