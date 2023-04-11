using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class OwnerRateService
    {
        private readonly IOwnerRateRepository _ownerRateRepository;

        public OwnerRateService()
        {
            _ownerRateRepository = new OwnerRateRepository();
        }
        public void SaveRate(OwnerRate ownerRate)
        {
            _ownerRateRepository.Save(ownerRate);
        }
    }
}
