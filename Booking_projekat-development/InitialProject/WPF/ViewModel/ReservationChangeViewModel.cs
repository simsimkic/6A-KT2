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
using System.Windows.Controls;
using System.Windows;

namespace InitialProject.ViewModel
{
    public class ReservationChangeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ChangeReservationRequestService requestService;
        private ReservationService reservationService;
        private readonly AccommodationService accommodationService;
        private readonly AccommodationReservationService accommodationReservationService;
        private int _userId;
        private int _ownerId;
        public ObservableCollection<KeyValuePair<int, string>> ReservationsForChange { get; set; }
        public int SelectedReservationId { get; set; }
        public DateTime NewCheckInDate { get; set; }
        public DateTime NewCheckOutDate { get; set; }
        private ObservableCollection<ChangeReservationRequest> _requests;
        public ObservableCollection<ChangeReservationRequest> Requests
        {
            get
            {
                return _requests;
            }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReservationChangeViewModel(int userId, ObservableCollection<ChangeReservationRequest> Requests)
        {
            reservationService = new ReservationService();
            accommodationService = new AccommodationService();
            requestService = new ChangeReservationRequestService();
            accommodationReservationService = new AccommodationReservationService();
            _userId = userId;
            this.Requests = Requests;

            InitializeReservationsForChange();
        }
        private void InitializeReservationsForChange()
        {
            ReservationsForChange = new ObservableCollection<KeyValuePair<int, string>>(accommodationReservationService.GetReservationsByUserId(_userId));
        }
        public void SendRequest_Button()
        {
            _ownerId = accommodationService.GetOwnerIdByReservationId(SelectedReservationId);
            string accommodationName = accommodationService.GetNameByReservationId(SelectedReservationId);
            ChangeReservationRequest request = new ChangeReservationRequest(SelectedReservationId, accommodationName, NewCheckInDate, NewCheckOutDate, StatusType.Pending, _userId, _ownerId);
            requestService.SaveRequest(request);
            UpdateRequests(request);
        }
        public void ComboBox_SelectionChanged(DatePicker CheckInPicker, DatePicker CheckOutPicker)
        {
            if (SelectedReservationId != 0)
            {
                CheckInPicker.SelectedDate = reservationService.GetCheckInDate(_userId, SelectedReservationId);
                CheckOutPicker.SelectedDate = reservationService.GetCheckOutDate(_userId, SelectedReservationId);
            }
        }
        private void UpdateRequests(ChangeReservationRequest request)
        {
            bool requestExists = Requests.Any(r => r.ReservationId == request.ReservationId);
            if (!requestExists)
            {
                Requests.Add(request);
            }
        }
    }
}
