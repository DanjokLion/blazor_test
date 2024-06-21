using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template.Domain.Dto;
using Template.Domain.Interfaces;
using Template.Domain.Models;

namespace Template.App.Controllers
{
    [ApiController]
    [Route("api/template/[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly IService _repository;
        public TemplateController(IService observable)
        {
            _repository = observable;
        }

        /// <summary>
        /// Регистрация документа
        /// </summary>
        /// <returns>200</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TemplateModel), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]

        public async Task<IActionResult> Send([FromBody] TemplateDto message)
        {
            var result = _repository.Save(message);
            return Ok(result);
        }
    }
}
