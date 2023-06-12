using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using TT.Deliveries.Data.Dto;
using TT.Deliveries.Data.Models;
using TT.Deliveries.Web.Api.Authentication;
using TT.Deliveries.BusinessLogic;

namespace TT.Deliveries.Web.Api.Controllers
{
    [Route("deliveries")]
    [ApiController]
    [Produces("application/json")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveriesController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet("{deliveryId}")]
        public IActionResult GetDelivery(Guid deliveryId)
        {
            var delivery = _deliveryService.GetDelivery(deliveryId);

            if (delivery == null)
                return NotFound();

            return Ok(delivery);
        }

        [HttpGet]
        public IActionResult GetAllDeliveries()
        {
            var deliveries = _deliveryService.GetAllDeliveries();
            return Ok(deliveries);
        }

        [HttpPost]
        public IActionResult CreateDelivery(CreateDeliveryDto createDto)
        {
            var delivery = _deliveryService.CreateDelivery(createDto);
            return CreatedAtAction(nameof(GetDelivery), new { deliveryId = delivery.Id }, delivery);
        }

        [HttpPut("{deliveryId}")]
        public IActionResult UpdateDelivery(Guid deliveryId, UpdateDeliveryDto updateDto)
        {
            var delivery = _deliveryService.UpdateDelivery(deliveryId, updateDto);

            if (delivery == null)
                return NotFound();

            return Ok(delivery);
        }

        [HttpDelete("{deliveryId}")]
        public IActionResult DeleteDelivery(Guid deliveryId)
        {
            var deleted = _deliveryService.DeleteDelivery(deliveryId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/approve")]
        public IActionResult ApproveDelivery(Guid id)
        {
            try
            {
                _deliveryService.ApproveDelivery(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/complete")]
        public IActionResult CompleteDelivery(Guid id)
        {
            try
            {
                _deliveryService.CompleteDelivery(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/cancel")]
        public IActionResult CancelDelivery(Guid id)
        {
            try
            {
                _deliveryService.CancelDelivery(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
