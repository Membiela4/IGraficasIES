using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGraficasIES
{
    internal class ProfesorFuncionario : Profesor, iEmpleadoPublico
    {
        public ProfesorExtendido ProfesorExtendido { get; set; }
        public iEmpleadoPublico.TipoMedico SeguroMedico { get; set; }
        public int AnyoIngresoCuerpo {get; set; }
        public bool DestinoDefinitivo { get; set; }
        public bool EsFuncionario { get; set; }
        
        public void SetAnyoIngresoCuerpo(int anyoIngresoCuerpo) => AnyoIngresoCuerpo = anyoIngresoCuerpo;
        public int GetAnyoIngresoCuerpo() => AnyoIngresoCuerpo;

        public void SetDestinoDefinitivo(bool definitivo) => DestinoDefinitivo = definitivo;
        public bool GetDestinoDefinitivo() => DestinoDefinitivo;

        public iEmpleadoPublico.TipoMedico GetTipoMedico() => this.SeguroMedico;

        public override string ToString()
        {
            throw new NotImplementedException();
        }





        public string GetRangoEdad(int edad)
        {
            if (edad < 40)
                return "joven";
            else if (edad >= 40 && edad < 60)
                return "maduro";
            else
                return "por jubilarse";
        }
    }
}
