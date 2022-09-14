﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class MovementProductViewModel
    {
        public Guid ID { get; set; }
        public Guid MovementId { get; set; }
        public Guid ProductId { get; set; }
        public string? Batch { get; set; }
        public decimal UnitsNumber { get; set; }
        public decimal UnitaryValue { get; set; }
        public decimal Amount { get; set; }
        public string? Details { get; set; }
        public DateTime Register { get; set; }
        public DateTime? BatchManufacturingDate { get; set; }
        public DateTime? BatchExpirationDate { get; set; }
        public string? Manufacturer { get; set; }
       public ProductViewModel? Product { get; set; }
    }
}