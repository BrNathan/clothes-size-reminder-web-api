using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesCategory> ClothesCategories { get; set; }
        public DbSet<ClothesSize> ClothesSizes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
