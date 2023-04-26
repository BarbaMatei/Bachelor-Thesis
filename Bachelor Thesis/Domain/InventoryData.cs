using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Thesis.Domain
{
    public class InventoryData
    {
        public int ProductId { get; set; }
        public int QuantityToAdd { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return string.Format("ProductId: {0}. QuantityToAdd: {1}", ProductId, QuantityToAdd);
        }
    }
}
