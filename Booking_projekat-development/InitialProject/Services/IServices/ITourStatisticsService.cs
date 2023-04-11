using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourStatisticsService
    {
        List<string> GetAllYears();

        Tour GetMostVisitedTour(string year);
    }
}