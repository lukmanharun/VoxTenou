using VoxTenouApp.Models.Organizer;

namespace VoxTenouApp.Models.SportEvent
{
    public class GridSportEvent
    {
        public string eventDate { get; set; }
        public string eventName { get; set; }
        public string eventType { get; set; }
        public int id { get; set; }
        public int? organizerId { get; set; }
        public GridOrganizer organizer { get; set; }
    }
}
