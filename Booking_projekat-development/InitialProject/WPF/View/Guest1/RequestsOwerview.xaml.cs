using InitialProject.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RequestsOwerview.xaml
    /// </summary>
    public partial class RequestsOwerview : Window
    {
        private int _userId;
        private RequestsOverviewViewModel viewModel;
        public RequestsOwerview(int userId)
        {
            InitializeComponent();
            _userId = userId;
            viewModel = new RequestsOverviewViewModel(userId);
            this.DataContext = viewModel;
        }

        private void ChangeReservation_Click(object sender, RoutedEventArgs e)
        {
            ReservationChange reservationChange = new ReservationChange(_userId, viewModel.Requests);
            reservationChange.Owner = this;
            reservationChange.ShowDialog();
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            CancelReservation cancelReservation = new CancelReservation(_userId);
            cancelReservation.Owner = this;
            cancelReservation.ShowDialog();
        }
    }
}
