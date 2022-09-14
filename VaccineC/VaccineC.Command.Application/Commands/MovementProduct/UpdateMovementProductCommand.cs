﻿using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.MovementProduct
{
    public class UpdateMovementProductCommand : IRequest<IEnumerable<MovementProductViewModel>>
    {
        public Guid ID;
        public Guid MovementId;
        public Guid ProductId;
        public string? Batch;
        public decimal UnitsNumber;
        public decimal UnitaryValue;
        public decimal Amount;
        public string? Details;
        public DateTime Register;
        public DateTime? BatchManufacturingDate;
        public DateTime? BatchExpirationDate;
        public string? Manufacturer;

        public UpdateMovementProductCommand(Guid id, Guid movementId, Guid productId, string? batch, decimal unitsNumber, decimal unitaryValue, decimal amount, string? details, DateTime register, DateTime? batchManufacturingDate, DateTime? batchExpirationDate, string? manufacturer)
        {
            ID = id;
            MovementId = movementId;
            ProductId = productId;
            Batch = batch;
            UnitsNumber = unitsNumber;
            UnitaryValue = unitaryValue;
            Amount = amount;
            Details = details;
            Register = register;
            BatchManufacturingDate = batchManufacturingDate;
            BatchExpirationDate = batchExpirationDate;
            Manufacturer = manufacturer;
        }
    }
}