namespace InitialProject.Services
{
    using InitialProject.CustomClasses;
    using InitialProject.Domen.Model;
    using InitialProject.Model;
    using InitialProject.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourReservationService
    {
        private readonly TourReservationRepository _repository;
        private readonly TourService _tourService;
        private readonly VoucherService _voucherService;
        public TourReservationService()
        {
            _repository = new TourReservationRepository();
            _tourService = new TourService();
            _voucherService = new VoucherService();
        }
        public List<TourReservation> GetReservationsByUserId(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId).ToList();
        }
        public void MakeReservationWithVoucher(int userId, int tourId, DateTime startingDateTime, int numberOfGuests, Voucher voucher)
        {
            TourReservation tourReservation = new TourReservation(userId, tourId, startingDateTime, numberOfGuests, voucher.Id);
            _repository.Save(tourReservation);
            _tourService.ReduceMaxGuestNumber(tourId, numberOfGuests);
            _voucherService.Delete(voucher);
        }

        public void MakeReservationWithoutVoucher(int userId, int tourId, DateTime startingDateTime, int numberOfGuests)
        {
            TourReservation tourReservation = new TourReservation(userId, tourId, startingDateTime, numberOfGuests, 0);
            _repository.Save(tourReservation);
            _tourService.ReduceMaxGuestNumber(tourId, numberOfGuests);
        }

        public List<Tour> GetAllReservedAndNotFinishedTour(int userId)
        {
            List<Tour> tours = new List<Tour>();
            foreach (TourReservation r in GetReservationsByUserId(userId))
            {
                foreach (Tour tour in _tourService.GetAllNotFinishedTour())
                {
                    if (r.TourId == tour.TourId)
                    {
                        tours.Add(tour);
                    }
                }
            }
            return tours;
        }
    }
}
