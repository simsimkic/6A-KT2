using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class OwnerToRateService
    {
        private readonly IOwnerToRateRepository _ownerToRateRepository;
        private readonly ReservationService _reservationService;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private List<OwnerToRate> _ownersToRate;

        public OwnerToRateService(int userId)
        {
            _ownerToRateRepository = new OwnerToRateRepository();
            _reservationService = new ReservationService();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationRepository = new AccommodationRepository();
            UpdateOwnersToRateList(userId);
            DeleteIfFiveDaysPassed();
            _ownersToRate = _ownerToRateRepository.GetAll();
        }
        public List<OwnerToRate> GetOwnersToRate()
        {
            return _ownerToRateRepository.GetAll();
        }
        public void UpdateOwnersToRateList(int userId)
        {
            List<Reservation> userReservations = _reservationService.GetReservationsByUserId(userId);
            foreach (Reservation r in userReservations)
            {
                if (CheckIfLeftReservation(r))
                {
                    int accommodationId = GetAccommodationIdByReservationId(r.ReservationId);
                    OwnerToRate ownerToRate = new OwnerToRate(GetOwnerIdByAccommodationId(accommodationId), accommodationId, r.UserId, r.ReservationDateRange.EndDate);
                    _ownerToRateRepository.Save(ownerToRate);
                    _reservationService.Delete(r);
                    _accommodationReservationRepository.DeleteReservation(r.ReservationId);
                }
            }
        }
        private void DeleteIfFiveDaysPassed()
        {
            DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
            List<OwnerToRate> tempList = _ownerToRateRepository.GetAll().Where(o => o.LeavingDay < fiveDaysAgo).ToList();
            foreach(OwnerToRate o in tempList)
            {
                _ownerToRateRepository.Delete(o);
            }
        }
        private bool CheckIfLeftReservation(Reservation reservation)
        {
            return reservation.ReservationDateRange.EndDate < DateTime.Now;
        }
        //PITAJ IMA LI POTREBE DA SE OVO STAVI U REPOZITORIUJM
        private int GetAccommodationIdByReservationId(int reservationId)
        {
            return (_accommodationReservationRepository.GetAll().Find(a => a.ReservationId == reservationId)).AccommodationId;
        }
        public int GetOwnerIdByAccommodationId(int accommodationId)
        {
            return (_accommodationRepository.GetAll().Find(a => a.AccommodationID == accommodationId)).UserId;
        }
        public Dictionary<int, string> GetAccommodationNamesByUser(int userId)
        {
            Dictionary<int, string> accommodationNames = new Dictionary<int, string>();
            foreach (OwnerToRate o in (_ownersToRate))
            {
                if (o.UserId == userId)
                {
                    Accommodation accommodation = _accommodationRepository.GetAll().Find(a => a.AccommodationID == o.AccommodationId);
                    accommodationNames.Add(accommodation.AccommodationID, accommodation.Name);
                }
            }
            return accommodationNames;
        }
        public void DeleteOwnerToRate(int accommodationId)
        {
            OwnerToRate founded = _ownersToRate.Find(or => or.AccommodationId == accommodationId);
            _ownerToRateRepository.Delete(founded);
        }
    }
}
