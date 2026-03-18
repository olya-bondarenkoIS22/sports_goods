using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sport.Models;

namespace sport;

public partial class SportingGoodsStoreContext : DbContext
{
    public SportingGoodsStoreContext()
    {
    }

    public SportingGoodsStoreContext(DbContextOptions<SportingGoodsStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressesOfPickUpPoint> AddressesOfPickUpPoints { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersSportingGood> OrdersSportingGoods { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SportingGood> SportingGoods { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<UnitsOfMeasurement> UnitsOfMeasurements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sporting_goods_store;Username=postgres;Password=1111");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressesOfPickUpPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("фddresses_of_pick-up_points_pkey");

            entity.ToTable("addresses_of_pick-up_points");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"фddresses_of_pick-up_points_id_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category1).HasColumnName("category");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manufacturers_pkey");

            entity.ToTable("manufacturers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Manufacturer1).HasColumnName("manufacturer");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.IdDeliveryPointAddress).HasColumnName("id_delivery_point_address");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");

            entity.HasOne(d => d.AddressesOfPickUpPoint).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdDeliveryPointAddress)
                .HasConstraintName("fk_orders_addresses_of_pick-up_points");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_statuses");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_users");
        });

        modelBuilder.Entity<OrdersSportingGood>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IdOrder, e.IdSportingGoods }).HasName("orders_sporting_goods_pkey");

            entity.ToTable("orders_sporting_goods");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdSportingGoods).HasColumnName("id_sporting_goods");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrdersSportingGoods)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_sporting_goods_orders");

            entity.HasOne(d => d.IdSportingGoodsNavigation).WithMany(p => p.OrdersSportingGoods)
                .HasForeignKey(d => d.IdSportingGoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_sporting_goods_sporting_goods");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role1).HasColumnName("role");
        });

        modelBuilder.Entity<SportingGood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sporting_goods_pkey");

            entity.ToTable("sporting_goods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Article).HasColumnName("article");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdManufacturer).HasColumnName("id_manufacturer");
            entity.Property(e => e.IdSupplier).HasColumnName("id_supplier");
            entity.Property(e => e.IdUnitOfMeasurement).HasColumnName("id_unit_of_measurement");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");

            entity.HasOne(d => d.Category).WithMany(p => p.SportingGoods)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sporting_goods_categories");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.SportingGoods)
                .HasForeignKey(d => d.IdManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sporting_goods_manufacturers");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SportingGoods)
                .HasForeignKey(d => d.IdSupplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sporting_goods_suppliers");

            entity.HasOne(d => d.UnitsOfMeasurement).WithMany(p => p.SportingGoods)
                .HasForeignKey(d => d.IdUnitOfMeasurement)
                .HasConstraintName("fk_sporting_goods_units of measurement");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statuses_pkey");

            entity.ToTable("statuses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status1).HasColumnName("status");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Supplier1).HasColumnName("supplier");
        });

        modelBuilder.Entity<UnitsOfMeasurement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("units_of_measurement_pkey");

            entity.ToTable("units_of_measurement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UnitOfMeasurement).HasColumnName("unit_of_measurement");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ussers_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
