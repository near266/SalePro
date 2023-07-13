﻿using Jhipster.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Jhipster.Infrastructure.Data.Repositories
{
    public class JwtRepository: IJwtRepository
    {
        private readonly ApplicationDatabaseContext _applicationDatabaseContext;
        public JwtRepository(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _applicationDatabaseContext = applicationDatabaseContext;
        }

        public async Task<string> GetIdUser(string username)
        {
          var check =  await _applicationDatabaseContext.Users.FirstOrDefaultAsync(i=>i.UserName == username);
            return check.Id.ToString();
        }
       
    }
}
