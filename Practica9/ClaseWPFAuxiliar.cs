using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IGraficasIES
{
      static class ClaseWPFAuxiliar
    {

        public static void ActivarDesactivarTextBox(Grid grid, bool activar)
        {
            foreach (var control in grid.Children)
            {
                if (control is TextBox textBox)
                {
                    textBox.IsEnabled = activar;
                }
            }
        }

        public static void LimpiarTextBoxs(Grid grid)
        {
            foreach (var control in grid.Children)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
            }
        }

        public static void ActivarBotonesExceptoGuardarCancelar(Grid grid, bool activar)
        {
            foreach (var control in grid.Children)
            {
                if (control is Button button && button.Name != "btnCancelar" && button.Name != "btnGuardar")
                {
                    button.IsEnabled = activar;
                }
            }
        }


        public static bool VerificarTextBoxNoVacios(Grid grid)
        {
            foreach (var control in grid.Children)
            {
                if (control is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Se encontró al menos un TextBox vacío
                    return false;
                }
            }

            // Todos los TextBox están llenos
            return true;
        }


        public static bool EsNumeroValido(string texto)
        {
            // Intenta convertir el texto a un número
            if (int.TryParse(texto, out int resultado))
            {
                // La conversión fue exitosa, es un número válido
                return true;
            }
            else
            {
                // La conversión falló, no es un número válido
                return false;
            }
        }

        public static int EncontrarPersona(this List<Persona> lista, Persona persona)
        {
            int indice = -1;
            Persona personaEncontrada = lista.Find(p => p.Nombre == persona.Nombre && p.Apellidos == persona.Apellidos && p.Apellidos == persona.Apellidos);
            if (personaEncontrada != null)
            {
                indice = lista.IndexOf(personaEncontrada);
            }
          
            return indice;
        }

        private static List<ProfesorExtendido> profesoresExtendidos = ProfesorExtendido.GetProfesores();

        public static void Mayores35(List<ProfesorFuncionario> profesorFuncionarios)
        {

            var mayores = profesorFuncionarios.Select(e => new { Nombre = e.Nombre, Apellido = e.Apellidos, e.Edad, e.Materia }).Where(e => e.Edad > 35);
            MostrarDatos("Mayores 35", mayores);

        }
        public static void AnyoIngresoMas2010(List<ProfesorFuncionario> profesorFuncionarios)
        {
            var consulta = profesorFuncionarios.Select(e => new
            {
                e.Nombre,
                e.Apellidos,
                e.Edad,
                e.Materia,
                e.AnyoIngresoCuerpo,
                TipoFuncionario = e.TipoProfesor.ToString(),
                TipoMedico = e.SeguroMedico.ToString(),
                DestinoDefinitivo = e.DestinoDefinitivo ? "Si" : "No",
                e.RutaFoto
            }).Where(e => e.AnyoIngresoCuerpo > 2010);

            MostrarDatos("filtrarAnoDeIngreso", consulta);
        }
        public static void EstaturaMas160(List<ProfesorFuncionario> profesorFuncionarios)
        {
            var consulta = profesorFuncionarios.Join(profesoresExtendidos, (pf => pf.Id), (pe => pe.Email), (pf, pe) => new
            {
                pf.Nombre,
                pf.Apellidos,
                pf.Edad,
                pe.Estatura,
                pe.Peso
            }).Where(pf => pf.Estatura > 160).OrderBy(pe => pe.Estatura).OrderByDescending(pe => pe.Peso);

            MostrarDatos("estaturaSuperio160", consulta);
        }
        public static void Agrupacion1(List<ProfesorFuncionario> profesorFuncionarios)
        {
            var consulta = profesoresExtendidos.Join(profesorFuncionarios, pe => pe.Email, pf => pf.Id, (pe, pf) => new { pe, pf })
                                                .GroupBy(final => final.pe.EstadoCivilProfesor)
                                                .Select(a => new
                                                {
                                                    Nombre = a.Key,
                                                    Elementos = a.ToList()
                                                });
            string final = "";
            foreach (var grupo in consulta)
            {
                final += ($"Estado Civil: {grupo.Nombre}\n");

                foreach (var elemento in grupo.Elementos)
                {
                    final += ($" Nombre: {elemento.pf.Nombre} \n Email: {elemento.pe.Email} \n Edad: {elemento.pf.Edad}\n Peso: {elemento.pe.Peso} \n Estatura: {elemento.pe.Estatura} \n");
                }
                final += "\n";
            }

            MessageBox.Show(final, "Agrupacion 1", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public static void Agrupacion2(List<ProfesorFuncionario> profesorFuncionarios)
        {
            var consulta = profesoresExtendidos.Join(profesorFuncionarios, pe => pe.Email, pf => pf.Id, (pe, pf) => new { pe, pf })
                                                .GroupBy(final => final.pe.EstadoCivilProfesor)
                                                .Select(a => new
                                                {
                                                    Valor = a.Key,
                                                    cantidad = a.Count()
                                                });

            string final = "";
            foreach (var grupo in consulta)
            {
                final += ($"Estado Civil: {grupo.Valor}\n Cantidad: {grupo.cantidad}");


                final += "\n";
            }

            MessageBox.Show(final, "Agrupacion 2", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public static void Agrupacion3(List<ProfesorFuncionario> profesorFuncionarios)
        {
            var consulta = from pf in profesorFuncionarios
                           orderby pf.Edad
                           group new { Nombre = pf.Nombre, Apellidos = pf.Apellidos, Email = pf.Id } by pf.Edad into personasAgrupadas
                           select new
                           {
                               Key = personasAgrupadas.Key,
                               Valores = personasAgrupadas
                           };

            string final = "";
            foreach (var grupo in consulta)
            {
                final += ($"Edad: {grupo.Key}\n");

                foreach (var elemento in grupo.Valores)
                {
                    final += ($" Nombre: {elemento.Nombre} \n Email: {elemento.Apellidos} \n Edad: {elemento.Email}\n ");
                }
                final += "\n";
            }

            MessageBox.Show(final, "Agrupacion 1", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void Agrupacion4(List<ProfesorFuncionario> profesorFuncionarios)
        {
            var consulta = from pf in profesorFuncionarios
                           join pe in profesoresExtendidos on pf.Id equals pe.Email
                           where pf.Edad >= 40
                           orderby pe.Peso
                           orderby pf.Apellidos
                           group new { Nombre = pf.Nombre, Apellidos = pf.Apellidos, Seguro = pf.SeguroMedico, Peso = pe.Peso } by pf.SeguroMedico into personasAgrupadas
                           select new
                           {
                               Key = personasAgrupadas.Key,
                               Valores = personasAgrupadas
                           };

            string final = "";
            foreach (var grupo in consulta)
            {
                final += ($"Seguro Medico: {grupo.Key}\n");

                foreach (var elemento in grupo.Valores)
                {
                    final += ($" Nombre: {elemento.Nombre} \n Email: {elemento.Apellidos} \n Seguro: {elemento.Seguro}\n Peso: {elemento.Peso}");
                }
                final += "\n";
            }

            MessageBox.Show(final, "Agrupacion 1", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void AnyoIngresoCasado(List<ProfesorFuncionario> profesorFuncionarios)
        {
            var consulta = profesorFuncionarios.Join(profesoresExtendidos, (pf => pf.Id), (pe => pe.Email), ((pf, pe) => new
            {
                pf.Nombre,
                pf.Apellidos,
                Año = pf.AnyoIngresoCuerpo,
                Estado = pe.EstadoCivilProfesor.ToString()
            })).Where(pf => pf.Año > 2010).Where(pe => pe.Estado.Equals(ProfesorExtendido.EstadoCivil.Casado.ToString()));

            MostrarDatos("Filtrar Año de ingreso", consulta);
        }



        public static void MostrarDatos<T>(string cadena, IEnumerable<T> consulta)
        {

            System.Reflection.PropertyInfo[] listaPropiedades = typeof(T).GetProperties();
            string finalTexto = "";
            foreach (var item in consulta)
            {
                foreach (System.Reflection.PropertyInfo propiedad in listaPropiedades)
                {
                    finalTexto += ($"{propiedad.Name}: {propiedad.GetValue(item)} \n");
                }
                finalTexto += "\n";
            }


            MessageBox.Show(finalTexto, cadena, MessageBoxButton.OK, MessageBoxImage.Information);


        }

    }
   

}
