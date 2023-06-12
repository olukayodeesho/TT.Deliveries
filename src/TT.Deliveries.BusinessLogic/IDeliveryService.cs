
using System;
using System.Collections.Generic;
using TT.Deliveries.Data.Dto;
using TT.Deliveries.Data;

namespace TT.Deliveries.BusinessLogic
{
    public interface IDeliveryService
    {
        DeliveryDto GetDelivery(Guid deliveryId);
        IEnumerable<DeliveryDto> GetAllDeliveries();
        DeliveryDto CreateDelivery(CreateDeliveryDto createDto);
        DeliveryDto UpdateDelivery(Guid deliveryId, UpdateDeliveryDto updateDto);
        bool DeleteDelivery(Guid deliveryId);
        void ApproveDelivery(Guid deliveryId);
        void CompleteDelivery(Guid deliveryId);
        void CancelDelivery(Guid deliveryId);
        void GetDeliveryById(Guid nonExistentDeliveryId);
    }
}

