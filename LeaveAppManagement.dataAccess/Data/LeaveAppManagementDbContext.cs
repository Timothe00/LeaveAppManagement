using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace LeaveAppManagement.dataAccess.Data
{
    public class LeaveAppManagementDbContext : DbContext
    {
        //my entities
        public DbSet<Users> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Role> Roles { get; set; }

        public LeaveAppManagementDbContext(DbContextOptions<LeaveAppManagementDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-1LD6C3B\\SQLEXPRESS;Database=LeaveManagementDb;Trusted_Connection=True;Encrypt=false;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuration de la relation Many-To-One entre DemandeConge et Employe
            modelBuilder.Entity<Users>(u => {
                u.HasKey(pk => pk.Id);
                u.HasIndex(e => e.Email).IsUnique();
                u.HasIndex(p => p.PhoneNumber).IsUnique();
            });


            // Configuration de la relation Many-To-One entre DemandeConge et Gestionnaire
            modelBuilder.Entity<Role>(u =>
            {
                u.HasKey(pk => pk.Id);
                u.HasIndex(r => r.Name).IsUnique();
            });


            // Configuration de la relation One-To-One entre Employe et SoldeConge
            modelBuilder.Entity<LeaveBalance>(lb =>
            {
                lb.HasKey(pk => pk.Id);
            });

            modelBuilder.Entity<LeaveRequest>(lr =>
            {
                lr.HasKey(pk => pk.Id);
            });

            modelBuilder.Entity<LeaveType>(lt =>
            {
                lt.HasKey(pk => pk.Id);
            });

        }

    }
}
