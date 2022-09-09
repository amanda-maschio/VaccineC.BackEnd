﻿namespace VaccineC.Query.Application.ViewModels
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }
        public Guid SbimVaccinesId { get; set; }
        public string Situation { get; set; }
        public string? Details { get; set; }
        public decimal SaleValue { get; set; }
        public DateTime Register { get; set; }
        public string Name { get; set; }
    }
}
