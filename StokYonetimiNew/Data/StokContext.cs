namespace StokYonetimiNew.Data
{
    using Microsoft.EntityFrameworkCore;
    using StokYonetimiNew.Models;

    public class StokContext : DbContext
    {
        public StokContext(DbContextOptions<StokContext> options)
            : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<CustomerTeam> CustomerTeams { get; set; }
        public DbSet<MaterialEntry> MaterialEntries { get; set; }
        public DbSet<MaterialExit> MaterialExits { get; set; }
        public DbSet<StockExit> StockExits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnits => Set<MeasurementUnit>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "Adet" },
                new Unit { Id = 2, Name = "Kilogram" },
                new Unit { Id = 3, Name = "Gram" },
                new Unit { Id = 4, Name = "Litre" },
                new Unit { Id = 5, Name = "Mililitre" },
                new Unit { Id = 6, Name = "Metre" },
                new Unit { Id = 7, Name = "Kilometre" }

            );
        }
    }
   



}
