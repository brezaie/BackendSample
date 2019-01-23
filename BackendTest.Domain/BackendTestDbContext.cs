using BackendTest.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Domain
{
    public class BackendTestDbContext : DbContext
    {
        public BackendTestDbContext()
        {
        }

        public BackendTestDbContext(DbContextOptions<BackendTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClassDetail> ClassDetail { get; set; }
        public virtual DbSet<ClassDetailStudent> ClassDetailStudent { get; set; }
        public virtual DbSet<ClassMaster> ClassMaster { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=BackendTest;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassDetailStudent>()
                .HasKey(cde => new { cde.Id});
            modelBuilder.Entity<ClassDetailStudent>()
                .HasOne(cde => cde.ClassDetail)
                .WithMany(cd => cd.ClassDetailStudents)
                .HasForeignKey(cde => cde.ClassDetailId);
            modelBuilder.Entity<ClassDetailStudent>()
                .HasOne(cde => cde.Student)
                .WithMany(s => s.ClassDetailStudents)
                .HasForeignKey(cde => cde.StudentId);

            modelBuilder.Entity<ClassDetail>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClassMaster)
                    .WithMany(p => p.ClassDetail)
                    .HasForeignKey(d => d.ClassMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassDetail_ClassMaster");
            });

            modelBuilder.Entity<ClassMaster>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.TeacherName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Gpa).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);
            });
        }
    }
}
