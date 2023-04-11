using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Factory;
using InitialProject.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class CancelTourService : ICancelTourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IVoucherRepository _voucherRepository;
        public CancelTourService()
        {
            _tourRepository = Injector.tourRepository();
            _reservationRepository = Injector.reservationRepository();
            _voucherRepository = Injector.voucherRepository();
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public List<Tour> GetAllTwoDaysFromNow()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<Tour> toursToCancel = new();
            foreach (Tour tour in tours)
            {
                if (tour.StartingDateTime < DateTime.Now.AddDays(2).Date && tour.TourStarted == false)
                {
                    toursToCancel.Add(tour);
                }
            }
            return toursToCancel;
        }
        
        //Cancel tour and give Voucher to user
        public void CancelTour(string tourToCancel)
        {
            int tourId = int.Parse(tourToCancel.Split(' ')[0]);
            _tourRepository.Delete(_tourRepository.GetById(tourId));
            List<Reservation> reservations = _reservationRepository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.TourId == tourId)
                {
                    _reservationRepository.Delete(reservation);
                }

                CreateVoucher(reservation.UserId);
            }
            
        }

        private void CreateVoucher(int userID)
        {
            Voucher voucher = new();
            voucher.GuideId = 1;//hardcoded because of no login form
            voucher.UserId = userID;
            voucher.Received = DateTime.Now;
            voucher.Name = "Vaucer";
            voucher.ValidUntil = DateTime.Now.AddYears(1);
            _voucherRepository.Save(voucher);
        }
    }
}
