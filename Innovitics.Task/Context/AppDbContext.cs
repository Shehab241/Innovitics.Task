﻿using Microsoft.EntityFrameworkCore;

namespace Innovitics.Task.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }
        public DbSet<User> User { get; set; }
    }
}
