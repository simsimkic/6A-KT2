using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        void CheckValidity();
        void Delete(Voucher voucher);
        List<Voucher> GetAll();
        int NextId();
        Voucher Save(Voucher voucher);
    }
}