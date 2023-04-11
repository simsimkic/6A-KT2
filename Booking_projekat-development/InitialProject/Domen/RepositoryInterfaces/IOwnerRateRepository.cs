using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IOwnerRateRepository
    {
        List<OwnerRate> GetAll();
        OwnerRate Save(OwnerRate ownerRate);
        List<OwnerRate> GetAllRatesByOwner(int ownerId);
        int NextId();
        void Delete(OwnerRate ownerRate);
    }
}
