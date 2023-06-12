using System;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Data.Repositories
{
	public interface IPartnerRepository
	{
        Partner GetPartnerById(Guid partnerId);
    }
}

