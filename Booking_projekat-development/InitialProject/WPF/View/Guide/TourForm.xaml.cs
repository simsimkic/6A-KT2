using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View.Guide;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window, INotifyPropertyChanged
    {
        private readonly LanguageRepository _languageRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly LocationRepository _locationRepository;
        private readonly TourImagesRepository _tourImagesRepository;
        private readonly TourRepository _tourRepository;
        private readonly int tourId;
        public static ObservableCollection<Language> Languages { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> KeyPoints { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<DateTime> DateAndTime { get; set; }

        public static ObservableCollection<TourImages> Images = new ObservableCollection<TourImages>();
        public TourForm()
        {
            InitializeComponent();
            DataContext = this;
            _languageRepository = new LanguageRepository();
            _tourRepository = new TourRepository();
            _tourPointRepository = new TourPointRepository();
            _locationRepository= new LocationRepository();
            _tourImagesRepository = new TourImagesRepository();
            tourId = _tourRepository.NextId();
            Languages = new ObservableCollection<Language>(_languageRepository.GetAll());
            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            KeyPoints = new ObservableCollection<string>();
            DateAndTime = new ObservableCollection<DateTime>();
            ReadCitiesAndCountries();
            _tourPointRepository.ClearTemp();

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(_language));
            }
        }



        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(_Description));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
            }
        }

        private string _imageUrl;

        public string TourImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged(nameof(_imageUrl));
                }
            }
        }



        private string _MaxGuests;
        public string MaxGuests
        {
            get { return _MaxGuests; }
            set
            {
                _MaxGuests = value;
                OnPropertyChanged(nameof(_MaxGuests));
            }
        }



        private string _TourDuratation;
        public string TourDuratation
        {
            get { return _TourDuratation; }
            set
            {
                _TourDuratation = value;
                OnPropertyChanged(nameof(_TourDuratation));
            }
        }

        private void FilterCities(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cmbx) return;

            
            string country = cmbx.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(country))
            {
                ReadCitiesAndCountries();
            }
            else
            {
                UpdateCitiesList(country);
            }
        }

        private void UpdateCitiesList(string country)
        {
            Cities.Clear();
            Cities.Add("");

            
            var filteredCities = Locations.Where(loc => loc.Country == country)
                                          .Select(loc => loc.City);

            foreach (string city in filteredCities)
            {
                Cities.Add(city);
            }

            CityComboBox.SelectedIndex = 1;
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            Cities.Add("");
            Countries.Add("");

            
            foreach (Location location in Locations)
            {
                Cities.Add(location.City);
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }


        public Tour GetInputValues()
        {
            Language language = new();
            Language languageForTour = language.fromStringToLanguage(LanguageComboBox.Text);
            Location location = new Location { Country = CountriesComboBox.Text, City = CityComboBox.Text };

            Tour tourToSave = new Tour
            {
                Name = NameTextBox.Text,
                MaxGuestNumber = int.Parse(MaxGuests),
                Duration = int.Parse(TourDuratation),
                Location = location,
                Description = Description,
                Language = languageForTour
            };

            return tourToSave;

        }

        public void SaveTours(int objectQunatity, Tour tour, List<DateTime> dates)
        {
            for (int i = 0; i < objectQunatity; i++)
            {
                tour.StartingDateTime = dates[i];
                _tourRepository.Save(tour);
            }
        }

        public void SaveTourPoints(int howManyObjectsToBuild)
        {
            List<TourPoint> pointsToSave = _tourPointRepository.getAllTemp();
            int startTourId = tourId;
            for (int i = 0; i < howManyObjectsToBuild; i++)
            {
                int currentTourId = startTourId + i;
                foreach (var tourPoint in pointsToSave)
                {
                    tourPoint.TourId = currentTourId;
                    _tourPointRepository.Save(tourPoint);
                }
            }
        }

        public void SaveImages(int howManyObjectsToBuild)
        {
            for (int i = 0; i < howManyObjectsToBuild; i++)
            {
                foreach (var image in Images)
                {
                    image.TourId = image.TourId + i;
                    _tourImagesRepository.Save(image);
                }
            }

        }




        private void AddKeyPointButton(object sender, RoutedEventArgs e)
        {
            TourPointForm addTourPoint = new TourPointForm(tourId, KeyPoints);
            addTourPoint.Show();
        }

        private void CancelTourButton(object sender, RoutedEventArgs e)
        {
            CancelTour cancelTour = new CancelTour();
            cancelTour.Show();
            //Close();
        }

        

        private void StatisticButton(object sender, RoutedEventArgs e)
        {
            TourStatistic tourStatistic = new TourStatistic();
            tourStatistic.Show();
            //Close();
        }

        private void AddDatesAndTimesButton(object sender, RoutedEventArgs e)
        {
            DateTimePicker date = new DateTimePicker(DateAndTime);
            date.Show();
        }

        private void AddPicturesButton(object sender, RoutedEventArgs e)
        {
            TourImages newImage = new TourImages();
            newImage.TourId = tourId;
            newImage.Url= TourImageUrl;
            Images.Add(newImage);
        }
        

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            _tourPointRepository.ClearTemp();
            Close();

        }

        private void TrackButton(object sender, RoutedEventArgs e)
        {

            TourTracking tt = new TourTracking();
            tt.Show();
            Close();
        }

        private void SaveTourButton(object sender, RoutedEventArgs e)
        {
            Tour uncompletedTour = GetInputValues();
            List<DateTime> dates = DateAndTime.ToList();
            int howManyObjectsToBuild = dates.Count();

            SaveTours(howManyObjectsToBuild,uncompletedTour,dates);
            SaveTourPoints(howManyObjectsToBuild);
            SaveImages(howManyObjectsToBuild);
            

            Close();
        }


     



    }
}
