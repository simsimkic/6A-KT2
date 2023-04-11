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
    public class ChangeReservationRequestRepository : IChangeReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/requests.txt";

        private readonly Serializer<ChangeReservationRequest> _serializer;

        private List<ChangeReservationRequest> _requests;

        public ChangeReservationRequestRepository()
        {
            _serializer = new Serializer<ChangeReservationRequest>();
            _requests = new List<ChangeReservationRequest>();
        }
        public List<ChangeReservationRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public ChangeReservationRequest Save(ChangeReservationRequest request)
        {
            request.RequestId = NextId();
            _requests.Add(request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }
        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(r => r.RequestId) + 1;
        }
        public void Delete(ChangeReservationRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            ChangeReservationRequest foundedRequest = _requests.Find(r => r.RequestId == request.RequestId);
            _requests.Remove(foundedRequest);
            _serializer.ToCSV(FilePath, _requests);
        }
        public ChangeReservationRequest Update(ChangeReservationRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            ChangeReservationRequest founded = _requests.Find(r => r.ReservationId == request.ReservationId && r.UserId == request.UserId);
            int index = _requests.IndexOf(founded);
            _requests.Remove(founded);
            _requests.Insert(index, request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }
    }
}
