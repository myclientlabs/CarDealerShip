namespace Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class CarEntities : DbContext
    {
        public CarEntities()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<Make> Makes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<PurchaseType> PurchaseTypes { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Salesperson> Salespersons { get; set; }
        public virtual DbSet<Special> Specials { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Style> Styles { get; set; }
        public virtual DbSet<Transmission> Transmissions { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Color>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Color)
                .HasForeignKey(e => e.ExteriorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Vehicles1)
                .WithRequired(e => e.Color1)
                .HasForeignKey(e => e.InteriorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Inquiries)
                .WithRequired(e => e.Contact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Street1)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Street2)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Make>()
                .HasMany(e => e.Models)
                .WithRequired(e => e.Make)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Model)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PurchaseType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseType>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.PurchaseType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.PurchasePrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Salesperson>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Salesperson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.FloorPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Available)
                .IsFixedLength();

            modelBuilder.Entity<Stock>()
                .Property(e => e.Feature)
                .IsFixedLength();

            modelBuilder.Entity<Style>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Style>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Style)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transmission>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Transmission>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Transmission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.Vin)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.New)
                .IsFixedLength();

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.MSRP)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);
        }
    }
}
