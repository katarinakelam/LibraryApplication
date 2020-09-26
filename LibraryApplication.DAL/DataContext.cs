using System;
using System.Collections.Generic;
using LibraryApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace LibraryApplication.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRentEvent> BookRentEvents { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("BookEvent_seq", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.Entity<BookRentEvent>()
                .Property(o => o.Id)
                .HasDefaultValueSql("NEXT VALUE FOR dbo.BookEvent_seq");

            modelBuilder.Entity<User>().Property(p => p.UserContacts)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));
        }
    }
}
