namespace InitialProject.Services
{
    using InitialProject.CustomClasses;
    using InitialProject.Model;
    using InitialProject.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TourService
    {
        private readonly TourRepository _tourRepository;
        private readonly TourAttendanceRepository _tourAttendanceRepository;
        private TourPointService _tourPointService;
        private List<Tour> _tours;

        public TourService()
        {
            _tourRepository = new TourRepository();
            _tourAttendanceRepository = new TourAttendanceRepository();
            _tourPointService = new TourPointService();
            _tours = _tourRepository.GetAll();
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public List<Tour> GetAllNotStartedTours()
        {
            return _tours.Where(t => t.TourStarted == false).ToList();
        }

        public Tour GetTourById(int id)
        {
            return _tours.Where(t => t.TourId == id).FirstOrDefault();
        }

        public List<Tour> GetSimilarAsTourHasFullCapacity(string country, string city)
        {
            return _tours.Where(a => (a.Location.Country == country) && (a.Location.City == city) && (a.MaxGuestNumber != 0)).ToList();
        }

        public void ReduceMaxGuestNumber(int tourId, int guestNumber)
        {
            Tour tour = _tours.Find(t => t.TourId == tourId);
            tour.MaxGuestNumber -= guestNumber;
            _tourRepository.Update(tour);
            _tours = _tourRepository.GetAll();
        }

        public List<Tour> GetAllFinished(int userId)
        {
            List<TourAttendance> _tourAttendance = _tourAttendanceRepository.GetAllPresented(userId);
            List<Tour> tours = new List<Tour>();
            foreach (TourAttendance t in _tourAttendance)
            {
                if (!tours.Contains(_tours.Find(tour => tour.TourId == t.TourId)))
                    tours.Add(_tours.Find(tour => tour.TourId == t.TourId));
            }
            return tours;
        }

        public List<Tour> GetAllNotFinishedTour()
        {
            List<Tour> tours = new List<Tour>();
            tours = _tours.Where(t => t.TourStarted == false).ToList();
            foreach (Tour tour in _tours.Where(t => t.TourStarted == true).ToList())
                if (!_tourPointService.TourStartedAndFinished(tour.TourId))
                {
                    tours.Add(tour);
                }
            return tours;
        }

        public List<Tour> GetAllFiltered(string city, string country, string durationFrom, string durationTo, string language, string guestNumber)
        {
            List<Tour> toursFiltered = new List<Tour>();
            foreach (Tour tour in GetAllNotStartedTours())
            {
                if (CityFilter(tour, city) && CountryFilter(tour, country) && DurationFilter(tour, durationFrom, durationTo) && LanguageFilter(tour, language) && GuestNumberFilter(tour, guestNumber))
                {
                    toursFiltered.Add(tour);
                }
            }
            return toursFiltered;
        }

        bool CountryFilter(Tour tour, string country)
        {
            return (string.IsNullOrEmpty(country) || tour.Location.Country == country);
        }

        bool CityFilter(Tour tour, string city)
        {
            return (string.IsNullOrEmpty(city) || tour.Location.City == city);
        }

        bool GuestNumberFilter(Tour tour, string guestNumber)
        {
            return (string.IsNullOrEmpty(guestNumber) || tour.MaxGuestNumber >= Convert.ToInt32(guestNumber));
        }

        bool DurationFilter(Tour tour, string durationFrom, string durationTo)
        {
            return (string.IsNullOrEmpty(durationFrom) || tour.Duration >= Convert.ToInt32(durationFrom)) &&
                    (string.IsNullOrEmpty(durationTo) || tour.Duration <= Convert.ToInt32(durationTo));
        }

        bool LanguageFilter(Tour tour, string language)
        {
            return (string.IsNullOrEmpty(language) || tour.Language.ToString() == language);
        }

    }
}
