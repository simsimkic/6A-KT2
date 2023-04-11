using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Services;
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

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for OwnerRating.xaml
    /// </summary>
    public partial class OwnerRating : Window, INotifyPropertyChanged
    {
        public List<OwnerToRate> ownersToRate;
        public ObservableCollection<KeyValuePair<int, string>> AccommodationsName { get; set; }
        private OwnerToRateService ownerToRateService;
        private OwnerRateService ownerRateService;
        public List<string> NekaLista;
        public List<string> Grades { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public int SelectedAccommodationId { get; set; }
        public List<string> Images { get; set; }
        public int Cleanliness { get; set; }
        private string _strCleanliness;
        public string StrCleanliness
        {
            get => _strCleanliness;
            set
            {
                if (value != _strCleanliness)
                {
                    try
                    {
                        int _cleanliness;
                        int.TryParse(value, out _cleanliness);
                        Cleanliness = _cleanliness;
                    }
                    catch (Exception) { }
                    _strCleanliness = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Correctness { get; set; }
        private string _strCorrectness;
        public string StrCorrectness
        {
            get => _strCorrectness;
            set
            {
                if (value != _strCorrectness)
                {
                    try
                    {
                        int _correctness;
                        int.TryParse(value, out _correctness);
                        Correctness = _correctness;
                    }
                    catch (Exception) { }
                    _strCorrectness = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AdditionalComment { get; set; }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;         
            }
        }
        private readonly int _userId;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public OwnerRating(int userId)
        {
            InitializeComponent();
            InitializeGrades();
            _userId = userId;
            DataContext = this;
            ownerToRateService = new OwnerToRateService(userId);
            ownerRateService = new OwnerRateService();
            Images = new List<string>();
            AccommodationsName = new ObservableCollection<KeyValuePair<int, string>>(ownerToRateService.GetAccommodationNamesByUser(userId));
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            if (!Images.Contains(Image))
            {
                Images.Add(Image);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            ownerToRateService.DeleteOwnerToRate(SelectedAccommodationId);
            int ownerId = ownerToRateService.GetOwnerIdByAccommodationId(SelectedAccommodationId);
            OwnerRate ownerRate = new OwnerRate(_userId, ownerId,SelectedAccommodationId, Cleanliness, Correctness, AdditionalComment, Images);
            this.Close();
            ownerRateService.SaveRate(ownerRate);
        }
        private void InitializeGrades()
        {
            Grades = new List<string>();
            for(int i = 1; i < 6; i++)
            {
                Grades.Add(i.ToString());
            }
        }
    }
}
