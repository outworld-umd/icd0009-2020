using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF {

    public class AppDbContext : IdentityDbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }

}