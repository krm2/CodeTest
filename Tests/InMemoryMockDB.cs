using DataServices;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class InMemoryMockDB
    {
        public ApplicationContext GetInMemAppContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                .Options;
            var dbContext = new ApplicationContext(options);

            return dbContext;
        }
    }
}
