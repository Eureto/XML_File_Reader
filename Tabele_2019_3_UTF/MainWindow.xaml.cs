using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.IO;
using System.Xml.Serialization;
using Tabele_2019_3_UTF.a.Klasy;
using System;
using Tabele_2019_3_UTF.SerializacjaViews;
using Tabele_2019_3_UTF.SerializacjaProcedurIFunkcji;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XMLFileReader;

namespace Tabele_2019_3_UTF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static FileInfo[]? XmlFiles { get; set; }
        public static string XmlTarget;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void FilePathButton_Click(object sender, RoutedEventArgs e) // Przycisk do zaznaczenia folderu z plikami xml
        {
            var dlg = new CommonOpenFileDialog
            {
                ShowHiddenItems = true,
                AllowNonFileSystemItems = true,
                IsFolderPicker = true,
                AllowPropertyEditing = true,
                AddToMostRecentlyUsedList = false,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                filePathBox.Text = dlg.FileName;
                CheckIfFileExist(dlg.FileName);
            }



        }

        public void CheckIfFileExist(string SelectedPath)           //Funkcja sprawdza czy w zaznaczony folderze są jakieś pliki xml
        {
            DirectoryInfo di = new DirectoryInfo(SelectedPath);
            var XMLFiles = di.GetFiles("*.xml");
            XmlFiles = XMLFiles;
            if (XMLFiles.Length == 0)
            {
                MessageBox.Show("Brak plików XML w tym folderze");
                Napis1.Visibility = Visibility.Hidden;
                Przycisk1.Visibility = Visibility.Hidden;
                SerializationPathBox.Visibility = Visibility.Hidden;
            }
            else
            {
                Napis1.Visibility = Visibility.Visible;
                Przycisk1.Visibility = Visibility.Visible;
                SerializationPathBox.Visibility = Visibility.Visible;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)   //Przycisk do zaznaczenia pustego folder 
        {                                                             //gdzie będą przechowywane serializowane pliki              
            var dlg = new CommonOpenFileDialog
            {
                ShowHiddenItems = true,
                AllowNonFileSystemItems = true,
                IsFolderPicker = true,
                AllowPropertyEditing = true,
                AddToMostRecentlyUsedList = false,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SerializationPathBox.Text = dlg.FileName;
                CheckIfFolderEmpty(dlg.FileName);
            }

        }

        public void CheckIfFolderEmpty(string FileName)         // sprawdzenie czy folder jest pusty jak tak to pokauje błąd
        {
            DirectoryInfo di = new DirectoryInfo(FileName);
            var XMLFiles = di.GetFiles();
            if (XMLFiles.Length == 0)
            {
                NextWindow.Visibility = Visibility.Visible;
                XmlTarget = FileName;
            }
            else
            {
                MessageBox.Show("Zaznacz Pusty Folder");
                NextWindow.Visibility = Visibility.Hidden;
            }
        }
        private void Button_NextWindow(object sender, RoutedEventArgs e)
        {
            SerializacjaTabeli serializacjaTabeli = new SerializacjaTabeli(XmlFiles);
            SerializacjaWidokow serializacjaWidokow = new SerializacjaWidokow(XmlFiles);
            SerializacjaProcedurFunkcji serializacjaProcedurFunkcji = new SerializacjaProcedurFunkcji(XmlFiles);


            foreach (var list in serializacjaTabeli.ErrorLog)
            {
                MessageBox.Show(list, "Błąd w pliku");
            }
            foreach (var list in serializacjaWidokow.ErrorLog)
            {
                MessageBox.Show(list, "Błąd w pliku");          // Komunikat o ewentualnym błędzie w pliku //
            }
            foreach (var list in serializacjaProcedurFunkcji.ErrorLog)
            {
                MessageBox.Show(list, "Błąd w pliku");
            }
            SaveToJSON(serializacjaTabeli.xmlZawarotsc, serializacjaWidokow.xmlZawarotscViewdef, serializacjaWidokow.xmlZawarotscScriptdef, serializacjaProcedurFunkcji.xmlZawarotscProcedurFunkcji, XmlTarget);
            MessageBox.Show("Done");

        }
        void SaveToJSON(List<Tabele_2019_3_UTF.a.Klasy.Table> Tabele, List<SerializacjaViews.Viewdef> Widoki, List<SerializacjaViews.Scriptdef> Scriptdef, List<SerializacjaProcedurIFunkcji.ProceduryIFunkcji> ProceduryIFunkcje, string FolderName)
        {
            int i = 0;
            string filePath = FolderName + @"\Tabele";
            // If directory does not exist, create it
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (var Tab in Tabele)
            {
                filePath = FolderName + @$"\Tabele\Tabela{i}.json";
                JsonSerializer jsonSerializer = new JsonSerializer();
                if (File.Exists(filePath)) File.Delete(filePath);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(Tab, Formatting.Indented));

                i++;
            }
            i = 0;
            filePath = FolderName + @"\Widoki";
            // If directory does not exist, create it
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (var Wid in Widoki)
            {
                filePath = FolderName + @$"\Widoki\Widok{i}.json";
                JsonSerializer jsonSerializer = new JsonSerializer();
                if (File.Exists(filePath)) File.Delete(filePath);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(Wid, Formatting.Indented));

                i++;
            }
            i = 0;
            filePath = FolderName + @"\Scriptdefy";
            // If directory does not exist, create it
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (var Scrd in Scriptdef)
            {
                filePath = FolderName + @$"\Scriptdefy\Scripdef{i}.json";
                JsonSerializer jsonSerializer = new JsonSerializer();
                if (File.Exists(filePath)) File.Delete(filePath);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(Scrd, Formatting.Indented));

                i++;
            }
            i = 0;
            filePath = FolderName + @"\ProceduryIFunkcje";
            // If directory does not exist, create it
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (var ProFunk in ProceduryIFunkcje)
            {
                filePath = FolderName + @$"\ProceduryIFunkcje\ProceduraIFunkcja{i}.json";
                JsonSerializer jsonSerializer = new JsonSerializer();
                if (File.Exists(filePath)) File.Delete(filePath);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(ProFunk, Formatting.Indented));

                //StreamWriter sw = new StreamWriter(filePath);
                //JsonConvert.SerializeObject(ProFunk, Formatting.Indented);
                ////JsonWriter jsonWriter = new JsonTextWriter(sw);

                //jsonSerializer.Serialize(jsonWriter, ProFunk);

                //jsonWriter.Close();
                //sw.Close();
                i++;
            }
        }
    }
}
