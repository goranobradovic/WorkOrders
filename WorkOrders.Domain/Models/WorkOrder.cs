using System;
using System.ComponentModel.DataAnnotations.Schema;
using WorkOrders.Domain.Models.Common;

namespace WorkOrders.Domain.Models
{
    public class WorkOrder : BaseModel
    {
        public WorkOrder()
        {
            this.DateModified = DateTime.Now;
        }

        public WorkOrder(long? vehicleId)
            : this()
        {
            this.VehicleId = vehicleId;
        }

        /// <summary>
        /// Gets or sets the advice.
        /// </summary>
        /// <value>The advice.</value>
        public string Advice { get; set; }

        /// <summary>
        /// Gets or sets the approved.
        /// </summary>
        /// <value>The approved.</value>
        public bool Approved { get; set; }

        /// <summary>
        /// Gets or sets the approved max value.
        /// </summary>
        /// <value>The approved max value.</value>
        public decimal? ApprovedMaxValue { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public virtual Client Client { get; set; }

        [ForeignKey("Client")]
        public long? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the date of completion.
        /// </summary>
        /// <value>The date of completion.</value>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime? DateReceived { get; set; }

        /// <summary>
        /// Gets or sets the deadline.
        /// </summary>
        /// <value>The deadline.</value>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Gets or sets the delivered to.
        /// </summary>
        /// <value>The delivered to.</value>
        public string DeliveredTo { get; set; }

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>The employee.</value>
        public string Employee { get; set; }

        /// <summary>
        /// Gets or sets the estimated value.
        /// </summary>
        /// <value>The estimated value.</value>
        public decimal? EstimatedValue { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets the request for estimate.
        /// </summary>
        /// <value>The request for estimate.</value>
        public bool RequestForEstimate { get; set; }
        /// <summary>
        /// Gets or sets the car.
        /// </summary>
        /// <value>The car.</value>
        public virtual Vehicle Vehicle { get; set; }

        [ForeignKey("Vehicle")]
        public long? VehicleId { get; set; }
    }
}
