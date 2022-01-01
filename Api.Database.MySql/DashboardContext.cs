using Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Database.MySql
{
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptions<DashboardContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccountUserInvitation> AccountUserInvitations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AreasOfPractice> AreasOfPractice { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreasOfPractice>()
                .HasData(
                    new AreasOfPractice
                    {
                        Id = 1,
                        ExternalId = "d3ba0922-a24e-4e11-82bb-5ea04d25e16c",
                        Name = "Family Law"
                    },
                    new AreasOfPractice
                    {
                        Id = 2,
                        ExternalId = "6fa539f4-bf66-4a13-9ae9-c051c57adb8b",
                        Name = "Consumer Law"
                    },
                    new AreasOfPractice
                    {
                        Id = 3,
                        ExternalId = "b36dee61-ce14-4bc4-9943-cee671dde98e",
                        Name = "Corporate Law"
                    },
                    new AreasOfPractice
                    {
                        Id = 4,
                        ExternalId = "4f718103-f998-4acd-8697-9c19c6acfb3a",
                        Name = "Debt and Finance"
                    },
                    new AreasOfPractice
                    {
                        Id = 5,
                        ExternalId = "ad46cb40-a55b-46fd-ad21-cafe330a389e",
                        Name = "Property Law"
                    }
                );
        }
    }
}