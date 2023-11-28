using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IGraficasIES
    {

    abstract class Profesor  : Persona
    {

        public string Materia { get; set; }

        public void SetMateria(string materia)
        {
            Materia = materia;
        }
        public string GetMateria()
        {
            return Materia;
        }

        public enum TipoFuncionario
        {
            
            EnPracticas = 1,
            DeCarrera = 2
        }

        public TipoFuncionario TipoProfesor { get; set; }

        public TipoFuncionario GetTipoFuncionario()
        {
            return TipoProfesor;
        }
        public abstract override string ToString();
    }
}
