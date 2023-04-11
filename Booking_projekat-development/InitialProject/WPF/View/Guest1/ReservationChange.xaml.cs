using InitialProject.Model;
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
    /// Interaction logic for ReservationChange.xaml
    /// </summary>
    public partial class ReservationChange : Window
    {
        private ReservationChangeViewModel viewModel;
        public ReservationChange(int userId, ObservableCollection<ChangeReservationRequest> Requests)
        {
            InitializeComponent();
            viewModel = new ReservationChangeViewModel(userId, Requests);
            DataContext = viewModel;
            Send_Button.IsEnabled = false;
        }
        private void SendRequest_Button(object sender, RoutedEventArgs e)
        {
            viewModel.SendRequest_Button();
            this.Close();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.ComboBox_SelectionChanged(CheckInPicker, CheckOutPicker);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
