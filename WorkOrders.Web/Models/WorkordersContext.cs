using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using WorkOrders.Domain.Models;
using WorkOrders.Domain.Models.Common;

namespace WorkOrders.Web.Models
{
  public class WorkOrdersContext : DbContext
  {
    static WorkOrdersContext()
    {
      Database.SetInitializer(new MigrateDatabaseToLatestVersion<WorkOrdersContext, Migrator>());
    }

    public WorkOrdersContext()
      : base("name=WorkOrders")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      DbConfiguration.RegisterAll(modelBuilder);
    }

    public override int SaveChanges()
    {
      var now = DateTime.Now;
      foreach (var vehicle in ChangeTracker.Entries<Vehicle>().Where(v => v.State == EntityState.Modified))
      {
        vehicle.State = EntityState.Added;
        vehicle.Entity.Id = 0;
      }
      foreach (var workOrder in ChangeTracker.Entries<WorkOrder>())
      {
        workOrder.Entity.DateModified = now;
        if (string.IsNullOrEmpty(workOrder.Entity.Number))
        {
          workOrder.Entity.Number = (WorkOrders.Count() + 1).ToString();
        }
      }
      return base.SaveChanges();
    }


    public DbSet<WorkOrder> WorkOrders { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<WorkItem> WorkItems { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }


    protected class Migrator : DbMigrationsConfiguration<WorkOrdersContext>
    {
      public Migrator()
      {
        AutomaticMigrationDataLossAllowed = true;
        AutomaticMigrationsEnabled = true;
      }
    }

    protected class DbConfiguration
    {
      public interface IEntityConfiguration
      {
        void Register(DbModelBuilder builder);
      }

      public static void RegisterAll(DbModelBuilder builder)
      {
        var configurations = GetAllConfigurations();
        configurations.ForEach(c => c.Register(builder));
      }

      public static List<IEntityConfiguration> GetAllConfigurations()
      {
        return typeof(DbConfiguration).Assembly.GetTypes().Where(t =>
            !t.IsAbstract &&
            typeof(IEntityConfiguration).IsAssignableFrom(t))
            .Select(t => Activator.CreateInstance(t) as IEntityConfiguration)
            .ToList();
      }

      /// <summary>
      /// Base configuration for entities
      /// </summary>
      /// <typeparam name="TModel">The type of the model.</typeparam>
      public abstract class BaseConfiguration<TModel> : EntityTypeConfiguration<TModel>, IEntityConfiguration where TModel : class
      {
        public string SchemaName = "asm";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BaseConfiguration"/> class.
        /// </summary>
        protected BaseConfiguration()
        {
          ToTable(typeof(TModel).Name, SchemaName);
        }

        public void Register(DbModelBuilder builder)
        {
          builder.Configurations.Add(this);
        }
      }

      public abstract class BaseIdConfiguration<TModel> : BaseConfiguration<TModel> where TModel : BaseModel
      {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:BaseIdConfiguration" /> class.
        /// </summary>
        protected BaseIdConfiguration()
        {
          this.HasKey(model => model.Id);
        }
      }

      public class WorkOrderConfiguration : BaseIdConfiguration<WorkOrder>
      {
        public WorkOrderConfiguration()
        {
          this.HasOptional(wo => wo.Vehicle)
              .WithMany()
              .HasForeignKey(wo => wo.VehicleId);

          this.HasOptional(wo => wo.Client)
              .WithMany()
              .HasForeignKey(wo => wo.ClientId);

          this.HasMany(wo => wo.WorkItems)
              .WithRequired(wi => wi.WorkOrder)
              .HasForeignKey(wi => wi.WorkOrderId);

          this.Ignore(wo => wo.WorkNeeded);
          this.Ignore(wo => wo.WorkOrdered);
          this.Ignore(wo => wo.WorkPerformed);
          this.Ignore(wo => wo.PartsInstalled);
          this.Ignore(wi => wi.JournalingId);

          //this.HasMany(wo => wo.WorkOrdered)
          //    .WithRequired(wi => wi.OrderedForWorkOrder)
          //    .HasForeignKey(wi => wi.OrderedForWorkOrderId)
          //    .WillCascadeOnDelete(false);

          //this.HasMany(wo => wo.WorkNeeded)
          //    .WithRequired(wi => wi.NeededForWorkOrder)
          //    .HasForeignKey(wi => wi.NeededForWorkOrderId)
          //    .WillCascadeOnDelete(false);

          //this.HasMany(wo => wo.WorkPerformed)
          //    .WithRequired(wi => wi.PerformedForWorkOrder)
          //    .HasForeignKey(wi => wi.PerformedForWorkOrderId)
          //    .WillCascadeOnDelete(false);

          //this.HasMany(wo => wo.PartsInstalled)
          //    .WithRequired(wi => wi.PartInstalledForWorkOrder)
          //    .HasForeignKey(wi => wi.PartInstalledForWorkOrderId)
          //    .WillCascadeOnDelete(false);
        }
      }

      public class WorkItemConfiguration : BaseIdConfiguration<WorkItem>
      {
        public WorkItemConfiguration()
        {
          this.Ignore(wi => wi.JournalingId);
        }
      }

      public class ClientConfiguration: BaseIdConfiguration<Client>
      {
        
      }

      public class VehicleConfiguration:BaseIdConfiguration<Vehicle>
      {
         
      }
    }
  }
}