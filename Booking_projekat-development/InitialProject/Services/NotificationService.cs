using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRespository;

        public NotificationService()
        {
            _notificationRespository = new NotificationRespository();
        }

        public Notification SaveNotification(Notification notificiation)
        {
            return _notificationRespository.Save(notificiation);
        }
    }
}
