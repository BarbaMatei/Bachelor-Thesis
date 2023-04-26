using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Thesis.Domain
{
    public class SaleData
    {
        public int ProductId { get; set; }
        public int QuantitySold { get; set; }
        public decimal Revenue { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
