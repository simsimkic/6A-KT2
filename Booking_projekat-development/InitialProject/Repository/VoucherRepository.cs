namespace InitialProject.Repository
{
    using InitialProject.Domen.RepositoryInterfaces;
    using InitialProject.Model;
    using InitialProject.Serializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.txt";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;


        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
            CheckValidity();
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public void Delete(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher founded = _vouchers.Find(v => v.Id == voucher.Id);
            _vouchers.Remove(founded);
            _serializer.ToCSV(FilePath, _vouchers);
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(t => t.Id) + 1;
        }

        public void CheckValidity()
        {
            List<Voucher> vouchers = _serializer.FromCSV(FilePath);
            foreach (Voucher voucher in vouchers)
            {
                if (DateTime.Compare(voucher.ValidUntil, DateTime.Now) < 0)
                {
                    Delete(voucher);
                }

            }
            //_serializer.ToCSV(FilePath, _vouchers);
        }
    }
}
