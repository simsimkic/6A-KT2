 using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace InitialProject.Model
{
    public class Tour : ISerializable, INotifyPropertyChanged
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        private int _maxGuestNumber;
        public int MaxGuestNumber 
        { get => _maxGuestNumber;
            set 
            {
                if (value != _maxGuestNumber)
                {
                    _maxGuestNumber = value;
                    OnPropertyChanged();
                }
            
            }
            
        }

        public List<TourPoint> KeyPoints { get; set; }
        public DateTime StartingDateTime { get; set; }
        public int Duration { get; set; }
        public bool TourStarted { get; set; }

        //public List<String> Images { get; set; }

        public Tour()
        {
            Name = "";
            Location = new Location();
            Description = "";
            Language = new Language();
            MaxGuestNumber = 1;
            StartingDateTime = DateTime.Now;
            Duration = 1;
            TourStarted = false;
            //Images = new List<String>();
        }

        public Tour(string name, Location location, string description, Language language, int maxGuestNumber, DateTime startingDateTimes, int duration, bool started)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuestNumber = maxGuestNumber;
            StartingDateTime = startingDateTimes;
            Duration = duration;
            TourStarted = started;
            //Images = images;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = Location.fromStringToLocation(values[2]);
            Description = values[3];
            Language = Language.fromStringToLanguage(values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            StartingDateTime = DateTime.Parse(values[6]); 
            Duration = Convert.ToInt32(values[7]);
            TourStarted = bool.Parse(values[8]);
            //Images = values[9].Split(";").ToList<string>();

        }


        public string DateTimeToCSV(List<DateTime> startingDateTimes) 
        {
            StringBuilder dateTimes = new StringBuilder();
            foreach (DateTime startingDateTime in startingDateTimes)
            {
                dateTimes.Append(";" + startingDateTime.ToString());
            }
            
            return dateTimes.ToString().Substring(1);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { TourId.ToString(), Name, Location.ToString(), Description, Language.ToString(), MaxGuestNumber.ToString(), StartingDateTime.ToString(), Duration.ToString(), TourStarted.ToString() };
            return csvValues;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReduceGuestNumber(int guestNumber)
        {
            if (MaxGuestNumber>=guestNumber)
                MaxGuestNumber-=guestNumber;
        }

        public override string ToString()
        {
            return Name;
        }
    }



}
