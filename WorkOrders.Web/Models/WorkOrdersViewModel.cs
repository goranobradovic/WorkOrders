using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkOrders.Domain.Models;

namespace WorkOrders.Web.Models
{
    public class WorkOrdersViewModel
    {
        public ICollection<WorkOrder> WorkOrdersList { get; set; }

        public WorkOrder SelectedWokOrder { get; set; }
    }
}