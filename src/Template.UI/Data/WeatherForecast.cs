namespace Template.UI.Data
{

    public struct WeatherForecast
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public bool isDelete { get; set; }
    }
}