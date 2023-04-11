using InitialProject.Model;
using InitialProject.Observer;
using InitialProject.Repository;
using Microsoft.VisualStudio.Services.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourPointForm.xaml
    /// </summary>
    public partial class TourPointForm : Window, INotifyPropertyChanged
    {
        private readonly int tourId;

        private readonly TourPointRepository _tourPointRepository;

        public static ObservableCollection<string> _keyPoints { get; set; }


        public List<int> availableOrders;

        public List<int> orders;

        public List<int> usedOrders;

        public readonly List<TourPoint> tempTourPoints;



        public TourPointForm(int tourID, ObservableCollection<string> KeyPoints)
        {

            DataContext = this;
            InitializeComponent();
            tourId = tourID;

            _tourPointRepository = new TourPointRepository();
            _keyPoints = KeyPoints;

            TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.getAllTemp());
            tempTourPoints = new List<TourPoint>(_tourPointRepository.getAllTemp());

            availableOrders = availableOrder();


        }


        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<TourPoint> _tourPoints;
        public ObservableCollection<TourPoint> TourPoints
        {
            get { return _tourPoints; }
            set
            {
                _tourPoints = value;
                OnPropertyChanged(nameof(TourPoints));
                SaveButtonEnabled();
            }
        }

        private TourPoint _selectedTourPoint;
        public TourPoint SelectedTourPoint
        {
            get { return _selectedTourPoint; }
            set
            {
                _selectedTourPoint = value;
                OnPropertyChanged(nameof(SelectedTourPoint));
                
            }
        }

         
        private string _Name;
        public string NameTourPoint
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
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


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public List<int> OredersCounter(List<TourPoint> tourPoints)
        {
            List<int> list = new List<int>();
            int i = 0;

            foreach (TourPoint tourPoint in _tourPoints)
            {
                i++;
                list.Add(i);
            }

            return list;
        }

        public List<int> UsedOrder(List<TourPoint> tourPoints)
        {
            List<int> list = new List<int>();
            foreach(TourPoint tour in tourPoints)
            {
                list.Add(tour.Order);
            }
            return list;
        }

        public List<int> availableOrder()
        {
            orders = OredersCounter(_tourPoints.ToList());
            usedOrders = UsedOrder(_tourPoints.ToList());
            availableOrders = orders.Except(usedOrders).ToList();
            return availableOrders;
        }


        public void SetOrederToZero()
        {
            foreach(var TourPoint in _tourPointRepository.getAllTemp())
            {
                TourPoint.Order = 0;
                _tourPointRepository.UpdateTemp(TourPoint);
            }
            TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.getAllTemp());
        }


        
        public void SaveButtonEnabled()
        {
            try
            {
                if (TourPoints.Count >= 2 && CheckForToursOrder() == true)
                {
                    SaveButtoon.IsEnabled = true;
                }
                else
                {
                    SaveButtoon.IsEnabled = false;
                }

            }catch(Exception e)
            {

            }
        }

        public bool CheckForToursOrder()
        {
            foreach(var keyPoint in TourPoints)
            {
                if(keyPoint.Order == 0)
                    return false;
            }
            return true;
        }

        public bool CheckIfListAreSame()
        {
            if(TourPoints.Count == tempTourPoints.Count)
            {
                foreach (var tourPointTemp in tempTourPoints)
                    foreach (var TourPoint in TourPoints)
                        if (tourPointTemp == TourPoint) return true;
            }
            return false;
        }
        

        public TourPoint CreateTourPoint()
        {
            TourPoint tp = new TourPoint();
            tp.TourId = tourId;
            tp.Name = NameTourPoint;
            tp.Description = Description;
            tp.Id = _tourPointRepository.NextIdTemp();
            return tp;
        }

        public void RefreshTourPoints()
        {
            TourPoints.Clear();
            List<TourPoint> list = new List<TourPoint>();
            list = _tourPointRepository.getAllTemp();
            foreach (var tourpoint in list)
            {
                TourPoints.Add(tourpoint);
            }
        }



        private void AddTourPoint_ButtonClick(object sender, RoutedEventArgs e)
        {

            _tourPointRepository.SaveTemp(CreateTourPoint());
            RefreshTourPoints();
            _keyPoints.Clear();
            foreach(var TourPoint in TourPoints)
            {
                _keyPoints.Add(TourPoint.Name);
            }
            TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.getAllTemp());


        }

        private void EditButton(object sender, RoutedEventArgs e)
        {

            if (SelectedTourPoint == null)
                return; //napraviti window da nije selektovano
            else
            {
                availableOrder();
                EditTourPointForm editTour = new EditTourPointForm(SelectedTourPoint, availableOrders, orders, usedOrders, TourPoints);
                editTour.ShowDialog();
                SaveButtonEnabled();
                TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.getAllTemp());
            }

            CollectionViewSource.GetDefaultView(_tourPoints).Refresh();


        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            if (CheckIfListAreSame() == true)
                Close();
            else
            {
                _tourPointRepository.ClearTemp();
                _keyPoints.Clear();
                foreach (var tourPoint in tempTourPoints)
                {
                    _tourPointRepository.SaveTemp(tourPoint);
                    _keyPoints.Add(tourPoint.Name);
                }
                TourPoints = new ObservableCollection<TourPoint>(tempTourPoints);
                Close();
            }

        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            _tourPointRepository.DeleteTemp(SelectedTourPoint);
            TourPoints.Remove(SelectedTourPoint);
            SetOrederToZero();
        }

    }
}
