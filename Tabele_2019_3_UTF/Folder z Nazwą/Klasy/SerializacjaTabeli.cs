using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace Tabele_2019_3_UTF.a.Klasy
{
    public class Table
    {
        public string? name { get; set; }
        public string? ident { get; set; }
        public string? prefix { get; set; }
        public string? filename { get; set; }
        public string? options { get; set; }
        public string? description { get; set; }
        public string? longdescription { get; set; }
        public string? owner { get; set; }
        public string? glbx { get; set; }
        public string? nosql { get; set; }
        public string? aliases { get; set; }
        public GrupaKolumn[] GrupyKolumn { get; set; }

        public Kolumna[] Kolumny { get; set; }

        public Indeks[] Indeksy { get; set; }

        public Relacja[] Relacje { get; set; }


    }
    public class Kolumna
    {
        public string? ident { get; set; }
        public string? clarionname { get; set; }
        public string? sqlname { get; set; }
        public string? type { get; set; }
        public string? cleriontype { get; set; }
        public string? sqltype { get; set; }
        public int?    size { get; set; }
        public int?    places { get; set; }
        public string? description{ get; set; }
        public string? longdescription{ get; set; }
        public string? options{ get; set; }
        public string? picture{ get; set; }
        public string? initial{ get; set; }
        public string? justification_type { get; set; }
        public int?    justification_index { get; set; }
        public string? header { get; set; }
        public string? validation{ get; set; }
        public string? choices { get; set; }
        public string? rangelow { get; set; }
        public string? rangehigh { get; set; }
        public string? isinprimary { get; set; }
        public string? nosql { get; set; }
        public string? comments{ get; set; }
        public string? isinsqlprimary{ get; set; }
    }
    public class GrupaKolumn
    {
        public string? ident { get; set; }
        public string? clarionname { get; set; }
        public string? type { get; set; }
        public string? clariontype { get; set; }
        public string? description { get; set; }
        public string? longdescription { get; set; }
        public Kolumna[] Kolumny { get; set; }
    }
    public class Indeks
    {
        public string? ident { get; set; }
        public string? clarionname{ get; set; }
        public string? sqlname { get; set; }
        public string? description { get; set; }
        public string? longdescription { get; set; }
        public string? options { get; set; }
        public string? primary { get; set; }
        public string? unique { get; set; }
        public string? casesensitive { get; set; }
        public string? excudenulls { get; set; }
        public string? autonum { get; set; }
        public string? nosql { get; set; }
        public IndeksKolumn[]? IndeksyKolumn { get; set; }
    }
                    public class IndeksKolumn
                    {
                        public string? ident { get; set; }
                        public string? clarionname { get; set; }
                        public string? sqlname { get; set; }
                        public string? nosql { get; set; }
                    }

    public class Relacja
    {
        public string? type { get; set; }
        public string? onupdate { get; set; }
        public string? ondelete { get; set; }
        public string? options { get; set; }
        public string? comments { get; set; }
        public string? nosql { get; set; }
        public string? setnull { get; set; }
        public string? conditional { get; set; }
        public string? parent { get; set; }
        public string? primaryname { get; set; }
        public string? primarysqlname { get; set; }
        public string? parenthandler { get; set; }
        public string? parentlink { get; set; }
        public string? child { get; set; }
        public string? foreignname { get; set; }
        public string? foreignsqlname { get; set; }
        public string? childhandler { get; set; }
        public string? childlink { get; set; }
        public RelacjaKolumn[] RelacjeKolumn { get; set; }
    }
                   public class RelacjaKolumn
                   {
                       public string? parentident { get; set; }
                       public string? parentname { get; set; }
                       public string? parentsqlname { get; set; }
                       public string? childident { get; set; }
                       public string? childname { get; set; }
                       public string? childsqlname { get; set; }
                       public string? isinsqlprimary { get; set; }
                   }
    public class SerializacjaTabeli
    {
        public  List<Table> xmlZawarotsc { get; set; } = new List<Table>();
        public  List<string> ErrorLog { get; set; } = new List<string>();

        public SerializacjaTabeli(FileInfo[] xmlFiles)
        {
            
            foreach(FileInfo fi in xmlFiles)
            {
             LoadingData(fi);
            }
                  
        }
        public void LoadingData(FileInfo XMLFilesInfo)
        {

            XDocument xmldoc = XDocument.Load(XMLFilesInfo.FullName);
            int resoult = tabele(xmldoc);
            if (resoult > 0)  // jeśli gdzies jest więcej atrybutow niz prszewidziano to zwraca lokalizacje tego pliku
            {
                ErrorLog.Add(XMLFilesInfo.FullName);             
            }
        }

        int tabele(XDocument xmldoc)
        {
            var Nodelist = xmldoc.Element("ROOT").Elements("table");

            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            var element = xmldoc.Element("ROOT").Element("table");
            if (element == null)
            {
                LiczbaAtrybutow = 0;
            }
            else 
            {
                LiczbaAtrybutow = element.Attributes().Count();
            }
            if (LiczbaAtrybutow > 11) LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow; //Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //






            foreach (XElement node in Nodelist)
            {

                Table obj = new Table();
                obj.name = node.Attribute("name")?.Value;
                obj.prefix = node.Attribute("prefix")?.Value;
                obj.filename = node.Attribute("filename")?.Value;
                obj.filename = node.Attribute("filename")?.Value;
                obj.owner = node.Attribute("owner")?.Value;
                obj.glbx = node.Attribute("glbx")?.Value;                                                                   //////// TABELE ///////
                obj.aliases = node.Attribute("aliases")?.Value;
                obj.ident = node.Attribute("ident")?.Value;
                obj.description = node.Attribute("description")?.Value;
                obj.longdescription = node.Attribute("longdescription")?.Value;
                obj.options = node.Attribute("options")?.Value;
                obj.nosql = node.Attribute("nosql")?.Value;

                var resoultGrupyKolumn = GrupyKolumn(xmldoc);
                obj.GrupyKolumn = resoultGrupyKolumn.GrupyKolumn.ToArray();

                var resoultKolumny = kolumny(null,xmldoc, false);
                obj.Kolumny = resoultKolumny.kolumny.ToArray();
                
                var resoultIndeksy = indeksy(xmldoc);
                obj.Indeksy = resoultIndeksy.indeksy.ToArray();
                
                var resoultRelacje = relacja(xmldoc);
                obj.Relacje = resoultRelacje.relacja.ToArray();
                
                LiczbaAtrybutowZwrotDanych += resoultKolumny.LiczbaAtrybutowZwrotDanych + resoultIndeksy.LiczbaAtrybutowZwrotDanych + resoultRelacje.LiczbaAtrybutowZwrotDanych + resoultGrupyKolumn.LiczbaAtrybutowZwrotDanych;

                xmlZawarotsc.Add(obj);
            }
            return LiczbaAtrybutowZwrotDanych;
        }
        (List<GrupaKolumn> GrupyKolumn, int LiczbaAtrybutowZwrotDanych) GrupyKolumn(XDocument xmldoc)
        {
            var Nodelist = xmldoc.Element("ROOT").Element("table").Element("columns").Elements("group");

            List<GrupaKolumn> XLGrupaKolumn = new List<GrupaKolumn>();

            int LiczbaAtrybutowChildNodes = 0;
            int LiczbaAtrybutowGrupykolumn = 0;
            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            foreach (var element in xmldoc.Element("ROOT").Element("table").Element("columns").Elements("group"))
            {
                LiczbaAtrybutow = element.DescendantsAndSelf().Attributes().Count();
                LiczbaAtrybutowChildNodes = element.Elements("column").DescendantsAndSelf().Attributes().Count();
                LiczbaAtrybutow -= LiczbaAtrybutowChildNodes;
                if (LiczbaAtrybutow > 6)//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
                {
                    LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;
                }
            }



            foreach (XElement node in Nodelist)
            {
                GrupaKolumn obj = new GrupaKolumn();
                obj.ident = node.Attribute("ident")?.Value;
                obj.clarionname = node.Attribute("clarionname")?.Value;
                obj.clariontype = node.Attribute("clariontype")?.Value;
                obj.type = node.Attribute("type")?.Value;
                obj.description = node.Attribute("description")?.Value;                                                     ////// Grupy Kolumn //////
                obj.longdescription = node.Attribute("longdescription")?.Value;

                var resoultKolumny = kolumny(node,null, true);
                obj.Kolumny = resoultKolumny.kolumny.ToArray();
                LiczbaAtrybutowGrupykolumn += resoultKolumny.LiczbaAtrybutowZwrotDanych;
                XLGrupaKolumn.Add(obj);
            }
            LiczbaAtrybutow += LiczbaAtrybutowGrupykolumn;
            return (XLGrupaKolumn, LiczbaAtrybutowZwrotDanych);
        }

        (List<Kolumna> kolumny, int LiczbaAtrybutowZwrotDanych) kolumny (XElement xmldocgrupy, XDocument xmldoc, bool IsGrupaOrNot) // 1 - Is in group, 0 - Is not in group
        {
            IEnumerable<XElement> Nodelist;
            if (IsGrupaOrNot == true)
            {
                Nodelist = xmldocgrupy.Elements("column");
            }
            else
            {
                Nodelist = xmldoc.Element("ROOT").Element("table").Element("columns").Elements("column");
            }
                List<Kolumna> XLKolumna = new List<Kolumna>();

            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            foreach (var element in Nodelist.DescendantsAndSelf())
            {
                LiczbaAtrybutow = element.Attributes().Count();                
            if (LiczbaAtrybutow > 24)LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
            }
            
                

            foreach (XElement node in Nodelist)
            {
                Kolumna obj = new Kolumna();
                obj.ident = node.Attribute("ident")?.Value;
                obj.clarionname = node.Attribute("clarionname")?.Value;
                obj.sqlname = node.Attribute("sqlname")?.Value;
                obj.type = node.Attribute("type")?.Value;
                obj.cleriontype = node.Attribute("clariontype")?.Value;
                obj.sqltype = node.Attribute("sqltype")?.Value;
                obj.size = Convert.ToInt32(node.Attribute("size")?.Value);
                obj.places = Convert.ToInt32(node.Attribute("places")?.Value);
                obj.description = node.Attribute("description")?.Value;
                obj.longdescription = node.Attribute("longdescription")?.Value;                                                 ////// Kolumny //////
                obj.options = node.Attribute("options")?.Value;
                obj.picture = node.Attribute("picture")?.Value;
                obj.initial = node.Attribute("initial")?.Value;
                obj.justification_type = node.Attribute("justification.type")?.Value;
                obj.justification_index = Convert.ToInt32(node.Attribute("justification.index")?.Value);
                obj.header = node.Attribute("header")?.Value;
                obj.validation = node.Attribute("validation")?.Value;
                obj.choices = node.Attribute("choices")?.Value;
                obj.rangelow = node.Attribute("rangelow")?.Value;
                obj.rangehigh = node.Attribute("rangehigh")?.Value;
                obj.isinprimary = node.Attribute("isinprimary")?.Value;
                obj.nosql = node.Attribute("nosql")?.Value;
                obj.comments = node.Attribute("comments")?.Value;
                obj.isinsqlprimary = node.Attribute("isinsqlprimary")?.Value;

                XLKolumna.Add(obj);
            }
            return (XLKolumna , LiczbaAtrybutowZwrotDanych);
        }
        (List<Indeks> indeksy, int LiczbaAtrybutowZwrotDanych) indeksy(XDocument xmldoc)
        {
            var Nodelist = xmldoc.Element("ROOT").Element("table").Element("indexes").Elements("index");

            List<Indeks> XLIndeks = new List<Indeks>();
           
            int LiczbaAtrybutowChildNodes = 0;
            int LiczbaAtrybutowIndeksykolumn = 0;
            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            foreach (var element in xmldoc.Element("ROOT").Element("table").Element("indexes").Elements("index"))
            {
                LiczbaAtrybutow = element.DescendantsAndSelf().Attributes().Count();
                LiczbaAtrybutowChildNodes = element.Elements("indexcolumns").DescendantsAndSelf().Attributes().Count();
                LiczbaAtrybutow -= LiczbaAtrybutowChildNodes;
                if (LiczbaAtrybutow > 12)LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
            }
            

            foreach (XElement node in Nodelist)
            {

                Indeks obj = new Indeks();
                obj.ident = node.Attribute("ident")?.Value;
                obj.clarionname = node.Attribute("clarionname")?.Value;
                obj.sqlname = node.Attribute("sqlname")?.Value;
                obj.description = node.Attribute("description")?.Value;
                obj.longdescription = node.Attribute("longdescription")?.Value;
                obj.options = node.Attribute("options")?.Value;                                                                  ////// Indeksy //////
                obj.primary = node.Attribute("primary")?.Value;
                obj.unique = node.Attribute("unique")?.Value;
                obj.casesensitive = node.Attribute("casesensitive")?.Value;
                obj.excudenulls = node.Attribute("excudenulls")?.Value;
                obj.autonum = node.Attribute("autonum")?.Value;
                obj.nosql = node.Attribute("nosql")?.Value;

                var resoultIndeksyKolumn = indeksKolumn(node);
                obj.IndeksyKolumn = resoultIndeksyKolumn.indeksKolumn.ToArray();

                LiczbaAtrybutowIndeksykolumn = resoultIndeksyKolumn.LiczbaAtrybutowIndexKolumn;
                XLIndeks.Add(obj);
            }
                LiczbaAtrybutow += LiczbaAtrybutowIndeksykolumn;
                return (XLIndeks, LiczbaAtrybutowZwrotDanych);
        }
        (List<IndeksKolumn> indeksKolumn, int LiczbaAtrybutowIndexKolumn) indeksKolumn (XElement node)
        {
            var Nodelist2 = node.Element("indexcolumns").DescendantNodes();

            List<IndeksKolumn> XLIndeksKolumn = new List<IndeksKolumn>();

            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            foreach (var element in node.Element("indexcolumns").DescendantsAndSelf())
            {
                LiczbaAtrybutow = element.Attributes().Count();
            if (LiczbaAtrybutow > 4)LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
            }



            foreach (XElement nodeIndKol in Nodelist2)
            {
                IndeksKolumn obj = new IndeksKolumn();
                obj.ident = nodeIndKol.Attribute("ident")?.Value;                                                            ////// INDEKSY KOLUMN //////
                obj.clarionname = nodeIndKol.Attribute("clarionname")?.Value;
                obj.sqlname = nodeIndKol.Attribute("sqlname")?.Value;
                obj.nosql = nodeIndKol.Attribute("nosql")?.Value;

                XLIndeksKolumn.Add(obj);
            }

            return (XLIndeksKolumn, LiczbaAtrybutowZwrotDanych);
        }
        (List<Relacja> relacja, int LiczbaAtrybutowZwrotDanych) relacja(XDocument xmldoc)
        {
            var Nodelist = xmldoc.Element("ROOT").Element("table").Element("relations").Elements("relation");

            List<Relacja> XLRelacja = new List<Relacja>();


            int LiczbaAtrybutowChildNodes = 0;
            int LiczbaAtrybutowRelacjeKolumn = 0;
            int LiczbaAtrybutow = 0;
            int LiczbaAtrybutowZwrotDanych = 0;
            foreach (var element in xmldoc.Element("ROOT").Element("table").Element("relations").Elements())
            {
                LiczbaAtrybutow = element.DescendantsAndSelf().Attributes().Count();
                LiczbaAtrybutowChildNodes = element.Elements().DescendantsAndSelf().Attributes().Count();
                LiczbaAtrybutow -= LiczbaAtrybutowChildNodes;
                if (LiczbaAtrybutow > 18)LiczbaAtrybutowZwrotDanych += LiczbaAtrybutow;//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
            }


            foreach (XElement node in Nodelist)
            {
                Relacja obj = new Relacja();
                obj.type = node.Attribute("type")?.Value;
                obj.onupdate = node.Attribute("onupdate")?.Value;
                obj.ondelete = node.Attribute("ondelete")?.Value;
                obj.options = node.Attribute("options")?.Value;
                obj.comments = node.Attribute("comments")?.Value;
                obj.nosql = node.Attribute("nosql")?.Value;
                obj.setnull = node.Attribute("setnull")?.Value;
                obj.conditional = node.Attribute("conditional")?.Value;
                obj.parent = node.Attribute("parent")?.Value;
                obj.primaryname = node.Attribute("primaryname")?.Value;                                                        ////// Relacje //////
                obj.primarysqlname = node.Attribute("primarysqlname")?.Value;
                obj.parenthandler = node.Attribute("parenthandler")?.Value;
                obj.parentlink = node.Attribute("parentlink")?.Value;
                obj.child = node.Attribute("child")?.Value;
                obj.foreignname = node.Attribute("foreignname")?.Value;
                obj.foreignsqlname = node.Attribute("foreignsqlname")?.Value;
                obj.childhandler = node.Attribute("childhandler")?.Value;
                obj.childlink = node.Attribute("childlink")?.Value;

                var resoult = relacjaKolumn(node);
                obj.RelacjeKolumn = resoult.relacjaKolumn.ToArray();
                LiczbaAtrybutowRelacjeKolumn = resoult.LiczbaAtrybutowRelacjeKolumn;
                XLRelacja.Add(obj);
            }
                LiczbaAtrybutow += LiczbaAtrybutowRelacjeKolumn;
            
            return (XLRelacja, LiczbaAtrybutowZwrotDanych);
        }
            (List<RelacjaKolumn> relacjaKolumn, int LiczbaAtrybutowRelacjeKolumn ) relacjaKolumn (XElement node)
            {
                var Nodelist3 = node.Elements();

                List<RelacjaKolumn> XLRelacjaKolumn = new List<RelacjaKolumn>();

                int LiczbaAtrybutowRelacjeKolumn = 0;
                int LiczbaAtrybutowZwrotDanych = 0;
                foreach (var element in Nodelist3.DescendantsAndSelf())
                {
                LiczbaAtrybutowRelacjeKolumn = element.Attributes().Count();
                if (LiczbaAtrybutowRelacjeKolumn > 7)LiczbaAtrybutowZwrotDanych += LiczbaAtrybutowRelacjeKolumn;//Sprawdzenie czy liczba atrybutow zgadza sie z ustaloną //
            }

            
            foreach (XElement nodeRelaKol in Nodelist3)
                {
                    RelacjaKolumn obj = new RelacjaKolumn();
                    obj.parentname = nodeRelaKol.Attribute("parentname")?.Value;                                            ////// RELACJE KOLUMN //////
                    obj.parentident = nodeRelaKol.Attribute("parentident")?.Value;
                    obj.parentsqlname = nodeRelaKol.Attribute("parentsqlname")?.Value;
                    obj.childident = nodeRelaKol.Attribute("childident")?.Value;
                    obj.childname = nodeRelaKol.Attribute("childname")?.Value;
                    obj.childsqlname = nodeRelaKol.Attribute("childsqlname")?.Value;
                    obj.isinsqlprimary = nodeRelaKol.Attribute("isinsqlprimary")?.Value;

                    XLRelacjaKolumn.Add(obj);
                }

            return (XLRelacjaKolumn, LiczbaAtrybutowZwrotDanych);
            }
    }
}
