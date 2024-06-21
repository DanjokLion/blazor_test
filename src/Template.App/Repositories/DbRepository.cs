using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Interfaces;

namespace Template.App.Repositories
{
    public class DbRepository : DbContext, IRepository<TemplateEntity>
    {

        public DbRepository(DbContextOptions<DbRepository> configure) : base(configure)
        {

        }


        public IEnumerable<TemplateEntity> Get(Expression<Func<TemplateEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public TemplateEntity GetById(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Вызов ХП pushAdd с параметрами <see cref="PushAddEntity"/>
        /// </summary>
        /// <param name="entity"></param>
        public async Task Insert(TemplateEntity entity)
        {

            using var connecting = Database.GetDbConnection();

            var parameters = new PushAddEntity
            {
                KeyPart = entity.Id,
                PlatformId = entity.Status,
                Token = entity.Uuid
            };
            var result = await connecting.QueryAsync<TemplateEntity>("pushAdd", parameters, commandType: CommandType.StoredProcedure);
        }

    public void Delete(TemplateEntity entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void Update(TemplateEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}