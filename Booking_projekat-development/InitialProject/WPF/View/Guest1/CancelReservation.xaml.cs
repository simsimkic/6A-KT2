using InitialProject.CustomClasses;
using InitialProject.Services;
using InitialProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CancelReservation.xaml
    /// </summary>
    public partial class CancelReservation : Window
    {
        private CancelReservationViewModel viewModel;
        public CancelReservation(int userId)
        {
            InitializeComponent();
            viewModel = new CancelReservationViewModel(userId);
            DataContext = viewModel;
            CancelButton.IsEnabled = false;
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CancelReservation();
            this.Close();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EnableButton(object sender, SelectionChangedEventArgs e)
        {
            CancelButton.IsEnabled = true;
        }
    }
}
