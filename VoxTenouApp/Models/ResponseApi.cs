namespace VoxTenouApp.Models
{
    public class ResponseApiError
    {
        public string message { get; set; }
        public object errors { get; set; }
        public int status_code { get; set; }
    }
    public class Links
    {
        public string previous { get; set; }
        public string next { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public Links links { get; set; }
    }

    public class ResponseApiPagination<T>
    {
        public List<T> data { get; set; }
        public Meta meta { get; set; }
    }
}
