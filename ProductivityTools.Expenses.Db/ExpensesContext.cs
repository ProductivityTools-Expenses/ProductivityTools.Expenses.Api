﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Database
{
    public class ExpensesContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Bag> Bag { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<BagCategory> BagCategories { get; set; }

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
            modelBuilder.Entity<Expense>().ToTable("Expense").HasKey(x => x.ExpenseId);
            modelBuilder.Entity<Expense>().Property(x => x.Cost).HasComputedColumnSql("Amount*Price+Additions-Deductions");
            modelBuilder.Entity<Expense>().Property(x => x.Value).HasComputedColumnSql("Amount*Price");

            modelBuilder.Entity<Bag>().ToTable("Bag").HasKey(x => x.BagId);

            modelBuilder.Entity<Category>().ToTable("Category").HasKey(x => x.CategoryId);

            modelBuilder.Entity<Bag>().HasMany(x => x.Expenses).WithOne(x => x.Bag).HasForeignKey(x => x.BagId).HasPrincipalKey(x => x.BagId);

            modelBuilder.Entity<Category>().HasMany(x => x.Expenses).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.CategoryId);

            modelBuilder.Entity<BagCategory>().ToTable("BagCategory").HasKey(x => x.BagCategoryId);
            modelBuilder.Entity<BagCategory>().HasOne(x => x.Bag).WithMany(x => x.BagCategories).HasForeignKey(x => x.BagId);
            modelBuilder.Entity<BagCategory>().HasOne(x => x.Category).WithMany(x => x.BagCategories).HasForeignKey(x => x.CategoryId);
            //modelBuilder.Entity<Bag>()
            //    .HasMany(e => e.Categories)
            //    .WithMany(e => e.Bags)
            //    .UsingEntity("BagCategory");

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