using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static InitialProject.CustomClasses.TourAttendance;
using static InitialProject.Model.TourPoint;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourPointStatus.xaml
    /// </summary>
    public partial class TourPointStatus : Window, INotifyPropertyChanged
    {

        private readonly TourPointRepository _tourPointRepository;
        public ObservableCollection<TourPoint> _tourPoints;
        private readonly TourPoint _selectedTourPoint;
        public static ObservableCollection<Reservation> Reservations;
        public List<TourAttendance> TourAttendances;

        public TourPointStatus(ObservableCollection<TourPoint> tourPoints, TourPoint selectedTourPoint, List<TourAttendance> usersOnTour)
        {
            DataContext = this;
            InitializeComponent();
            TourPoints = tourPoints;
            _selectedTourPoint = selectedTourPoint;
            _tourPointRepository = new TourPointRepository();
            TourAttendances = usersOnTour;
            Users = new ObservableCollection<TourAttendance>(TourAttendances);
            


        }


        private ObservableCollection<TourPoint> _points;
        public ObservableCollection<TourPoint> TourPoints
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged(nameof(TourPoints));
            }
        }

        private ObservableCollection<TourAttendance> _users;
        public ObservableCollection<TourAttendance> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private TourAttendance _selectedTourAttendance;
        public TourAttendance SelectedTourAttendance
        {
            get { return _selectedTourAttendance; }
            set
            {
                _selectedTourAttendance = value;
                OnPropertyChanged(nameof(SelectedTourAttendance));

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleActiveStatus(Status status)
        {
            if (status == Status.Finished)
            {
                _selectedTourPoint.CurrentStatus = Status.Finished;
                _tourPointRepository.Update(_selectedTourPoint);

                if (TourPoints.Last() != _selectedTourPoint)
                {
                    int index = TourPoints.IndexOf(_selectedTourPoint) + 1;
                    TourPoints[index].CurrentStatus = Status.Active;
                    _tourPointRepository.Update(TourPoints[index]);
                }
            }
        }

        private void HandleNotActiveStatus()
        {
            foreach (var tourPoint in TourPoints)
            {
                if (tourPoint.CurrentStatus == Status.Active)
                {
                    tourPoint.CurrentStatus = Status.Finished;
                }
            }

            _selectedTourPoint.CurrentStatus = Status.Active;
        }



        private void ChangeStatusButton(object sender, RoutedEventArgs e)
        {
            ChangeTourAttendanceStatus changeStatus = new ChangeTourAttendanceStatus(SelectedTourAttendance, Users);
            changeStatus.ShowDialog();
            Users = new ObservableCollection<TourAttendance>(TourAttendances);
        }



        private void SaveButton(object sender, RoutedEventArgs e)
        {
            Status status = (Status)Enum.Parse(typeof(Status), StatusComboBox.Text);

            if (_selectedTourPoint.CurrentStatus == Status.Active)
            {
                HandleActiveStatus(status);
            }
            else if (_selectedTourPoint.CurrentStatus == Status.NotActive)
            {
                HandleNotActiveStatus();
            }

            Close();
        }



        
    }
}
