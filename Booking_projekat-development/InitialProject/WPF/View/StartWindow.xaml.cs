using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View.Guest1;
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
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly LocationRepository _locationRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly UserToReviewRepository _userToReviewRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly int _userId;
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Reservation> Reservations { get; set; }
        public static List<GuestReview> GuestReviews {get; set;}
        public static ObservableCollection<UserToReview> UsersToReview { get; set;}
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public StartWindow(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _locationRepository = new LocationRepository();
            _accommodationRepository = new AccommodationRepository();
            _guestReviewRepository= new GuestReviewRepository();
            _reservationRepository= new ReservationRepository();
            _userToReviewRepository = new UserToReviewRepository();
            Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
            GuestReviews = new List<GuestReview>(_guestReviewRepository.GetAll());
            Locations = new ObservableCollection<Location>(_accommodationRepository.GetAllLocationsFromAccommodations());
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            _userId = userId;
            InitializeUsersToReview();
        }

        private void Guest1_ButtonClick(object sender, RoutedEventArgs e)
        {
            Guest1Window guest1View = new Guest1Window(_userId, Locations);
            guest1View.Show();
        }
        private void Guest2_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourView tourView = new TourView(1);
            tourView.Show();
        }
        private void Owner_ButtonClick(object sender, RoutedEventArgs e)
        {
            RegisterNewAccommodation newAccommodation = new RegisterNewAccommodation();
            newAccommodation.Show();
        }
        private void Guide_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourForm tour = new TourForm();
            tour.Show();
        }
        private void OpenRegistrationForm(object sender, RoutedEventArgs e)
        {
            UserLogIn registrationForm = new UserLogIn();
            registrationForm.Show();
        }
        private void InitializeUsersToReview()
        {
            foreach(Reservation reservation in Reservations){
                if (CheckIfLeftReservation(reservation))
                {
                    //_reservationRepository.Delete(reservation);
                    UserToReview userToReview = new UserToReview(0, reservation.UserId, reservation.ReservationDateRange.EndDate); //bez sign in forme defaultni ownerId je 0
                    _userToReviewRepository.Save(userToReview);
                }
            }
        }
        private bool CheckIfLeftReservation(Reservation reservation)
        {
            if(reservation.ReservationDateRange.EndDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
