﻿using Dialogue_Visualizer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue_Visualizer.Helpers
{
    public class DialogueDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DialogueDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            else
            {
                optionsBuilder.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
            }
        }

        public DbSet<Dialogue> Dialogue { get; set; }
        public DbSet<Scene> Scene { get; set; }
        public DbSet<DialogueBlock> DialogueBlocks { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
