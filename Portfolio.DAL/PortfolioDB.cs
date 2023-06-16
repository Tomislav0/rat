using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Models;
using Portfolio.DAL.Models.Account;
using Portfolio.DAL.Models.Protfolio;

namespace Portfolio.DAL
{
    public class PortfolioDB : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly DbContextOptions<PortfolioDB> _options;


        public PortfolioDB(DbContextOptions<PortfolioDB> options) : base(options)
        {
            _options = options;
        }

        public static PortfolioDB CreateContext(string connectionString) { 
            var contextOptionsBuilder = new DbContextOptionsBuilder<PortfolioDB>();
            contextOptionsBuilder.UseSqlServer(connectionString);
            return new PortfolioDB(contextOptionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("User");
            modelBuilder.Entity<Role>()
               .ToTable("Role");
            modelBuilder.Entity<UserRole>()
               .ToTable("UserRole");
            modelBuilder.Entity<UserClaim>()
              .ToTable("UserClaim");
            modelBuilder.Entity<RoleClaim>()
                .ToTable("RoleClaim");
            modelBuilder.Entity<UserLogin>()
                .ToTable("UserLogin");
            modelBuilder.Entity<UserToken>()
                .ToTable("UserToken");

            modelBuilder.Entity<Picture>()
                .ToTable("Picture", "Gallery")
                .Property(x=>x.CreatedAt)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<PictureGroup>()
                .ToTable("PictureGroup", "Gallery");
        }


        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<PictureGroup> PictureGroup { get; set; }
    }
}
