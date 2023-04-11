namespace InitialProject.View
{
    using InitialProject.Constants;
    using InitialProject.CustomClasses;
    using InitialProject.Model;
    using InitialProject.Repository;
    using InitialProject.Services;
    using InitialProject.View.Guest2;
    using Microsoft.TeamFoundation.Common;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public partial class TourView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<string> Duration { get; set; }
        public static ObservableCollection<int> GuestNumber { get; set; }
        public static ObservableCollection<string> Languages { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }

        public static ObservableCollection<TourAttendance> TourAttendances { get; set; }
        private int UserId { get; }
        public Tour SelectedTour { get; set; }
        public int NumberOfGuests { get; set; }
        public string SelectedLanguage { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedGuestNumber { get; set; }
        public string SelectedDurationFrom { get; set; }
        public string SelectedDurationTo { get; set; }

        private readonly TourService _tourService;

        private readonly TourAttendanceService _tourAttendanceService;

        private ObservableCollection<Tour> _tours { get; set; }
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public TourView(int userId)
        {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _tourService = new TourService();
            _tourAttendanceService = new TourAttendanceService();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Tours = new ObservableCollection<Tour>(_tourService.GetAllNotStartedTours());

            InitializeLanguages();
            InitializeLocations();
            InitializeGuestNumber();
            InitializeDuration();
            ReadCitiesAndCountries();
            CheckTourAttendance();
        }

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (value != _selectedCountry)
                {
                    _selectedCountry = value;
                    FilterCities();
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }

        private void InitializeLanguages()
        {
            Languages = new ObservableCollection<string>();
            Languages.Insert(0, string.Empty);
            foreach (Tour t in Tours)
            {

                if (!Languages.Contains(t.Language.Name))
                {
                    Languages.Add(t.Language.Name);
                }

            }
        }

        private void InitializeLocations()
        {
            Locations = new ObservableCollection<Location>();
            foreach (Tour t in Tours)
            {
                if (!Locations.Contains(t.Location))
                {
                    Locations.Add(t.Location);
                }
            }
        }


        private void InitializeGuestNumber()
        {
            GuestNumber = new ObservableCollection<int>();
            int max = 1;
            foreach (Tour t in Tours)
            {
                if (max < t.MaxGuestNumber)
                {
                    max = t.MaxGuestNumber;
                }
            }
            for (int i = 1; i <= max; i++)
            {
                GuestNumber.Add(i);
            }
        }

        private void InitializeDuration()
        {
            Duration = new ObservableCollection<string>();
            int max = 1;
            foreach (Tour t in Tours)
            {
                if (max < t.Duration)
                {
                    max = t.Duration;
                }
            }
            for (int i = 1; i <= max; i++)
            {
                Duration.Add(i.ToString());
            }
            Duration.Insert(0,string.Empty);
        }
        private void Rezervisi_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show(TourViewConstants.MustSelectTour, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
            }
            else
            {
                if (SelectedTour.MaxGuestNumber == 0)
                {
                    HandleFullTourCapacity();
                }
                else
                {
                    TourReservation tourReservation = new TourReservation(UserId, SelectedTour, NumberOfGuests);
                    tourReservation.ShowDialog();
                    SelectedTour.ReduceGuestNumber(tourReservation.NumberOfGuests);
                }
            }
        }


        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            Tours = new ObservableCollection<Tour>(_tourService.GetAllFiltered(SelectedCity, SelectedCountry, SelectedDurationFrom, SelectedDurationTo, SelectedLanguage, SelectedGuestNumber));
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            foreach (Location l in Locations)
            {
                if (!Cities.Contains(l.City))
                {
                    Cities.Add(l.City);
                }
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
            Countries.Insert(0, string.Empty);
            Cities.Insert(0, string.Empty);
        }

        private void Filter_Countries(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterCities()
        {
            if (string.IsNullOrEmpty(SelectedCountry))
            {
                ReadCitiesAndCountries();
                SelectedCity = Cities.FirstOrDefault();
            }
            else
            {
                Cities.Clear();
                foreach (Location loc in Locations)
                {
                    if (loc.Country == SelectedCountry && !Cities.Contains(loc.City))
                    {
                        Cities.Add(loc.City);
                    }
                }
                Cities.Insert(0, string.Empty);
                SelectedCity = Cities[0];
            }
        }

        private void HandleFullTourCapacity()
        {
            MessageBoxResult result;
            result = MessageBox.Show(TourViewConstants.MaxGuestNumberIsZero, TourViewConstants.Caption, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                Tours = new ObservableCollection<Tour>(_tourService.GetSimilarAsTourHasFullCapacity(SelectedTour.Location.Country, SelectedTour.Location.City));

                if (Tours.Count == 0)
                {
                    MessageBox.Show(TourViewConstants.ViewOtherTours, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                    Tours = new ObservableCollection<Tour>(_tourService.GetAll());
                }

            }
        }

        private void CheckTourAttendance()
        {
            TourAttendances = new ObservableCollection<TourAttendance>(_tourAttendanceService.GetAllToCheckByUser(UserId));
            if (!TourAttendances.IsNullOrEmpty())
            {
                foreach (var t in TourAttendances)
                {
                    CheckingTour checkingTour = new CheckingTour(t);
                    checkingTour.ShowDialog();
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void ZavrseneTure_Click(object sender, RoutedEventArgs e)
        {
            FinishedTours finishedTours = new FinishedTours(UserId);
            finishedTours.ShowDialog();
        }

        private void RezervisaneTure_Click(object sender, RoutedEventArgs e)
        {
            ReservedTours reservedTours = new ReservedTours(UserId);
            reservedTours.ShowDialog();
        }
    }
}
