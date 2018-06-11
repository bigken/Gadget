namespace Gadget.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gadget.Data.Entity;
    using Microsoft.EntityFrameworkCore;

    public class GadgetDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public GadgetDbContext(DbContextOptions<GadgetDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Author
            //base
            modelBuilder.Entity<Author>().Property(x => x.Id);
            modelBuilder.Entity<Author>().Property(x => x.CreatedDateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP()");
            modelBuilder.Entity<Author>().Property(x => x.UpdatedDateTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP()");
            modelBuilder.Entity<Author>().Property(x => x.CreatedBy).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Author>().Property(x => x.UpdatedBy).IsRequired().HasMaxLength(128);
            //
            modelBuilder.Entity<Author>().Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Author>().Property(x => x.LastName).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Author>().Property(x => x.Email).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Author>().Property(x => x.AvatarUrl).HasMaxLength(1024).HasDefaultValue("../images/2018/05/default-avatar.jpg");
            //key
            modelBuilder.Entity<Author>().HasKey(x => x.Id);
            #endregion

            #region Article
            //base
            modelBuilder.Entity<Article>().Property(x => x.Id);
            modelBuilder.Entity<Article>().Property(x => x.CreatedDateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP()");
            modelBuilder.Entity<Article>().Property(x => x.UpdatedDateTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP()");
            modelBuilder.Entity<Article>().Property(x => x.CreatedBy).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Article>().Property(x => x.UpdatedBy).IsRequired().HasMaxLength(128);
            //
            modelBuilder.Entity<Article>().Property(x => x.Title).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<Article>().Property(x => x.PublishedDateTime).HasDefaultValueSql("CURRENT_TIMESTAMP()");
            modelBuilder.Entity<Article>().Property(x => x.ContextFilePath).IsRequired().HasMaxLength(1024);
            modelBuilder.Entity<Article>().Property(x => x.Rating);

            //key
            modelBuilder.Entity<Article>().HasKey(x => x.Id);

            //navigate property
            modelBuilder.Entity<Article>().HasOne(x => x.Author);
            modelBuilder.Entity<Article>().HasMany(x => x.Comments);

            #endregion

            #region  Comment

            //base
            modelBuilder.Entity<Comment>().Property(x => x.Id);
            modelBuilder.Entity<Comment>().Property(x => x.CreatedDateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP()");
            modelBuilder.Entity<Comment>().Property(x => x.UpdatedDateTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP()");
            modelBuilder.Entity<Comment>().Property(x => x.CreatedBy).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Comment>().Property(x => x.UpdatedBy).IsRequired().HasMaxLength(128);
            //
            modelBuilder.Entity<Comment>().Property(x => x.Content).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<Comment>().Property(x => x.PublishedDateTime).HasDefaultValueSql("CURRENT_TIMESTAMP()");

            //key
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);

            //navigate property
            modelBuilder.Entity<Comment>().HasOne(x => x.Author);
            modelBuilder.Entity<Comment>().HasOne(x => x.Article);

            #endregion
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            AuditEntities();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AuditEntities()
        {
            var userName = "bigken";
            var utcNow = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(e => e.CreatedBy).CurrentValue = userName;
                    entry.Property(e => e.UpdatedBy).CurrentValue = userName;

                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.UpdatedBy).CurrentValue = userName;
                }
            }
        }
    }
}
