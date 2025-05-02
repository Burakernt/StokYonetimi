using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StokYonetimiNew.Data
{
    public class StokContextFactory : IDesignTimeDbContextFactory<StokContext>
    {
        public StokContext CreateDbContext(string[] args)
        {
            // 1) Build çıktısının yolu (bin/... klasörü)
            var basePath = AppContext.BaseDirectory;

            // 2) Oradan appsettings.json'i yükle
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 3) Connection string al
            var conn = config.GetConnectionString("DefaultConnection");

            // 4) DbContextOptions'i oluşturup DbContext'i yarat
            var optionsBuilder = new DbContextOptionsBuilder<StokContext>();
            optionsBuilder.UseNpgsql(conn);

            return new StokContext(optionsBuilder.Options);
        }
    }
}
