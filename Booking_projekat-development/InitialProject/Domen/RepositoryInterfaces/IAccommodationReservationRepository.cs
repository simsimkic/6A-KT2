using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        List<AccommodationReservation> GetAll();
        AccommodationReservation Save(AccommodationReservation accommodationReservation);
        void DeleteReservation(int reservationId);
        void DeleteAccommodation(int accommodationId);
        AccommodationReservation Update(AccommodationReservation accommodationReservation);
    }
}
