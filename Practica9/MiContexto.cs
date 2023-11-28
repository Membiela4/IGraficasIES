using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace IGraficasIES
{
    class MiContexto : DbContext
    {
        //Establecemos el motor de la base de datos
        //especificando su cadena de conexión
        protected override void OnConfiguring(DbContextOptionsBuilder
       optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IGraficasIES_David;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        
        public DbSet<ProfesorFuncionario> ProfesorFuncionario{ get; set; }
        public DbSet<ProfesorExtendido> ProfesorExtendido { get; set; }

    }
}
