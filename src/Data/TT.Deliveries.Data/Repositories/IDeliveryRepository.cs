using System;
using System.Collections.Generic;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Data.Repositories
{
	public interface IDeliveryRepository
	{
        Delivery GetDeliveryById(Guid deliveryId);
        IEnumerable<Delivery> GetAllDeliveries();
        void SaveDelivery(Delivery delivery);
        void DeleteDelivery(Delivery delivery);
    }
}

