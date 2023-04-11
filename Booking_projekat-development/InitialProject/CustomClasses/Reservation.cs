using InitialProject.Model;
using System;
using System.Collections.Generic;
using InitialProject.Serializer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.CustomClasses
{
    public class Reservation : ISerializable
    {
        public int ReservationId { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        public float AvgRating { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }
        public int VoucherId { get; set; }

        public Reservation() 
        {
            UserId = -1;
            TourId = -1;
            AvgRating = 0;
            ReservationDateRange = new DateRange();
            NumberOfGuests = 0;
            VoucherId = 0;
        }

        public Reservation(int userId, int tourId, DateTime startDate, int numberOfVisitors, int voucherId)
        {
            UserId = userId;
            TourId = tourId;
            ReservationDateRange = new DateRange(startDate, numberOfVisitors);  
            NumberOfGuests = numberOfVisitors;
            AvgRating = 0;
            VoucherId = voucherId;
        }

        public Reservation(DateRange dateRange, int guestNumber, int userId)
        {
            TourId = -1;
            UserId = userId;
            ReservationDateRange = dateRange;
            NumberOfGuests = guestNumber;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                ReservationId.ToString(),
                TourId.ToString(),
                AvgRating.ToString(),
                NumberOfGuests.ToString(),
                ReservationDateRange.ToString(),
                UserId.ToString(),
                VoucherId.ToString()
            };
                
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);    
            TourId = Convert.ToInt32(values[1]);
            AvgRating = float.Parse(values[2]);
            NumberOfGuests = Convert.ToInt32(values[3]);
            ReservationDateRange = ReservationDateRange.fromStringToDateRange(values[4]);
            UserId = Convert.ToInt32(values[5]);
            VoucherId = Convert.ToInt32(values[6]);
        }

    }
}
