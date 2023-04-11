using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ChangeReservationRequestService
    {
        private readonly ChangeReservationRequestRepository _requestRepository;
        private readonly ReservationService _reservationService;
        private List<ChangeReservationRequest> _changes;
        public ChangeReservationRequestService()
        {
            _requestRepository = new ChangeReservationRequestRepository();
            _reservationService = new ReservationService();
        }

        public List<ChangeReservationRequest> GetRequests(int userId)
        {
            return _requestRepository.GetAll().Where(r => r.UserId == userId).ToList();
        }

        public void SaveRequest(ChangeReservationRequest request)
        {
            if(_requestRepository.GetAll().Find(r => r.ReservationId == request.ReservationId && r.UserId == request.UserId) != null)
            {
                _requestRepository.Update(request);
            }
            else
            {
                _requestRepository.Save(request);
            }
        }
    }
}
