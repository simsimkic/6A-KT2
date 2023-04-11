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
using static InitialProject.Model.TourPoint;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for TourStatistic.xaml
    /// </summary>
    public partial class TourStatistic : Window
    {
        private readonly TourStatisticsViewModel _viewModel;
        public TourStatistic()
        {
            _viewModel = new TourStatisticsViewModel();
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void FindTourButton(object sender, RoutedEventArgs e)
        {
            MostVisitedTour.Content = _viewModel.GetMostVisitedTour(_viewModel.Year).Name;
        }

    }
}
