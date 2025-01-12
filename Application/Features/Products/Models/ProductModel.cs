using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssessment.Application.Features.Products.Models
{
    public class ProductModel : ProductDetails
    {
        public int ProductId { get; set; }       
    }

    public class ProductDetails
    {
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescrption { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string? SearchKeyWord { get; set; }
    }

}
