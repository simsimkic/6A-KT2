using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Services.Common;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for DateTimePicker.xaml
    /// </summary>
    public partial class DateTimePicker : Window, INotifyPropertyChanged
    {
        ObservableCollection<DateTime> dates;
        
        public DateTimePicker(ObservableCollection<DateTime> dateAndTimes)
        {
            DataContext = this;
            InitializeComponent();
            dates = dateAndTimes;
            Hours = new ObservableCollection<string>();
            Minutes = new ObservableCollection<string>();

            LoadHoursAndMinutes();
        }
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            DateTime dateWithCorrectTime = new DateTime(
                Date.Year,
                Date.Month,
                Date.Day,
                int.Parse(HoursComboBox.Text),
                int.Parse(MinutesComboBox.Text),
                DateTime.Now.Second
            );

            dates.Add(dateWithCorrectTime);

            Close();
        }


        public void LoadHoursAndMinutes()
        {
            List<string> minutes = MinutesCounter();
            List<string> hours = HourCounter();

            // clear existing lists
            Hours.Clear();
            Minutes.Clear();

            // add hours and minutes to respective lists
            foreach (var hour in hours)
            {
                Hours.Add(hour);
            }
            foreach (var minute in minutes)
            {
                Minutes.Add(minute);
            }

        }

        public List<string> HourCounter()
        {
            List<string> hours = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i.ToString("D2"));
            }
            return hours;
        }

        public List<string> MinutesCounter()
        {
            List<string> minutes = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                minutes.Add(i.ToString("D2"));
            }
            return minutes;
        }

        private ObservableCollection<string> _hours;
        public ObservableCollection<string> Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _minutes;
        public ObservableCollection<string> Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        private DateTime _Date;
        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
