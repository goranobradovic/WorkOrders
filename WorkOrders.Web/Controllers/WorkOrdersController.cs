﻿using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using WebMatrix.WebData;
using WorkOrders.Domain.Models;
using WorkOrders.Web.Filters;
using WorkOrders.Web.Infrastructure;
using WorkOrders.Web.Models;

namespace WorkOrders.Web.Controllers
{
    [BreezeController, Authorize(Roles = Constants.RoleNames.Employee)]
    public class WorkOrdersController : ApiController
    {
        readonly EFContextProvider<WorkOrdersContext> _contextProvider =
            new EFContextProvider<WorkOrdersContext>();

        public WorkOrdersController()
        {
            // todo change this
            //WebSecurity.RequireRoles(Constants.RoleNames.Employee);
        }

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
    }

}
