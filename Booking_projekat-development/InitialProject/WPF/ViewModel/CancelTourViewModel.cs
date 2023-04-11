using InitialProject.Factory;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InitialProject.ViewModel
{
    public class CancelTourViewModel : INotifyPropertyChanged
    {
        private readonly ICancelTourService _cancelTourService;
        public ObservableCollection<string> Tours { get; set; }
        public CancelTourViewModel()
        {
            _cancelTourService = Injector.cancelTourService();
            LoadTours();
        }

        private string _SelectedTour;
        public string SelectedTour
        {
            get => _SelectedTour;
            set
            {
                if (_SelectedTour != value)
                {
                    _SelectedTour = value;
                }
            }
        }

        private void LoadTours()
        {
            Tours = new ObservableCollection<string>(_cancelTourService.GetAllTwoDaysFromNow().Select(c => c.TourId + " " + c.Name + " " + c.StartingDateTime));
        }

        public void CancelTour()
        {
            _cancelTourService.CancelTour(SelectedTour);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
