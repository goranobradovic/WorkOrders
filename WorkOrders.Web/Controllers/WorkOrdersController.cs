using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using WorkOrders.Domain.Models;
using WorkOrders.Web.Models;

namespace WorkOrders.Web.Controllers
{
    [BreezeController]
    public class WorkOrdersController : ApiController
    {
        readonly EFContextProvider<WorkOrdersContext> _contextProvider =
            new EFContextProvider<WorkOrdersContext>();
        
        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        [HttpGet]
        public IQueryable<WorkOrder> WorkOrders()
        {
            return _contextProvider.Context.WorkOrders;
        }

        
        [HttpPost]
        public WorkOrder CreateWorkOrder(long? vehicleId = null)
        {
            var wo = new WorkOrder(vehicleId);
            _contextProvider.Context.WorkOrders.Add(wo);
            _contextProvider.Context.SaveChanges();
            return wo;
        }

        //public IQueryable<WorkOrder> GetWorkOrders()
        //{
        //    return db.WorkOrders
        //             .Include(wo => wo.Client)
        //             .Include(wo => wo.Vehicle)
        //             .OrderByDescending(wo => wo.DateReceived)
        //             .Where(wo => wo.CompletionDate == null);
        //}
    }

}
