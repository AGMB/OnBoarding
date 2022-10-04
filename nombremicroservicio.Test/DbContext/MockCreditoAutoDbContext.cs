using Microsoft.EntityFrameworkCore;
using nombremicroservicio.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Test.DbContext
{
    public class MockCreditoAutoDbContext
    {
        protected CreditoAutoDbContext BuildDatabaseMock(string nombreBase)
        {
            var dataBase = new DbContextOptionsBuilder<CreditoAutoDbContext>().UseInMemoryDatabase(nombreBase).Options;
            var dbContext = new CreditoAutoDbContext(dataBase);
            return dbContext;
        }
    }
}
