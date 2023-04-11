namespace InitialProject.Repository
{
    using InitialProject.Domen.Model;
    using InitialProject.Serializer;
    using System.Collections.Generic;
    using System.Linq;

    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreservations.txt";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _tourReservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourReservation Save(TourReservation reservation)
        {
            reservation.ReservationId = NextId();
            _tourReservations.Add(reservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            return reservation;
        }
        public int NextId()
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(t => t.ReservationId) + 1;
        }

        public TourReservation GetByReservationId(int reservationId)
        {
            throw new System.NotImplementedException();
        }
    }
}
