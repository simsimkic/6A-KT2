using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        List<Notification> GetAll();
        Notification Save(Notification notification);
        int NextId();
        void Delete(Notification notification);
        Notification Update(Notification notification);
    }

}
