using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationreservations.txt";

        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = new List<AccommodationReservation>();
        }
        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            _accommodationReservations.Add(accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }
        public void DeleteReservation(int reservationId)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation foundedReservation = _accommodationReservations.Find(acmRes => acmRes.ReservationId == reservationId);
            _accommodationReservations.Remove(foundedReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
        }
        public void DeleteAccommodation(int accommodationId)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation foundedAccommodation = _accommodationReservations.Find(acmRes => acmRes.ReservationId == accommodationId);
            _accommodationReservations.Remove(foundedAccommodation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
        }
        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation current = _accommodationReservations.Find(acmRes => acmRes.ReservationId == accommodationReservation.ReservationId);
            int index = _accommodationReservations.IndexOf(current);
            _accommodationReservations.Remove(current);
            _accommodationReservations.Insert(index, accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }
    }
}
