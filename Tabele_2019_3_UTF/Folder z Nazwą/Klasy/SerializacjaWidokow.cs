using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace Tabele_2019_3_UTF.SerializacjaViews
{
    public class Viewdef
    {
        public string? name { get; set; }
        public string? viewdefZawarotsc { get; set; }
    }
    public class Scriptdef
    {
        public string? name { get; set; }
        public bool? hidden { get; set; }
        public string? scriptdefZawarotsc { get; set; }
    }


    public class SerializacjaWidokow
    {
        public List<Viewdef> xmlZawarotscViewdef { get; set; } = new List<Viewdef>();
        public List<Scriptdef> xmlZawarotscScriptdef { get; set; } = new List<Scriptdef>();
        public List<string> ErrorLog { get; set; } = new List<string>();
        public SerializacjaWidokow(FileInfo[] XF)
        {
            foreach (FileInfo fi in XF)
            {
                LoadnigDataViews(fi);
            }
            
        }
        void LoadnigDataViews(FileInfo XMLFilesInfo)
        {
            XDocument xmldoc = XDocument.Load(XMLFilesInfo.FullName);
            int ala = viewdef(xmldoc);
            if (ala > 0) ErrorLog.Add(XMLFilesInfo.FullName);
            if (XMLFilesInfo.Name == "VIEW_PreliminarzView.xml" || XMLFilesInfo.Name == "VIEW_TreMMView.xml" || XMLFilesInfo.Name == "VIEW_TrpTrnKazView.xml" || XMLFilesInfo.Name == "VIEW_TrpTrnView.xml")
            {                                   // Dodałem na sztywno nazwy plików poniewaz te pliki zawierają sie w zakładce widoki
                int ola = scriptdef(xmldoc);
                if (ola > 0) ErrorLog.Add(XMLFilesInfo.FullName);
            }
        }

        int viewdef(XDocument xmldoc)
        {
            var Nodelist = xmldoc.Element("ROOT").Elements("viewdef");

            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            foreach (var element in xmldoc.Element("ROOT").Elements("viewdef"))
            {
                if (element == null)
                {
                    LiczbaAtrybutow = 0;
                }
                else
                {
                    LiczbaAtrybutow = element.DescendantsAndSelf().Attributes().Count();
                }
                if (LiczbaAtrybutow > 1)//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
                {
                    LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;
                }
            }

            foreach (XElement node in Nodelist)
            {

                Viewdef obj = new Viewdef();
                obj.name = node.Attribute("name")?.Value;
                obj.viewdefZawarotsc = node?.Value;
                xmlZawarotscViewdef.Add(obj);
            }

            return LiczbaAtrybutowZwrotDanych;
        }
        int scriptdef(XDocument xmldoc)
        {
            var Nodelist = xmldoc.Element("ROOT").Elements("scriptdef");

            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            foreach (var element in xmldoc.Element("ROOT").Elements("scriptdef"))
            {
                if (element == null)
                {
                    LiczbaAtrybutow = 0;
                }
                else
                {
                    LiczbaAtrybutow = element.DescendantsAndSelf().Attributes().Count();
                }
                if (LiczbaAtrybutow > 2)//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
                {
                    LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;
                }
            }

            foreach (XElement node in Nodelist)
            {

                Scriptdef obj = new Scriptdef();
                obj.name = node.Attribute("name")?.Value;
                obj.hidden = Convert.ToBoolean(node.Attribute("hidden")?.Value);
                obj.scriptdefZawarotsc = node?.Value;
                xmlZawarotscScriptdef.Add(obj);
            }

            return LiczbaAtrybutowZwrotDanych;
        }
    }
}
