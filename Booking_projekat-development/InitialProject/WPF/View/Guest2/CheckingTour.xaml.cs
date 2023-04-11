namespace InitialProject.View.Guest2
{
    using InitialProject.CustomClasses;
    using InitialProject.Model;
    using InitialProject.Repository;
    using InitialProject.Services;
    using System.Windows;

    /// <summary>
    /// Interaction logic for CheckingTour.xaml
    /// </summary>
    public partial class CheckingTour : Window
    {
        public Tour Tour { get; }
        private readonly TourService _tourService;
        private readonly TourAttendanceService _tourAttendanceService;
        private TourAttendance _tourAttendance;

        public CheckingTour(TourAttendance tourAttendance)
        {
            _tourService = new TourService();
            _tourAttendanceService = new TourAttendanceService();
            Tour = _tourService.GetTourById(tourAttendance.TourId);
            _tourAttendance = tourAttendance;
            InitializeComponent();
            DataContext = this;
        }



        private void Odbij_Click(object sender, RoutedEventArgs e)
        {
            _tourAttendanceService.RejectTourAttendance(_tourAttendance);
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            _tourAttendanceService.ConfirmTourAttendance(_tourAttendance);
            this.Close();

        }
    }
}
