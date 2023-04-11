using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IChangeReservationRepository
    {
        List<ChangeReservationRequest> GetAll();
        ChangeReservationRequest Save(ChangeReservationRequest request);
        void Delete(ChangeReservationRequest request);
        ChangeReservationRequest Update(ChangeReservationRequest request);
    }
}
