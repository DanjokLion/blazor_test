using Template.Domain.Base;
using Template.Domain.Dto;
using Template.Domain.Models;

namespace Template.Domain.Interfaces
{
    public interface IService
    {
        /// <summary>
        /// сохранение
        /// </summary>
        /// <param name="message"></param>
        /// <returns>модель</returns>
        /// <exception cref="BusinessException">Ошибка логики</exception>
        public TemplateModel Save(TemplateDto message);
    }
}