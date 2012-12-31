using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkOrders.Domain.Models;
using WorkOrders.Web.Models;

namespace WorkOrders.Web.Controllers
{
    public class WorkOrdersController : ApiController
    {
        private WorkOrdersContext db = new WorkOrdersContext();

        public IQueryable<WorkOrder> GetWorkOrders()
        {
            return db.WorkOrders
                     .Include(wo => wo.Client)
                     .Include(wo => wo.Vehicle)
                     .OrderByDescending(wo => wo.DateReceived)
                     .Where(wo => wo.CompletionDate == null);
        }
    }
}
