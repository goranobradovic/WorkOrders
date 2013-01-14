using WorkOrders.Domain.Models.Common;

namespace WorkOrders.Domain.Models
{
  public class Vehicle : BaseJournalingModel
  {
    /// <summary>
    /// Gets or sets the manufacturer.
    /// </summary>
    /// <value>
    /// The manufacturer.
    /// </value>
    public string Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the VIN.
    /// </summary>
    /// <value>
    /// The VIN.
    /// </value>
    public string VIN { get; set; }

    /// <summary>
    /// Gets or sets the registration number.
    /// </summary>
    /// <value>
    /// The registration number.
    /// </value>
    public string RegistrationNumber { get; set; }

    /// <summary>
    /// Gets or sets the engine number.
    /// </summary>
    /// <value>
    /// The engine number.
    /// </value>
    public string EngineNumber { get; set; }

    /// <summary>
    /// Gets or sets the odometer.
    /// </summary>
    /// <value>
    /// The odometer.
    /// </value>
    public long? Odometer { get; set; }
  }
}
