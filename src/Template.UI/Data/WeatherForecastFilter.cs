namespace Template.UI.Data;

public struct WeatherForecastFilterModel
{
    public Guid? Id { get; set; }
    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; } 

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? Summary { get; set; }
}