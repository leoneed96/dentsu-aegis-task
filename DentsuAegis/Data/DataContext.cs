using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class DataContext : DbContext
    {
        public DbSet<RepositoryInfo> Repositories { get; set; }
        public DbSet<SearchRequest> SearchRequests { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            Database.Migrate();
        }
        public DataContext()
        {
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepositoryInfo>(a =>
            {
                a.HasKey(k => k.ID);
                a.Property(x => x.ID).UseIdentityColumn();
                a.Property(x => x.AuthorAvatar);
                a.Property(x => x.AuthorLogin);
                a.Property(x => x.CodeLanguage);
                a.Property(x => x.Description);
                a.Property(x => x.Forks);
                a.Property(x => x.LastUpdate);
                a.Property(x => x.Link);
                a.Property(x => x.Stars);
                a.Property(x => x.Title);

                a.HasOne(x => x.Search)
                    .WithMany(x => x.Repositories)
                    .HasForeignKey(x => x.SearchId);
            });

            modelBuilder.Entity<SearchRequest>(a =>
            {
                a.HasKey(k => k.ID);
                a.Property(x => x.ID).UseIdentityColumn();
                a.Property(x => x.ExecutionDate);
                a.Property(x => x.SearchString).HasMaxLength(255);
                a.HasIndex(x => x.SearchString).IsUnique(true);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
