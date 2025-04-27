using System;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent
    {
        public Guid SaleItemId { get; }
        public DateTime CancelledAt { get; }

        public ItemCancelledEvent(Guid saleItemId)
        {
            SaleItemId = saleItemId;
            CancelledAt = DateTime.UtcNow;
        }
    }
}
