using System;
using System.Collections.Generic;

namespace Xero.Products.DataLayer.Entities
{
    public partial class ProductOption
    {
        public string Id { get; set; }
        public string? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
