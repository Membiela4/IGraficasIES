using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace IGraficasIES
{
    class ProfesorExtendido
    {
        public enum EstadoCivil
        {
            Soltero,
            Casado,
            Divorciado,
            Viudo
        }

        public int Id { get; set; }
        public string ProfesorFuncionarioId { get; set; }
        public string Email { get; set; }
        public int Peso { get; set; }
        public int Estatura { get; set; }
        public EstadoCivil EstadoCivilProfesor { get; set; }

       
        public String GetEmail()
        {
            return Email;
        }
        
        public int GetPeso() {  return Peso; }
        public int GetEstatura() {  return Estatura; }


        public static List<ProfesorExtendido> GetProfesores()
        {
            // Lista de profesores
            var lista = new List<ProfesorExtendido>();

            // Añade a la lista varios profesores y devuelve la lista
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Divorciado, Email = "gadus@trass.com", Peso = 75, Estatura = 180 });
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Soltero, Email = "rirol@trass.com", Peso = 80, Estatura = 170 });
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Casado, Email = "gujim@trass.com", Peso = 70, Estatura = 185 });
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Casado, Email = "maduj@trass.com", Peso = 65, Estatura = 175 });
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Divorciado, Email = "cfl@trass.com", Peso = 70, Estatura = 190 });
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Casado, Email = "lts@trass.com", Peso = 75, Estatura = 175 });
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Viudo, Email = "psm@trass.com", Peso = 85, Estatura = 160 });
            lista.Add(new ProfesorExtendido() { EstadoCivilProfesor = EstadoCivil.Soltero, Email = "mesua@trass.com", Peso = 75, Estatura = 180 });

            return lista;
        }





    }
}
