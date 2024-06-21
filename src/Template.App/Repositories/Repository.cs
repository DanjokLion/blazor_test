using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using Template.Domain.Declare;
using Template.Domain.Dto;
using Template.Domain.Interfaces;
using Template.Domain.Models;

namespace Template.App.Repositories
{
    public class Repository : IService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Domain.Entities.TemplateEntity> _repository;

        public Repository(ILogger<Repository> logger, IOptions<AppSettings> options, HttpClient client, IRepository<Domain.Entities.TemplateEntity> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public TemplateModel Save(TemplateDto message)
        {
            try
            {
                _repository.Get(a => a.Status == Status.Fault && a.Uuid == "hgf");
                _logger.LogInformation("Received Text: {message}", message);
                return default;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "упс");
                throw;
            }
        }
    }
}