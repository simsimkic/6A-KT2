using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for UrlTable.xaml
    /// </summary>
    public partial class UrlTable : Window
    {
        public static ObservableCollection<AccommodationImage> Images { get; set; }

        
       
        public UrlTable(ObservableCollection<AccommodationImage> ImageList)
        {
            InitializeComponent();
            DataContext= this;

            // Images = new ObservableCollection<AccommodationImages>();
            Images = ImageList;
          
        }
    }
}
