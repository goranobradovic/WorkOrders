using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

    public long? OrderedForWorkOrderId { get; set; }

    [ForeignKey("OrderedForWorkOrderId")]
    public virtual WorkOrder OrderedForWorkOrder { get; set; }

    public long? NeededForWorkOrderId { get; set; }

    [ForeignKey("NeededForWorkOrderId")]
    public virtual WorkOrder NeededForWorkOrder { get; set; }

    public long? PerformedForWorkOrderId { get; set; }

    [ForeignKey("PerformedForWorkOrderId")]
    public virtual WorkOrder PerformedForWorkOrder { get; set; }

    public long? PartInstalledForWorkOrderId { get; set; }

    [ForeignKey("PartInstalledForWorkOrderId")]
    public virtual WorkOrder PartInstalledForWorkOrder { get; set; }
  }
}
