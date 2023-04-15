﻿using Microsoft.EntityFrameworkCore;

namespace BornomalaStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //create connection with entity framework core
        {

        }
    }
}
