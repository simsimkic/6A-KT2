using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.txt";

        private readonly Serializer<Reservation> _serializer;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public List<Reservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservation.ReservationId = NextId();
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }
        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(t => t.ReservationId) + 1;
        }
        public void Delete(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation foundedAccommodation = _reservations.Find(r => r.ReservationId == reservation.ReservationId);
            _reservations.Remove(foundedAccommodation);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public Reservation GetByReservationId(int reservationId)
        {
            throw new System.NotImplementedException();
        }
    }
}
