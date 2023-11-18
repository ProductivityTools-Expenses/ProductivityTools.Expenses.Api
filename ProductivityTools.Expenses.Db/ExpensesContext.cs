using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Database
{
    public class ExpensesContext: DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<Expense> Expenses { get; set; }

        public ExpensesContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PTExpenses"));
                optionsBuilder.UseLoggerFactory(GetLoggerFactory());
                optionsBuilder.EnableSensitiveDataLogging();
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("me");
            modelBuilder.Entity<Expense>().ToTable("Expense")
                .HasKey(x => x.ExpenseId);
            //modelBuilder.Entity<Account>().HasMany(x => x.TransfersSource).WithOne(x => x.Source).HasForeignKey(x => x.SourceId).HasPrincipalKey(x => x.AccountId);
            //modelBuilder.Entity<Account>().HasMany(x => x.TransfersTarget).WithOne(x => x.Target).HasForeignKey(x => x.TargetId).HasPrincipalKey(x => x.AccountId);
          
            //modelBuilder.Entity<TransferHistory>().ToTable("TransferHistory")
            //    .HasKey(x => x.TransferHistoryId);
            //modelBuilder.Entity<Account>().ToTable("Account")
            //    .HasKey(x => x.AccountId);
            base.OnModelCreating(modelBuilder);
        }
    }
}