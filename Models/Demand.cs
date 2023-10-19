using System.ComponentModel.DataAnnotations;

namespace last_try_api.Models
{
    public class Demand
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Demand text is required.")]
        public string DemandText { get; set; }

        // Foreign key for Complaint
        public int ComplaintId { get; set; }
        public Complaint Complaint { get; set; }
    }
}
