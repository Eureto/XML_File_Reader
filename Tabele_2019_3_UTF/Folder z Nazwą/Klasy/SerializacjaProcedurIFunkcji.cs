using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace Tabele_2019_3_UTF.SerializacjaProcedurIFunkcji
{
    public class ProceduryIFunkcji
    {
        public string? name { get; set; }
        public bool? hidden { get; set; }
        public string? scriptdefZawarotsc { get; set; }
    }


    public class SerializacjaProcedurFunkcji
    {
        public  List<ProceduryIFunkcji> xmlZawarotscProcedurFunkcji { get; set; } = new List<ProceduryIFunkcji>();
        public List<string> ErrorLog { get; set; } = new List<string>();
        public SerializacjaProcedurFunkcji(FileInfo[] XF)
        {
            foreach (FileInfo fi in XF)
            {
                LoadnigDataProcedurFunkcji(fi);
            }
            ;
        }
        void LoadnigDataProcedurFunkcji(FileInfo XMLFilesInfo)
        {
            XDocument xmldoc = XDocument.Load(XMLFilesInfo.FullName);
            if (XMLFilesInfo.Name != "VIEW_PreliminarzView.xml" || XMLFilesInfo.Name != "VIEW_TreMMView.xml" || XMLFilesInfo.Name != "VIEW_TrpTrnKazView.xml" || XMLFilesInfo.Name != "VIEW_TrpTrnView.xml")
            {                                   // Dodałem na sztywno nazwy plików poniewaz te pliki zawierają sie w zakładce widoki
                int ola = scriptdef(xmldoc);
                if (ola > 0) ErrorLog.Add(XMLFilesInfo.FullName);
            }
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
                if (LiczbaAtrybutow > 2)    //Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
                {
                    LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;
                }
            }

            foreach (XElement node in Nodelist)
            {

                ProceduryIFunkcji obj = new ProceduryIFunkcji();
                obj.name = node.Attribute("name")?.Value;
                obj.hidden = Convert.ToBoolean(node.Attribute("hidden")?.Value);
                obj.scriptdefZawarotsc = node?.Value;
                xmlZawarotscProcedurFunkcji.Add(obj);
            }

            return LiczbaAtrybutowZwrotDanych;
        }
    }
}
