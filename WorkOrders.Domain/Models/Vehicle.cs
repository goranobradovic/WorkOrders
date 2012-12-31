using WorkOrders.Domain.Models.Common;

namespace WorkOrders.Domain.Models
{
    public class Vehicle:BaseModel
    {
        public string Manufacturer { get; set; }
        
        public string Model { get; set; }

        public string VIN { get; set; }

        public string RegistrationNumber { get; set; }

        public string EngineNumber { get; set; }
    }
}
