using Microsoft.EntityFrameworkCore;
using VEHICLE_MANAGEMENT_SYSTEM.Entity;
using VEHICLE_MANAGEMENT_SYSTEM.Models;

namespace VEHICLE_MANAGEMENT_SYSTEM.Data
{
    public class vehicledb_context : DbContext
    {
        public vehicledb_context(DbContextOptions<vehicledb_context> options) : base(options) { }
        public DbSet<DISTRICT> DISTRICT { get; set; }
        public DbSet<BLOCK> BLOCK { get; set; }
        public DbSet<TOFVC> TOFVC { get; set; }
        public DbSet<VCNAME> VCNAME { get; set; }
        public DbSet<NOOFVC> NOOFVC { get; set; }
        public DbSet<VEHICLE> VEHICLE { get; set; }
        public DbSet<VEHICLEINFO> VEHICLEINFO { get; set; }
    }
}
