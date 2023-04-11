namespace InitialProject.Domen.Model
{
    using InitialProject.CustomClasses;
    using InitialProject.Serializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourReservation : ISerializable
    {
        public int ReservationId { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }
        public int VoucherId { get; set; }

        public TourReservation()
        {
            UserId = -1;
            TourId = -1;
            ReservationDateRange = new DateRange();
            NumberOfGuests = 0;
            VoucherId = 0;
        }

        public TourReservation(int userId, int tourId, DateTime startDate, int numberOfVisitors, int voucherId)
        {
            UserId = userId;
            TourId = tourId;
            ReservationDateRange = new DateRange(startDate, numberOfVisitors);
            NumberOfGuests = numberOfVisitors;
            VoucherId = voucherId;
        }

        public TourReservation(DateRange dateRange, int guestNumber, int userId)
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
            NumberOfGuests = Convert.ToInt32(values[2]);
            ReservationDateRange = ReservationDateRange.fromStringToDateRange(values[3]);
            UserId = Convert.ToInt32(values[4]);
            VoucherId = Convert.ToInt32(values[5]);
        }
    }
}
