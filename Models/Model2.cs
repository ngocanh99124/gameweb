namespace GameWeb_XL.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<BanBe> BanBes { get; set; }
        public virtual DbSet<LoiMoiKB> LoiMoiKBs { get; set; }
        public virtual DbSet<NguoiChoi> NguoiChois { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BanBe>()
                .Property(e => e.Ten1)
                .IsUnicode(false);

            modelBuilder.Entity<BanBe>()
                .Property(e => e.Ten2)
                .IsUnicode(false);

            modelBuilder.Entity<LoiMoiKB>()
                .Property(e => e.TenGui)
                .IsUnicode(false);

            modelBuilder.Entity<LoiMoiKB>()
                .Property(e => e.TenNhan)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiChoi>()
                .Property(e => e.Ten)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiChoi>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiChoi>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiChoi>()
                .HasMany(e => e.BanBes)
                .WithRequired(e => e.NguoiChoi)
                .HasForeignKey(e => e.Ten1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiChoi>()
                .HasMany(e => e.BanBes1)
                .WithRequired(e => e.NguoiChoi1)
                .HasForeignKey(e => e.Ten2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiChoi>()
                .HasMany(e => e.LoiMoiKBs)
                .WithRequired(e => e.NguoiChoi)
                .HasForeignKey(e => e.TenGui)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiChoi>()
                .HasMany(e => e.LoiMoiKBs1)
                .WithRequired(e => e.NguoiChoi1)
                .HasForeignKey(e => e.TenNhan)
                .WillCascadeOnDelete(false);
        }
    }
}
