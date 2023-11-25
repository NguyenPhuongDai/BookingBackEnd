using System;
using System.Collections.Generic;
using BookingTourWeb_WebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookingTourWeb_WebAPI.Models;

public partial class DvmayBayContext : DbContext
{
    public DvmayBayContext()
    {
    }

    public DvmayBayContext(DbContextOptions<DvmayBayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietve> Chitietves { get; set; }

    public virtual DbSet<Chuyenbay> Chuyenbays { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Maybay> Maybays { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Ve> Ves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Name=DVMayBayDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietve>(entity =>
        {
            entity.HasKey(c => c.MaCTV);
            entity.ToTable("CHITIETVE");

            entity.Property(e => e.LoaiVe)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.MaChuyenBay).HasMaxLength(100);
            entity.Property(e => e.TinhTrang)
                .HasMaxLength(20)
                .HasDefaultValueSql("(N'Đang xác nhận')");
            entity.Property(e => e.TongGia).HasColumnType("decimal(16, 4)");

            //entity.HasOne(d => d.MaChuyenBayNavigation).WithMany()
            //    .HasForeignKey(d => d.MaChuyenBay)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_CHITIETVE_CHUYENBAY");

            //entity.HasOne(d => d.MaVeNavigation).WithMany()
            //    .HasForeignKey(d => d.MaVe)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_CHITIETVE_VE");
        });

        modelBuilder.Entity<Chuyenbay>(entity =>
        {
            entity.HasKey(e => e.MaChuyenBay);

            entity.ToTable("CHUYENBAY");

            entity.Property(e => e.MaChuyenBay).HasMaxLength(100);
            entity.Property(e => e.DonGia).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.GioBay).HasPrecision(6);
            entity.Property(e => e.MaMayBay).HasMaxLength(10);
            entity.Property(e => e.NgayXuatPhat).HasColumnType("date");
            entity.Property(e => e.NoiDen).HasMaxLength(15);
            entity.Property(e => e.NoiXuatPhat).HasMaxLength(15);
            entity.Property(e => e.SoLuongVeBsn).HasColumnName("SoLuongVeBSN");
            entity.Property(e => e.SoLuongVeEco).HasColumnName("SoLuongVeECO");

            //entity.HasOne(d => d.MaMayBayNavigation).WithMany(p => p.Chuyenbays)
            //    .HasForeignKey(d => d.MaMayBay)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_CHUYENBAY_MAYBAY")
            //    .IsRequired(false);
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("KHACHHANG");

            entity.HasIndex(e => e.GmailKh, "KHACHHANG_GmailKH").IsUnique();

            entity.HasIndex(e => e.Sdt, "KHACHHANG_SDT").IsUnique();

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.GmailKh)
                .HasMaxLength(50)
                .HasColumnName("GmailKH");
            entity.Property(e => e.Phai).HasMaxLength(5);
            entity.Property(e => e.Sdt)
                .HasMaxLength(13)
                .HasColumnName("SDT");
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.Khachhangs)
                .HasForeignKey(d => d.MaTaiKhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KHACHHANG_TAIKHOAN");
        });

        modelBuilder.Entity<Maybay>(entity =>
        {
            entity.HasKey(e => e.MaMayBay);

            entity.ToTable("MAYBAY");

            entity.Property(e => e.MaMayBay).HasMaxLength(10);
            entity.Property(e => e.SlgheBsn).HasColumnName("SLGheBSN");
            entity.Property(e => e.SlgheEco).HasColumnName("SLGheECO");
            entity.Property(e => e.TenMayBay).HasMaxLength(20);
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan);

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.TaiKhoan1, "TAIKHOAN_TaiKhoan").IsUnique();

            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.TaiKhoan1)
                .HasMaxLength(50)
                .HasColumnName("TaiKhoan");
            entity.Property(e => e.VaiTro).HasDefaultValueSql("((2))");
        });

        modelBuilder.Entity<Ve>(entity =>
        {
            entity.HasKey(e => e.MaVe);

            entity.ToTable("VE");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayDatVe).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Ves)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VE_KHACHHANG");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<ThongTinChuyenBay>? ThongTinChuyenBay { get; set; }
}
