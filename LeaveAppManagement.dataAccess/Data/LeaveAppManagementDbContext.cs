using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace LeaveAppManagement.dataAccess.Data
{
    public class LeaveAppManagementDbContext : DbContext
    {
        //my entities
        public DbSet<Users> TUsers { get; set; }
        public DbSet<Employee> TEmployee { get; set; }
        public DbSet<Manager> TManager { get; set; }
        public DbSet<Admin> TAdmin { get; set; }
        public DbSet<LeaveBalance> TLeaveBalance { get; set; }
        public DbSet<LeaveCalendar> TLeaveCalendars { get; set; }
        public DbSet<LeaveRequest> TLeaveRequest { get; set; }
        public DbSet<CalendarRequest> TCalendarRequests { get; set; }

        public LeaveAppManagementDbContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-1LD6C3B\\SQLEXPRESS;Database=LeaveManagementDb;Trusted_Connection=True;Encrypt=false;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("TUsers");
            modelBuilder.Entity<Employee>().ToTable("TEmployee");
            modelBuilder.Entity<Manager>().ToTable("TManager");
            modelBuilder.Entity<Admin>().ToTable("TAdmin");

            // Configuration de la relation Many-To-One entre DemandeConge et Employe
            modelBuilder.Entity<LeaveRequest>(l => {
                l.HasKey(s => s.Id);

                l.HasOne(lr => lr.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });


            // Configuration de la relation Many-To-One entre DemandeConge et Gestionnaire
            modelBuilder.Entity<LeaveRequest>(l =>
            {
                l.HasKey(pk => pk.Id);

                l.HasOne(lr => lr.Manager)
                .WithMany(m => m.LeaveRequestPending)
                .HasForeignKey(fk => fk.ManagerId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });


            // Configuration de la relation One-To-One entre Employe et SoldeConge
            modelBuilder.Entity<LeaveBalance>(l =>
            {
                l.HasKey(pk => pk.Id);
                l.HasOne(lb => lb.Employee)
                .WithOne(e => e.LeaveBalance)
                .HasForeignKey<LeaveBalance>(fk => fk.EmployeeId)
                .OnDelete(DeleteBehavior.ClientCascade);

            });


            modelBuilder.Entity<LeaveCalendar>()
            .HasMany(lc => lc.LeaveRequests)
            .WithMany(lr => lr.LeaveCalendar)
            .Map(cs =>
            {
                cs.MapLeftKey("LeaveCalendarId");
                cs.MapRightKey("LeaveRequestId");
                cs.ToTable("CalendarRequest");
            });


        }

    }
}
