using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Xml; 
using System.Xml.Linq; 
 
using Android.App; 
using Android.Content; 
using Android.OS; 
using Android.Runtime; 
using Android.Views; 
using Android.Widget;
using System.IO;
 
namespace Revento.Droid
{
    public class XmlWriter
    {
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