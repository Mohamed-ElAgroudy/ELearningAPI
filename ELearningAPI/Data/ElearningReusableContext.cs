using System;
using System.Collections.Generic;
using ELearningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Data;

public partial class ElearningReusableContext : DbContext
{
    public ElearningReusableContext()
    {
    }

    public ElearningReusableContext(DbContextOptions<ElearningReusableContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCourse> UserCourses { get; set; }

    public virtual DbSet<UserShoppingCartItem> UserShoppingCartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-BB9M7OKP;Initial Catalog=ELearning_Reusable;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A7E9664F60");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Users__A9D105359C8056F7");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Area).HasMaxLength(100);
            entity.Property(e => e.MobileNumber).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(e => e.UserCourseId).HasName("PK__UserCour__58886ED4226C89F8");

            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.UserEmail).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserCours__Cours__3D5E1FD2");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserCours__UserE__3C69FB99");
        });

        modelBuilder.Entity<UserShoppingCartItem>(entity =>
        {
            entity.HasKey(e => e.UserShoppingCartItemId).HasName("PK__UserShop__F2559B27BA9380E3");

            entity.ToTable("UserShoppingCartItem");

            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.UserEmail).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.UserShoppingCartItems)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserShopp__Cours__4222D4EF");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.UserShoppingCartItems)
                .HasForeignKey(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserShopp__UserE__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
