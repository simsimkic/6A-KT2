using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetAll();
        int NextId();
        Tour Save(Tour tour);
        Tour Update(Tour tour);
        Tour GetById(int id);
        void Delete(Tour tour);
    }
}