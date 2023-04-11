using InitialProject.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View.Guide
{
    public partial class CancelTour : Window
    {
        private readonly CancelTourViewModel viewModel;
        public CancelTour()
        {
            InitializeComponent();
            viewModel = new CancelTourViewModel();
            DataContext = viewModel;
            Cancelbutton.IsEnabled = false;
        }

        //create click 
        private void QuitButton(object sender, RoutedEventArgs e)
        {
            //viewModel.CancelTour();
            this.Close();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            viewModel.CancelTour();
            this.Close();
        }

        private void EnableButton(object sender, SelectionChangedEventArgs e)
        {
            Cancelbutton.IsEnabled = true;
        }
    }
}
