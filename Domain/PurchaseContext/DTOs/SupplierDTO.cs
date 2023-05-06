﻿namespace Domain.PurchaseContext.DTOs
{
    public class SupplierDTO
    {
        public string TaxIdentifier { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string IdentityCard { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public List<CompanyDTO> Companies { get; } = new();
    }
}
