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
using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.Win32;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RegisterNewAccommodation.xaml
    /// </summary>
    public partial class RegisterNewAccommodation : Window,IDataErrorInfo
    {

        public UrlTable urlTable;

        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationImageRepository _accommodationImageRepository;
        private readonly LocationRepository _locationRepository;
        private readonly UserToReviewRepository _userToReviewRepository;
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<UserToReview> UsersToReview { get; set; }

        public static ObservableCollection<AccommodationImage>Images { get; set; }

        private string _accommodationName;

        private string _maxGuests;

        private string _minDays;

        private string _cancelationDays;

        private string _imageUrl;

        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }


        
        public string AccommodationMaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationReservationMinDays
        {
            get => _minDays;
            set
            {
                if (value != _minDays)
                {

                    _minDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationCancelationDays
        {
            get => _cancelationDays;
            set
            {
                if (value != _cancelationDays)
                {
                    _cancelationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationImageUrl
        {
            get => _imageUrl;
            set
            {
                if(value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterNewAccommodation()
        {
            InitializeComponent();
            DataContext = this;

            Images = new ObservableCollection<AccommodationImage>();
            urlTable = new UrlTable(Images);

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _accommodationImageRepository= new AccommodationImageRepository();
            _userToReviewRepository = new UserToReviewRepository();

            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            UsersToReview = new ObservableCollection<UserToReview>(_userToReviewRepository.GetByOwnerId(0));

            AccommodationCancelationDays = "1";
            RateNotification();
            ReadCitiesAndCountries();
        }

        private void RateNotification()
        {
            foreach(UserToReview userToReview in UsersToReview) 
            { 
                if(CheckDateRange(userToReview.LeavingDay)) // 0 je defaultni owner id
                {
                    RateUser(userToReview.Guest1Id, userToReview.LeavingDay);
                }
                else
                {
                    _userToReviewRepository.DeleteByIdAndDate(userToReview.Guest1Id, userToReview.LeavingDay);
                }
            }
        }
        private void RateUser(int userID, DateTime date)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Rate User", "You can still rate user", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                GuestReviewForm reviewForm = new GuestReviewForm(userID);
                reviewForm.ShowDialog();
                if (reviewForm.IsReviewd)
                {
                    _userToReviewRepository.DeleteByIdAndDate(userID, date);
                }
            }
        }
        private bool CheckDateRange(DateTime date)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(-5);
            if(startDate >= date && endDate <= date)
            {
                return true;
            }
            return false;
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            Cities.Add("");
            Countries.Add("");
            foreach (Location l in Locations)
            {
                Cities.Add(l.City);
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
        }

        private void FilterCities(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            string country = "";
            try
            {
                if (cmbx.SelectedItem != null)
                {
                    country = cmbx.SelectedItem.ToString();
                }
                else
                {
                    cmbx.SelectedItem = 0;
                }
            }
            catch (System.NullReferenceException)
            {
                ReadCitiesAndCountries();
            }
            if (country == "")
            {

                ReadCitiesAndCountries();
            }
            else
            {
                Cities.Clear();
                Cities.Add("");
                foreach (Location loc in Locations)
                {
                    if (loc.Country == country)
                    {
                        Cities.Add(loc.City);
                    }
                }
                CityComboBox.SelectedIndex = 1;
            }
        }

        private void NewAccommodationRegistrationButton(object sender, RoutedEventArgs e)
        {
            if (IsValid )
            {
                Accommodation newAccommodation = CreateNewAccommodation();
                SaveAccommodation(newAccommodation);
                SaveAccommodationImages(Images);
                Close();
            }
            else
            {
                MessageBox.Show("Not all fields are filled correctly");
            }
        }

        private Accommodation CreateNewAccommodation()
        {
            return new Accommodation
            {
                Name = AccommodationName,
                MaxGuestNumber = Convert.ToInt32(AccommodationMaxGuests),
                DaysBeforeCancelling = Convert.ToInt32(AccommodationCancelationDays),
                MinReservationDays = Convert.ToInt32(AccommodationReservationMinDays),
                Location = new Location(CountryComboBox.Text, CityComboBox.Text),
                TypeOfAccommodation = GetAccommodationType()

            };
        }

        private AccommodationType GetAccommodationType()
        {
            switch (TypeComboBox.Text)
            {
                case "Appartment":
                    return AccommodationType.Apartment;
                case "Shack":
                    return AccommodationType.Shack;
                default:
                    return AccommodationType.House;

            }
        }

        private void SaveAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Save(accommodation);
        }

        private void SaveAccommodationImages(ObservableCollection<AccommodationImage> images)
        {
            foreach(var image in images)
            {
                _accommodationImageRepository.Save(image, _accommodationRepository.GetLastAccommodationId());
            }
        }
        

        private Regex _numberBoxRegex = new Regex("[1-9][0-9]{0,2}");

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "AccommodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                        return "Field equired";

                }
                else if (columnName == "AccommodationMaxGuests")
                {
                    if (string.IsNullOrEmpty(AccommodationMaxGuests))
                        return "Field required";

                    Match match = _numberBoxRegex.Match(AccommodationMaxGuests);
                    if (!match.Success)
                        return "Incorect format";
                }

                else if (columnName == "AccommodationReservationMinDays")
                {

                    if (string.IsNullOrEmpty(AccommodationReservationMinDays))
                        return "Field required";

                    Match match = _numberBoxRegex.Match(AccommodationReservationMinDays);
                    if (!match.Success)
                        return "Incorect format";
                }

                else if (columnName == "AccommodationCancelationDays")
                {

                    if (string.IsNullOrEmpty(AccommodationCancelationDays))
                        return "Field required";

                    Match match = _numberBoxRegex.Match(AccommodationCancelationDays);
                    if (!match.Success)
                        return "Incorect format";
                }

                else if (columnName == "TypeComboBox")
                {
                    if (string.IsNullOrEmpty(TypeComboBox.Text))
                        return "Field required";
                }

                else if (columnName == "CountryComboBox")
                {
                    if (string.IsNullOrEmpty(CountryComboBox.Text))
                        return "Field required";
                }

                else if (columnName == "CityComboBox")
                {
                    if (string.IsNullOrEmpty(CityComboBox.Text))
                        return "Field required";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "AccommodationName", "AccommodationMaxGuests", "AccommodationReservationMinDays", "AccommodationCancelationDays", "TypeComboBox", "CountryComboBox", "CityComboBox" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }


        private void ViewUrlTableButton(object sender, RoutedEventArgs e)
        {
            urlTable.Show();
           
        }
       

        private void AddUrlButton(object sender, RoutedEventArgs e)
        {
            AccommodationImage newImage = new AccommodationImage() ;
            newImage.Url = UrlTextBox.Text;
            Images.Add(newImage);
           
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
