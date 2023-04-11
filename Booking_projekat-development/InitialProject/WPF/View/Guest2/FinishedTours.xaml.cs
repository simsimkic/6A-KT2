namespace InitialProject.View.Guest2
{
    using InitialProject.Constants;
    using InitialProject.Model;
    using InitialProject.Services;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourService _tourService;
        private readonly TourAttendanceService _tourAttendanceService;
        public int UserId { get; set; }


        public FinishedTours(int userId)
        {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _tourService = new TourService();
            _tourAttendanceService = new TourAttendanceService();
            Tours = new ObservableCollection<Tour>(_tourService.GetAllFinished(UserId));
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                if (_tourAttendanceService.CheckPossibleComment(UserId, SelectedTour.TourId))
                {
                    TourReview tourReview = new TourReview(SelectedTour.TourId, UserId);
                    tourReview.Show();
                }
                else
                {
                    MessageBox.Show(TourViewConstants.TourReviewed, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                }
            }
        }
    }
}
