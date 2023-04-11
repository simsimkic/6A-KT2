using InitialProject.Serializer;
using Microsoft.VisualStudio.Services.ClientNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.CustomClasses
{
    public enum TypeNotification { ReservationCancelled = 0 }
    public class Notification: ISerializable
    {
        private int _notificationId;
        private int _userId;
        private int _ownerId;
        private TypeNotification _notificationType;
        private int _reservationId;

        public int NotificationId { get => _notificationId; set => _notificationId = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int OwnerId { get => _ownerId; set => _ownerId = value; }
        public TypeNotification NotificationType { get => _notificationType; set => _notificationType = value; }
        public int ReservationId { get => _reservationId; set => _reservationId = value; }

        public Notification(int userId, int ownerId, TypeNotification notificationType, int reservationId)
        {
            UserId = userId;
            OwnerId = ownerId;
            NotificationType = notificationType;
            ReservationId = reservationId;
        }
        public Notification()
        {

        }

        public string[] ToCSV()
        {
            string[] cssValues =
            {
                NotificationId.ToString(),
                UserId.ToString(),
                OwnerId.ToString(),
                NotificationType.ToString(),
                ReservationId.ToString()
            };
            return cssValues;
        }
        public void FromCSV(string[] values)
        {
            NotificationId = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            OwnerId = Convert.ToInt32(values[2]);
            NotificationType = (TypeNotification)Enum.Parse(typeof(TypeNotification), values[3]);
            ReservationId = Convert.ToInt32(values[4]);
        }
    }
}
