using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class UpdateSaleRequest
    {
        public List<UpdateSaleItemRequest> Items { get; set; }
    }

    public class UpdateSaleItemRequest
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
