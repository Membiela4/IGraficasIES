using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGraficasIES
{
    internal interface iEmpleadoPublico
    {
        

        public enum TipoMedico
        {
            SeguridadSocial = 1,
            Muface = 2
        }
        public TipoMedico SeguroMedico { get; set; }

        //public (int,int,int) TiempoServicio();
        //public int GetSexenios();
        //public int GetTrienios();
    }
}
