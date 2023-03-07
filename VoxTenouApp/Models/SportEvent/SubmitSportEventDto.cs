using System.ComponentModel.DataAnnotations;
using VoxTenouApp.Models.Organizer;

namespace VoxTenouApp.Models.SportEvent
{
    public class SubmitSportEventDto
    {
        [Required(ErrorMessage = "Event Date is Required")]
        public string eventDate { get; set; }
        [Required(ErrorMessage = "Event Name is Required")]
        public string eventName { get; set; }
        [Required(ErrorMessage = "Event Type is Required")]
        public string eventType { get; set; }
        [Required(ErrorMessage = "Organizer is Required")]
        public int organizerId { get; set; }
    }

    public class SubmitEditSportEventDto
    {
        [Required(ErrorMessage ="Event Date is Required")]
        public string eventDate { get; set; }
        [Required(ErrorMessage = "Event Name is Required")]
        public string eventName { get; set; }
        [Required(ErrorMessage = "Event Type is Required")]
        public string eventType { get; set; }
        [Required(ErrorMessage = "Organizer is Required")]
        public int organizerId { get; set; }
        [Required(ErrorMessage = "Data is Required")]
        public int id { get; set; }
    }
}
