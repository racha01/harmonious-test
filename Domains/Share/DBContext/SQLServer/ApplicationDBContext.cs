using DBContext.SQLServer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DBContext.SQLServer
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        public DbSet<Wages> Wages { get; set; }

        public DbSet<EmplayeeWages> EmployeeWages { get; set; }
    }
}
