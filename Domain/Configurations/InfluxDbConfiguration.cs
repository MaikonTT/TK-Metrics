namespace Domain.Configurations
{
    public class InfluxDbConfiguration
    {
        public string Url { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}