using System;
using System.Collections.Generic;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Data.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly List<Partner> _partners;

        public PartnerRepository()
        {
            _partners = new List<Partner>();
        }

        public Partner GetPartnerById(Guid partnerId)
        {
            return _partners.Find(p => p.PartnerId == partnerId);
        }
    }
}

