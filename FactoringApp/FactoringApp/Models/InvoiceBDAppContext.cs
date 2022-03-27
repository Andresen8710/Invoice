using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FactoringApp.Models
{
    public partial class InvoiceBDAppContext : DbContext
    {
        public InvoiceBDAppContext()
        {
        }

        public InvoiceBDAppContext(DbContextOptions<InvoiceBDAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public virtual DbSet<Products> Products { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=LAPTOP-C4LF1QIK\\SQLEXPRESS;Database=InvoiceBDApp;User ID=sa;Password=F3l1p3.102*");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Age).HasComputedColumnSql("((CONVERT([int],CONVERT([char](10),getdate(),(112)))-CONVERT([char](10),CONVERT([date],[Birthdate]),(112)))/(10000))", false);

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Identification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.TaxValue).HasColumnType("money");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasComputedColumnSql("([TaxValue]+[SubTotal])", false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Customers");
            });

            modelBuilder.Entity<InvoiceDetails>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceId, e.RowNumber });

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasComputedColumnSql("([Price]*[Quantity])", false);

                entity.Property(e => e.TaxValue).HasColumnType("money");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasComputedColumnSql("([Price]*[Quantity]+[TaxValue])", false);

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_InvoiceDetails_Invoices");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_InvoiceDetails_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
