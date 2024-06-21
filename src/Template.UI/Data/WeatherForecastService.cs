using Microsoft.AspNetCore.Components;
using Template.UI.Components;

namespace Template.UI.Data
{
    public class WeatherForecastService
    {
        private List<WeatherForecast> Summaries { get; set; }

        public WeatherForecastService()
        {

            var summary = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            Summaries = new List<WeatherForecast>();
            for (var i = 0; i < 50; i++)
            {
                Summaries.Add(new WeatherForecast
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddDays(Random.Shared.Next(-30, 30)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summary[Random.Shared.Next(summary.Length)]
                });
            }


        }
        public Task<Guid> Update(WeatherForecast item)
        {
            var exist = Summaries.Find(x => x.Id.Equals(item.Id));

            if (!exist.Id.Equals(Guid.Empty))
            {
                Summaries.Remove(exist);
            }
            else
            {
                item.Id= Guid.NewGuid();
            }

            Summaries.Add(item);

            return Task.FromResult(item.Id);
        }

        public async Task<List<WeatherForecast>> LazyGet(WeatherForecastFilterModel filter)
        {
            await Task.Delay(2000);
             return  await Get(filter);
        }

        public Task<List<WeatherForecast>> Get(WeatherForecastFilterModel filter)
        {
            filter.DateFrom ??= DateTime.MinValue;
            filter.DateTo ??= DateTime.MaxValue;
            var res = Summaries.Where(x =>
                x.Date >= filter.DateFrom
                && x.Date <= filter.DateTo
                && x.Id == (filter.Id ?? x.Id) &&
                x.isDelete == false);

            return Task.FromResult(res.ToList());
        }

        public Task Remove(Guid id)
        {

            Summaries.Remove(Summaries.First(x => x.Id.Equals(id)));

            return Task.CompletedTask;
        }
    }
}