using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;

namespace InitialProject.Factory
{
    public static class Injector
    {
        public static ICancelTourService cancelTourService() 
        {
            return new CancelTourService();
        }

        public static ITourRepository tourRepository()
        {
            return new TourRepository();
        }

        public static IReservationRepository reservationRepository()
        {
            return new ReservationRepository();
        }

        public static IVoucherRepository voucherRepository()
        {
            return new VoucherRepository();
        }

        public static ITourStatisticsService tourStatisticsService()
        {
            return new TourStatisticsService();
        }
    }
}
