using MassTransit;
using System.Threading.Tasks;
using Template.Domain.Dto;
using Template.Domain.Interfaces;

namespace Template.App.Consumers
{
    public class TemplateConsumer : IConsumer<TemplateDto>
    {
        private readonly IService _observable;

        public TemplateConsumer(IService observable)
        {
            _observable = observable;
        }

        public Task Consume(ConsumeContext<TemplateDto> context)
        {

            _observable.Save(context.Message);
            return Task.CompletedTask;
        }
    }


}