using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUI.Models;
using MAUI.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace MAUI.DataAccess
{
    public class VideoGMDbContext: DbContext
    {
        public DbSet<Videogame> Videogames { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("videogames.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Videogame>(entity =>
            {
                entity.HasKey(col => col.IdVideoGame);
                entity.Property(col => col.IdVideoGame).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
