using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrders.Domain.Models.Common;

namespace WorkOrders.Domain.Models
{
  public enum WorkItemType
  {
    Ordered = 1,
    Needed = 2,
    Performed = 3,
    PartInstalled = 4
  }

  public class WorkItem : BaseModel
  {

    public string Name { get; set; }

    public decimal Amount { get; set; }

    public string Unit { get; set; }

    public decimal Value { get; set; }

    public WorkItemType Type { get; set; }

    public long WorkOrderId { get; set; }

    public virtual WorkOrder WorkOrder { get; set; }

  }

}
