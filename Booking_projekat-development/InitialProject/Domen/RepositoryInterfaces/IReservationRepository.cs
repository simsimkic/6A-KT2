using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IReservationRepository
    {
        void Delete(Reservation reservation);
        List<Reservation> GetAll();
        Reservation GetByReservationId(int reservationId);
        int NextId();
        Reservation Save(Reservation reservation);
    }
}
