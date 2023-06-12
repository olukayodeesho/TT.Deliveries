using System;
using TT.Deliveries.Data.Dto;
using TT.Deliveries.Data.Models;
using TT.Deliveries.Data.Repositories;
using TT.Deliveries.Web.Api.Logging;

namespace TT.Deliveries.BusinessLogic
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly ILoggerAdapter<DeliveryService> logger;

        public DeliveryService(IDeliveryRepository deliveryRepository, IPartnerRepository partnerRepository, ILoggerAdapter<DeliveryService> logger)
        {
            _deliveryRepository = deliveryRepository;
            _partnerRepository = partnerRepository;
            this.logger = logger;
        }

        public DeliveryDto GetDelivery(Guid deliveryId)
        {
            logger.LogInformation($"Getting delivery with ID: {deliveryId}");
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);

            if (delivery == null)
                return null;

            // Map the Delivery entity to DeliveryDto and return it
            return MapDeliveryToDto(delivery);
        }

        public IEnumerable<DeliveryDto> GetAllDeliveries()
        {
            var deliveries = _deliveryRepository.GetAllDeliveries();

            // Map each Delivery entity to DeliveryDto and return as a collection
            var deliveryDtos = new List<DeliveryDto>();
            foreach (var delivery in deliveries)
            {
                deliveryDtos.Add(MapDeliveryToDto(delivery));
            }

            return deliveryDtos;
        }

        public DeliveryDto CreateDelivery(CreateDeliveryDto createDto)
        {
            // Create a new Delivery entity from the CreateDeliveryDto
            var delivery = new Delivery
            {
                State = createDto.State,
                AccessWindow = new AccessWindow(createDto.AccessWindow.StartTime, createDto.AccessWindow.EndTime),
                Recipient = new Recipient(createDto.Recipient.Name, createDto.Recipient.Address, createDto.Recipient.Email, createDto.Recipient.PhoneNumber),
                Order = new Order(createDto.Order.OrderNumber, createDto.Order.Sender)
            };

            // Save the new Delivery entity using the repository
            _deliveryRepository.SaveDelivery(delivery);

            // Map the saved Delivery entity to DeliveryDto and return it
            return MapDeliveryToDto(delivery);
        }

        public DeliveryDto UpdateDelivery(Guid deliveryId, UpdateDeliveryDto updateDto)
        {
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);

            if (delivery == null)
                return null;

            // Update the Delivery entity with the new state
            delivery.State = updateDto.State;

            // Save the updated Delivery entity using the repository
            _deliveryRepository.SaveDelivery(delivery);

            // Map the updated Delivery entity to DeliveryDto and return it
            return MapDeliveryToDto(delivery);
        }

        public bool DeleteDelivery(Guid deliveryId)
        {
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);

            if (delivery == null)
                return false;

            // Delete the Delivery entity using the repository
            _deliveryRepository.DeleteDelivery(delivery);

            return true;
        }

        public void ApproveDelivery(Guid deliveryId)
        {
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);
            if (delivery == null)
                throw new Exception("Delivery not found");

            if (delivery.State != DeliveryState.Created)
                throw new Exception("Delivery cannot be approved");

            delivery.State = DeliveryState.Approved;
            _deliveryRepository.SaveDelivery(delivery);
        }

        public void CompleteDelivery(Guid deliveryId)
        {
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);
            if (delivery == null)
                throw new Exception("Delivery not found");

            if (delivery.State != DeliveryState.Approved)
                throw new Exception("Delivery cannot be completed");

            delivery.State = DeliveryState.Completed;
            _deliveryRepository.SaveDelivery(delivery);
        }

        public void CancelDelivery(Guid deliveryId)
        {
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);
            if (delivery == null)
                throw new Exception("Delivery not found");

            if (delivery.State != DeliveryState.Created && delivery.State != DeliveryState.Approved)
                throw new Exception("Delivery cannot be cancelled");

            delivery.State = DeliveryState.Cancelled;
            _deliveryRepository.SaveDelivery(delivery);
        }

        // Helper method to map Delivery entity to DeliveryDto
        private DeliveryDto MapDeliveryToDto(Delivery delivery)
        {
            return new DeliveryDto
            {
                Id = delivery.Id,
                State = delivery.State,
                AccessWindow = new AccessWindowDto
                {
                    StartTime = delivery.AccessWindow.StartTime,
                    EndTime = delivery.AccessWindow.EndTime
                },
                Recipient = new RecipientDto
                {
                    Name = delivery.Recipient.Name,
                    Address = delivery.Recipient.Address,
                    Email = delivery.Recipient.Email,
                    PhoneNumber = delivery.Recipient.PhoneNumber
                },
                Order = new OrderDto
                {
                    OrderNumber = delivery.Order.OrderNumber,
                    Sender = delivery.Order.Sender
                }
            };
        }

        void IDeliveryService.GetDeliveryById(Guid nonExistentDeliveryId)
        {
            throw new NotImplementedException();
        }
    }
}
