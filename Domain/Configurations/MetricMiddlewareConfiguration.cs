namespace Domain.Configurations
{
    public class MetricMiddlewareConfiguration
    {
        public string RouteName { get; set; }
        public string RoutePath { get; set; }
        public int RouteSla { get; set; }
    }
}