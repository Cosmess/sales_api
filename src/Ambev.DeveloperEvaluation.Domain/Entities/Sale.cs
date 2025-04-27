using Ambev.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string Customer { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Branch { get; private set; }
        public SaleStatus Status { get; private set; }

        public List<SaleItem> Items { get; private set; }

        protected Sale() { } // Para EF Core

        public Sale(string saleNumber, DateTime saleDate, string customer, string branch)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            Customer = customer;
            Branch = branch;
            Items = new List<SaleItem>();
            Status = SaleStatus.Pending;
        }

        public void AddItem(string productName, int quantity, decimal unitPrice)
        {
            if (quantity < 1)
                throw new ArgumentException("Quantity must be at least 1.");

            if (quantity > 20)
                throw new ArgumentException("Cannot sell more than 20 identical items.");

            decimal discount = 0m;
            if (quantity >= 4 && quantity < 10)
                discount = 0.10m;
            else if (quantity >= 10 && quantity <= 20)
                discount = 0.20m;

            var item = new SaleItem(productName, quantity, unitPrice, discount);
            Items.Add(item);

            RecalculateTotal();
        }

        public void Cancel()
        {
            Status = SaleStatus.Cancelled;
        }

        private void RecalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
                TotalAmount += item.Total;
        }
    }
}
