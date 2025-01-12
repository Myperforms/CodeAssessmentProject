using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Common.Enums;

namespace CodeAssessment.Application.Features.Products.Models
{
    public class ProductStockUpdateModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public ProductStockUpdateType Type { get; set; }
    }
}
