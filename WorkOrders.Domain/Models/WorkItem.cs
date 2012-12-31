using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrders.Domain.Models.Common;

namespace WorkOrders.Domain.Models
{
    public class WorkItem : BaseModel
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Unit { get; set; }

        public decimal Value { get; set; }
    }
}
