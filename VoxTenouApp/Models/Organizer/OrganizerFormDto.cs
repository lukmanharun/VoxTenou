using System.ComponentModel.DataAnnotations;

namespace VoxTenouApp.Models.Organizer
{
    public class OrganizerFormDto
    {
        [Required]
        public string organizerName { get; set; }
        [Required]
        public string imageLocation { get; set; }
    }
    public class OrganizerFormSubmitDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string organizerName { get; set; }
        [Required]
        public string imageLocation { get; set; }
    }
}
