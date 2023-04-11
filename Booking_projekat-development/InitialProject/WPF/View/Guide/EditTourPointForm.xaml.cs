using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.VisualStudio.Services.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for EditTourPointForm.xaml
    /// </summary>
    public partial class EditTourPointForm : Window, INotifyPropertyChanged
    {
        private readonly TourPoint _tourPoint;
        private readonly TourPointRepository _tourPointRepository;

        private ObservableCollection<TourPoint> _tourPoints;
        public ObservableCollection<TourPoint> TourPoints
        {
            get { return _tourPoints; }
            set
            {
                _tourPoints = value;
                OnPropertyChanged(nameof(TourPoints));
            }
        }

        public static ObservableCollection<int> Orders { get; set; }

        

        public List<int> _availableOrders;
        public List<int> _orders;
        public List<int> _usedOrders;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditTourPointForm(TourPoint tourPoint, List<int> availableOrders, List<int> orders,List<int> usedOrders, ObservableCollection<TourPoint> tourPoints )
        {
            _tourPoint = tourPoint;
            _availableOrders = availableOrders;
            _orders = orders;
            _usedOrders = usedOrders;
            _tourPointRepository = new TourPointRepository();
            TourPoints = tourPoints;
            InitializeComponent();
            DataContext = this;
            FirstName.Content = _tourPoint.Name;
            Orders = new ObservableCollection<int>(_orders);
        }





        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            int broj = int.Parse(OrderComboBox.Text);
            int index = TourPoints.IndexOf(_tourPoint);
            _tourPointRepository.UpdateTempOrder(_tourPoint, broj);
            TourPoints[index].Order = broj;

            Close();

        }
    }
}
