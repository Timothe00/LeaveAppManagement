using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace LeaveAppManagement.dataAccess.Data
{
    public class LeaveAppManagementDbContext : DbContext
    {
        //my entities
        public DbSet<Users> Users { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<LeaveBalance> LeaveBalance { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public LeaveAppManagementDbContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-1LD6C3B\\SQLEXPRESS;Database=LeaveManagementDb;Trusted_Connection=True;Encrypt=false;");
        }
    }
}
