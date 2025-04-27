using Ambev.DeveloperEvaluation.Domain.Common;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal Total { get; private set; }

        protected SaleItem() { }

        public SaleItem(string productName, int quantity, decimal unitPrice, decimal discountPercentage)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountPercentage = discountPercentage;

            Total = CalculateTotal();
        }

        private decimal CalculateTotal()
        {
            var totalWithoutDiscount = UnitPrice * Quantity;
            var discountAmount = totalWithoutDiscount * DiscountPercentage;
            return totalWithoutDiscount - discountAmount;
        }
    }
}
