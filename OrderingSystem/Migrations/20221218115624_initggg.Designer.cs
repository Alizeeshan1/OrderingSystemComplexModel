// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderingSystem.Data;

#nullable disable

namespace OrderingSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221218115624_initggg")]
    partial class initggg
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OrderingSystem.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("OrderingSystem.Models.Order", b =>
                {
                    b.Property<int?>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("OrderId"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderingSystem.Models.OrderedItem", b =>
                {
                    b.Property<int?>("OrderId_FK")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId_Fk")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<decimal>("Sub_Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<int?>("UnitId_Fk")
                        .HasColumnType("int");

                    b.HasKey("OrderId_FK", "ItemId_Fk");

                    b.HasIndex("ItemId_Fk");

                    b.HasIndex("UnitId_Fk");

                    b.ToTable("OrderedItems");
                });

            modelBuilder.Entity("OrderingSystem.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"), 1L, 1);

                    b.Property<string>("UnitType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UnitId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("OrderingSystem.Models.UnitItem", b =>
                {
                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("UnitId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("UnitItems");
                });

            modelBuilder.Entity("OrderingSystem.Models.OrderedItem", b =>
                {
                    b.HasOne("OrderingSystem.Models.Item", "Item")
                        .WithMany("OrderedItems")
                        .HasForeignKey("ItemId_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingSystem.Models.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingSystem.Models.Unit", "Unit")
                        .WithMany("OrderedItems")
                        .HasForeignKey("UnitId_Fk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Item");

                    b.Navigation("Order");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("OrderingSystem.Models.UnitItem", b =>
                {
                    b.HasOne("OrderingSystem.Models.Item", "Item")
                        .WithMany("UnitItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingSystem.Models.Unit", "Unit")
                        .WithMany("UnitItems")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("OrderingSystem.Models.Item", b =>
                {
                    b.Navigation("OrderedItems");

                    b.Navigation("UnitItems");
                });

            modelBuilder.Entity("OrderingSystem.Models.Order", b =>
                {
                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("OrderingSystem.Models.Unit", b =>
                {
                    b.Navigation("OrderedItems");

                    b.Navigation("UnitItems");
                });
#pragma warning restore 612, 618
        }
    }
}
