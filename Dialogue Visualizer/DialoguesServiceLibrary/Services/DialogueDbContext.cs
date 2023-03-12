using DialoguesServiceLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialoguesServiceLibrary.Services
{
    internal class DialogueDbContext : DbContext
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

        public DbSet<Dialogue> _dialogues { get; set; }
    }
}
