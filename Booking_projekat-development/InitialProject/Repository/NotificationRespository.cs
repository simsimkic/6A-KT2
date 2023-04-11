using InitialProject.CustomClasses;
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
    public class NotificationRespository : INotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/notifications.txt";

        private readonly Serializer<Notification> _serializer;

        private List<Notification> _notifications;

        public NotificationRespository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = new List<Notification>();
        }
        public List<Notification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Notification Save(Notification notification)
        {
            notification.NotificationId = NextId();
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }
        public int NextId()
        {
            _notifications = _serializer.FromCSV(FilePath);
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(n => n.NotificationId) + 1;
        }
        public void Delete(Notification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Notification foundedNotification = _notifications.Find(n => n.NotificationId == notification.NotificationId);
            _notifications.Remove(foundedNotification);
            _serializer.ToCSV(FilePath, _notifications);
        }
        public Notification Update(Notification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Notification current = _notifications.Find(n => n.NotificationId == notification.NotificationId);
            int index = _notifications.IndexOf(current);
            _notifications.Remove(current);
            _notifications.Insert(index, notification);
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }
    }
}
