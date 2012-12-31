using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WorkOrders.Domain.Models;

namespace WorkOrders.Web.Models
{
    public class WorkOrdersContext : DbContext
    {
        protected class Migrator : DbMigrationsConfiguration<WorkOrdersContext>
        {
            public Migrator()
            {
                this.AutomaticMigrationDataLossAllowed = true;
                this.AutomaticMigrationsEnabled = true;
            }
        }
        static WorkOrdersContext()
        {
            Database.SetInitializer<WorkOrdersContext>(new MigrateDatabaseToLatestVersion<WorkOrdersContext, Migrator>());
        }

        public WorkOrdersContext()
            : base("name=WorkOrders")
        {

        }

        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}