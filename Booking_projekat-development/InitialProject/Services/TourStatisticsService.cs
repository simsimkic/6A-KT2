using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Factory;
using InitialProject.Model;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class TourStatisticsService : ITourStatisticsService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IReservationRepository _reservationRepository;
        public TourStatisticsService()
        {
            _tourRepository = Injector.tourRepository();
            _reservationRepository = Injector.reservationRepository();
        }

        public List<string> GetAllYears()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<string> years = new();
            foreach (Tour tour in tours)
            {
                if (tour.TourStarted == true)
                {
                    string year = tour.StartingDateTime.Year.ToString();
                    if (!years.Contains(year))
                    {
                        years.Add(year);
                    }
                }
            }
            //check if same year already exist in years 
            return years;
        }

        public Tour GetMostVisitedTour(string year)
        {
            Dictionary<int, int> tourVisits = new Dictionary<int, int>();
            List<Reservation> reservations = _reservationRepository.GetAll().Where(c=> c.TourId > 0).ToList();

            foreach (Reservation reservation in reservations)
            {
                Tour tour = _tourRepository.GetById(reservation.TourId);
                if (tour.StartingDateTime.Year.ToString() == year)
                {
                    int reservationId = reservation.TourId;
                    int numberOfGuests = reservation.NumberOfGuests;

                    if (tourVisits.ContainsKey(reservationId))
                    {
                        tourVisits[reservationId] += numberOfGuests;
                    }
                    else
                    {
                        tourVisits.Add(reservationId, numberOfGuests);
                    }
                }
            }

            int tourId = tourVisits.FirstOrDefault(x => x.Value == tourVisits.Values.Max()).Key;
            return _tourRepository.GetById(tourId);
            
        }





    }
}

/*
        public int ReservationId { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        public float AvgRating { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }
 
 */