using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.CustomClasses
{
    public class DateRange
    {
        private DateTime _startDate;
        private DateTime _endDate;
        public DateTime StartDate 
        { 
            get 
            { 
                return _startDate; 
            } 
            set 
            { 
                _startDate = value; 
                SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate); 
            } 
        }
        public DateTime EndDate 
        { 
            get 
            { 
                return _endDate; 
            } 
            set 
            { 
                _endDate = value; 
                SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate); 
            } 
        }
        public string SStartDate { get; set; }
        public string SEndDate { get; set; }

        public DateRange(DateTime start, DateTime end)
        {
            this._startDate = start;
            this._endDate = end;
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }
        public DateRange()
        {
            StartDate = new DateTime();
            EndDate = new DateTime();
        }

        public DateRange(DateTime startDate, int duration)
        {
            StartDate = startDate;
            EndDate = startDate.AddHours(duration);
        }

        public bool WithinRange(DateTime value)
        {
            if(value >= StartDate && value <= EndDate)
            {
                return true;
            }else
            {
                return false;
            }
        }
        
        public bool WithinRange(DateRange range)
        {
            for (DateTime date = range.StartDate; date <= range.EndDate; date = date.AddDays(1))
            {
                if (WithinRange(date))
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return StartDate.ToString() + "," + EndDate.ToString();
        }
        public DateRange fromStringToDateRange(string value)
        {
            string[] result = value.Split(",");
            return new DateRange(DateTime.Parse(result[0]), DateTime.Parse(result[1]));
        }
    }
}
