using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicInside.DataAccessLayer.Context
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MusicInsideDbContext>
    {
        public MusicInsideDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicInsideDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MusicInsideTest;Trusted_Connection=True");

            return new MusicInsideDbContext(optionsBuilder.Options);
        }
    }
}
