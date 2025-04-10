using System.Net;

namespace SignSafe.Presentation.ActionFilters.GlobalApi
{
    public class GlobalApiReturn
    {
        public string Title { get; set; } = "Title";
        public HttpStatusCode Status { get; set; }
        public string Type { get; set; } = "Type";
        public object? Data { get; set; }
    }
}
