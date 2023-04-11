using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        List<Accommodation> GetAll();
        Accommodation Save(Accommodation accommodation);
        List<Location> GetAllLocationsFromAccommodations();
        int NextId();
        int GetLastAccommodationId();
        void Delete(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
    }
}
