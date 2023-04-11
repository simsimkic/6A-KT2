using InitialProject.Model;
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

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1Window.xaml
    /// </summary>
    public partial class Guest1Window : Window
    {
        private int _userId;
        public static ObservableCollection<Location> Locations;
        public Guest1Window(int userId, ObservableCollection<Location> locations)
        {
            InitializeComponent();
            _userId = userId;
            Locations = locations;
        }

        private void AccommodationDisplay_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDisplay accommodationDisplay = new AccommodationDisplay(Locations, _userId);
            accommodationDisplay.ShowDialog();
        }

        private void RequestsOverview_Click(object sender, RoutedEventArgs e)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview(_userId);
            requestsOwerview.ShowDialog();
        }

        private void OwnerRating_Click(object sender, RoutedEventArgs e)
        {
            OwnerRating ownerRating = new OwnerRating(_userId);
            ownerRating.ShowDialog();
        }
    }
}
