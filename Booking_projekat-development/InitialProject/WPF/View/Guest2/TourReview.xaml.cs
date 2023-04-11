namespace InitialProject.View.Guest2
{
    using InitialProject.Constants;
    using InitialProject.Model;
    using InitialProject.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    public partial class TourReview : Window
    {
        public static ObservableCollection<string> Rating { get; set; }
        public TourRate TourRate { get; set; }
        private TourRateService _tourRateService;

        public TourReview(int tourId, int userId)
        {
            TourRate = new TourRate();
            _tourRateService = new TourRateService();
            TourRate.TourId = tourId;
            TourRate.GuestId = userId;
            InitializeComponent();
            DataContext = this;
            InitializeRating();
            TourRate.Images = new List<string>();

        }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
            }
        }

        private void InitializeRating()
        {
            Rating = new ObservableCollection<string>();
            for (int i = 1; i <= 5; i++)
            {
                Rating.Add(i.ToString());
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            _tourRateService.MakeTourRate(TourRate);
            MessageBox.Show(TourViewConstants.CommentNoted, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (!TourRate.Images.Contains(Image))
            {
                TourRate.Images.Add(Image);
            }
        }
    }
}
