using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace InitialProject.Model
{
    public enum StatusType { Pending, Canceled, Approved}
    public class ChangeReservationRequest: ISerializable
    {
        private int _requestId;
        private int _reservationId;
        private string _accommodationName;
        private DateTime _newStartDate;
        private DateTime _newEndDate;
        private StatusType _requestStatus;
        private int _userId;
        private int _ownerId;
        private string _ownerComment;

        public ChangeReservationRequest(int reservationId, string accommodationName, DateTime newStartDate, DateTime newEndDate, StatusType requestStatus, int userId, int ownerId)
        {
            this.ReservationId = reservationId;
            this.AccommodationName = accommodationName;
            this.NewStartDate = newStartDate;
            this.NewEndDate = newEndDate;
            this.RequestStatus = requestStatus;
            this.UserId = userId;
            this.OwnerId = ownerId;
            this.OwnerComment = "-";
        }
        public ChangeReservationRequest()
        {
        }
        public int ReservationId { get => _reservationId; set => _reservationId = value; }
        public DateTime NewStartDate { get => _newStartDate; set => _newStartDate = value; }
        public DateTime NewEndDate { get => _newEndDate; set => _newEndDate = value; }
        public StatusType RequestStatus { get => _requestStatus; set => _requestStatus = value; }
        public int RequestId { get => _requestId; set => _requestId = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int OwnerId { get => _ownerId; set => _ownerId = value; }
        public string OwnerComment { get => _ownerComment; set => _ownerComment = value; }
        public string AccommodationName { get => _accommodationName; set => _accommodationName = value; }

        public string[] ToCSV()
        {
            string[] csvValues = {
                _requestId.ToString(),   
                _reservationId.ToString(),
                _accommodationName.ToString(),
                _newStartDate.ToString(),
                _newEndDate.ToString(),
                _requestStatus.ToString(),
                _userId.ToString(),
                _ownerId.ToString(),
                _ownerComment
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            _requestId = Convert.ToInt32(values[0]);
            _reservationId = Convert.ToInt32(values[1]);
            _accommodationName = Convert.ToString(values[2]);
            _newStartDate = Convert.ToDateTime(values[3]);
            _newEndDate = Convert.ToDateTime(values[4]);
            _requestStatus = (StatusType)Enum.Parse(typeof(StatusType), values[5]);
            _userId = Convert.ToInt32(values[6]);
            _ownerId = Convert.ToInt32(values[7]);
            _ownerComment = Convert.ToString(values[8]);
        }
    }
}
