﻿using last_try_api.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<UserLoginModel> UserLoginModels { get; set; }

    // Other DbSet properties as needed
}
