using Microsoft.EntityFrameworkCore;
using OrderingSystem.Models;

namespace OrderingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<UnitItem> UnitItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //Item Model
            modelBuilder.Entity<Item>()
                        .Property(e => e.Name)
                        .IsRequired(true)
                        .HasMaxLength(50);

            modelBuilder.Entity<Item>()
                       .Property(e => e.Price)
                       .IsRequired(true)
                       .HasColumnType("decimal(18, 0)");


            //Order
            modelBuilder.Entity<Order>()
                       .Property(e => e.OrderName)
                       .IsRequired(true);
            modelBuilder.Entity<Order>()
                        .Property(e => e.OrderDate)
                        .IsRequired(true);
            modelBuilder.Entity<Order>()
                        .Property(e => e.TotalPrice)
                        .IsRequired(true);

            //Unit
            modelBuilder.Entity<Unit>()
                        .Property(e => e.UnitType)
                        .IsRequired(true)
                        .HasMaxLength(50);
           



            //OrderedItem
            modelBuilder.Entity<OrderedItem>()
                        .HasKey(k => new { k.OrderId_FK, k.ItemId_Fk });
            modelBuilder.Entity<OrderedItem>()
                    .Property(e => e.Quantity)
                    .IsRequired(true)
                    .HasMaxLength(50);
            modelBuilder.Entity<OrderedItem>()
                        .HasOne(o => o.Order)
                        .WithMany(oi => oi.OrderItem)
                        .HasForeignKey(f => f.OrderId_FK)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderedItem>()
                       .HasOne(i => i.Item)
                       .WithMany(oi => oi.OrderedItems)
                       .HasForeignKey(f => f.ItemId_Fk)
                       .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderedItem>()
                       .HasOne(u => u.Unit)
                       .WithMany(oi => oi.OrderedItems)
                       .HasForeignKey(f => f.UnitId_Fk)
                       .OnDelete(DeleteBehavior.Cascade);
          

            //UnitItem
            modelBuilder.Entity<UnitItem>()
                      .HasKey(k => new { k.UnitId, k.ItemId });
            modelBuilder.Entity<UnitItem>()
                        .HasOne(u => u.Unit)
                        .WithMany(ui => ui.UnitItems)
                        .HasForeignKey(f => f.UnitId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UnitItem>()
                .HasOne<Item>(u => u.Item)
                .WithMany(i => i.UnitItems)
                .HasForeignKey(ui => ui.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

        }
        #endregion



    }
}
