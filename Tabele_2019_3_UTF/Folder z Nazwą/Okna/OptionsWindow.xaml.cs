using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tabele_2019_3_UTF.Folder_z_Nazwą.Okna
{
    
    public partial class OptionsWindow : Window
    {
        public bool IsDone { get; set; } = false;
        
        public OptionsWindow()
        {
                InitializeComponent();
        }
        
        //private async void asdf()
        //{
        //    await Task.Run(() => { 
            
        //        while (true)
        //        {
        //            //if (IsDone == true)
        //            //{
        //                lad.Visibility = Visibility.Hidden;
        //                break;
        //            //}
        //        }
        //    });
        //}


    }
}
