using System.ComponentModel.DataAnnotations;

namespace WorkOrders.Domain.Models.Common
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the original id.
        /// </summary>
        /// <value>
        /// The original id.
        /// </value>
        public long? JournalingId { get; set; }
    }
}
