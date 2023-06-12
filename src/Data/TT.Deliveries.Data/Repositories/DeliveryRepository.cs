using System;
using System.Collections.Generic;
using TT.Deliveries.Data.Models;
using System.Linq;


namespace TT.Deliveries.Data.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly List<Delivery> _deliveries;

        public DeliveryRepository()
        {
            _deliveries = new List<Delivery>();
        }

        public Delivery GetDeliveryById(Guid deliveryId)
        {
            return _deliveries.FirstOrDefault(d => d.Id == deliveryId);
        }

        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return _deliveries;
        }

        public void SaveDelivery(Delivery delivery)
        {
            // Check if the delivery already exists
            var existingDelivery = GetDeliveryById(delivery.Id);
            if (existingDelivery != null)
            {
                // Update the existing delivery
                existingDelivery.State = delivery.State;
                existingDelivery.AccessWindow = delivery.AccessWindow;
                existingDelivery.Recipient = delivery.Recipient;
                existingDelivery.Order = delivery.Order;
            }
            else
            {
                // Add the new delivery
                _deliveries.Add(delivery);
            }
        }

        public void DeleteDelivery(Delivery delivery)
        {
            _deliveries.Remove(delivery);
        }
    }
}

