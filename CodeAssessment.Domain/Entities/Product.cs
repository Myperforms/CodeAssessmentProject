using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssessment.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescrption { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string? SearchKeyWord { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
