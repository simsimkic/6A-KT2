using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ICancelTourService
    {
        List<Tour> GetAll();
        List<Tour> GetAllTwoDaysFromNow();
        void CancelTour(string tourToCancel);
    }
}