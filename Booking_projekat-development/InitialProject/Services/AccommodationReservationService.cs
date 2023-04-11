 using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class AccommodationReservationService
    {
        private readonly ReservationService reservationService;
        private readonly AccommodationService accommodationService;
        public AccommodationReservationService()
        {
            reservationService = new ReservationService();
            accommodationService = new AccommodationService();
        }
        public Dictionary<int, string> GetReservationsByUserId(int userId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Reservation> usersReservations = reservationService.GetReservationsByUserId(userId);
            FilterReservations(usersReservations);
            if (usersReservations.Count > 0)
            {
                foreach (Reservation reservation in usersReservations)
                {
                    int accommodationId = accommodationService.GetAccommodationIdByReservationId(reservation.ReservationId);
                    Reservation founded = reservationService.GetReservationById(reservation.ReservationId);
                    string value = "";
                    string accommodationName = accommodationService.GetNameById(accommodationId);
                    value = value + " " + accommodationName + "; " + founded.ReservationDateRange.SStartDate + "-" + founded.ReservationDateRange.SEndDate;
                    result.Add(reservation.ReservationId, value);
                }
                return result;
            }
            return null;

        }
        public bool IsCancellingPossible(DateTime currentDate, int ReservationId)
        {
            Accommodation founded = accommodationService.GetAccommodationByReservationId(ReservationId);
            Reservation reservation = reservationService.GetReservationById(ReservationId);
            int daysBeforeCancel = founded.DaysBeforeCancelling;
            DateTime allowedCancellingDate = reservation.ReservationDateRange.EndDate.AddDays(daysBeforeCancel);
            return allowedCancellingDate > currentDate;
        }
        private void FilterReservations(List<Reservation> reservations)
        {
            reservations.RemoveAll(r => r.ReservationDateRange.StartDate <= DateTime.Now);
        }
    }
}
