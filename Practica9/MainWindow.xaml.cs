using System;
using System.Collections.Generic;
using System.Drawing.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

//ERROR SI EL PROFESOR CARGADO TIENE CAMPO NULL CRASH


namespace IGraficasIES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string rutaFija = "\\Practica9\\Practica9\\img\\profesores\\";
        const string rutaImg = "\\Practica9\\Practica9\\img\\";
        string rutaProfesor;
        List<ProfesorFuncionario> profesorFuncionarioList;
        List<ProfesorExtendido> listProfesoresExtendidos;
        private bool modoCreacion = true;

        public MainWindow()
        {
            InitializeComponent();
            for (int edad = 22; edad <= 70; edad++)
            {
                comboEdad.Items.Add(edad);
            }
            int index = -1;
            txtNombre.TextChanged += txtNombre_TextChanged;
            txtEmail.IsEnabled = false;

            // Suscribir al evento LostFocus de txtApellidos
            txtApellidos.LostFocus += txtApellidos_LostFocus;

            listSMedico.Items.Add("Seguridad Social");
            listSMedico.Items.Add("MUFACE");
            //METER EN METODO

            CargarImagenes();

            GridCentral.IsEnabled = false;

            DeshabilitarBotones(false);
            btnAgregar.IsEnabled = true;
            menuFiltros.IsEnabled = false;
            menuAgrupacion.IsEnabled = false;


            
        }

        private void DeshabilitarBotones(bool valor)
        {
            btnAnterior.IsEnabled = valor;
            btnPrimero.IsEnabled = valor;
            btnUltimo.IsEnabled = valor;
            btnSiguiente.IsEnabled = valor;
            btnActualizar.IsEnabled = valor;
            btnCancelar.IsEnabled = valor;
            btnGuardar.IsEnabled = valor;
            btnBorrar.IsEnabled = valor;

        }

        private void DeshabilitarBotonesControl(bool valor)
        {
            btnAnterior.IsEnabled = valor;
            btnPrimero.IsEnabled = valor;
            btnUltimo.IsEnabled = valor;
            btnSiguiente.IsEnabled = valor;
        }


        private void CargarImagenes()
        {
            imgAnterior.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "flecha-izquierda.png") as ImageSource;
            imgPrimero.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "imgPrimero.png") as ImageSource;
            imgSiguiente.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "flecha-correcta.png") as ImageSource;
            imgUltimo.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "saltear.png") as ImageSource;
            imgActualizar.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "actualizar.png") as ImageSource;
            imgAgregar.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "agregar-usuario.png") as ImageSource;
            imgBorrar.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "quitar-usuario.png") as ImageSource;
            imgCancelar.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "boton-x.png") as ImageSource;
            imgGuardar.Source = (new ImageSourceConverter()).ConvertFromString(rutaImg + "disco-flexible.png") as ImageSource;
        }



        private void mayores35_checked(object sender, RoutedEventArgs e)
        {
            ClaseWPFAuxiliar.Mayores35(profesorFuncionarioList);
        }

        private void anyosuperior2010_checked(object sender, RoutedEventArgs e)
        {
            ClaseWPFAuxiliar.AnyoIngresoMas2010(profesorFuncionarioList);
        }

        private void anyosuperior2010casado_checked(object sender, RoutedEventArgs e)
        {
           ClaseWPFAuxiliar.AnyoIngresoCasado(profesorFuncionarioList);    


        }

        private void estaturasmas160_checked(object sender, RoutedEventArgs e)
        {
            ClaseWPFAuxiliar.EstaturaMas160(profesorFuncionarioList);

        }

        private void agrupacion_checked(object sender, RoutedEventArgs e)
        {
            ClaseWPFAuxiliar.Agrupacion1(profesorFuncionarioList);
        }

        private void cursiva_checked(object sender, RoutedEventArgs e)
        {
            TextoCursiva();
        }

        private void negrita_checked(object sender, RoutedEventArgs e)
        {
            TextoNegrita();

        }

        private void TextoNegrita()
        {
            lblNombre.FontWeight = FontWeights.Bold;
            lblApellidos.FontWeight = FontWeights.Bold;
            lblEdad.FontWeight = FontWeights.Bold;
            lblSMedico.FontWeight = FontWeights.Bold;
            lblEmail.FontWeight = FontWeights.Bold;
            lblAnyoIngreso.FontWeight = FontWeights.Bold;
            rdbCarrera.FontWeight = FontWeights.Bold;
            rdbPracticas.FontWeight = FontWeights.Bold;
            checkDestDef.FontWeight = FontWeights.Bold;
            txtNombre.FontWeight = FontWeights.Bold;
            txtApellidos.FontWeight = FontWeights.Bold;
            txtEmail.FontWeight = FontWeights.Bold;
            txtAnyoIngreso.FontWeight = FontWeights.Bold;

        }

        private void TextoCursiva()
        {
            lblNombre.FontStyle = FontStyles.Italic;
            lblApellidos.FontStyle = FontStyles.Italic;
            lblEdad.FontStyle = FontStyles.Italic;
            lblEmail.FontStyle = FontStyles.Italic;
            lblAnyoIngreso.FontStyle = FontStyles.Italic;
            lblSMedico.FontStyle = FontStyles.Italic;
            rdbCarrera.FontStyle = FontStyles.Italic;
            rdbPracticas.FontStyle = FontStyles.Italic;
            checkDestDef.FontStyle = FontStyles.Italic;
            txtNombre.FontStyle = FontStyles.Italic;
            txtApellidos.FontStyle = FontStyles.Italic;
            txtEmail.FontStyle = FontStyles.Italic;
            txtAnyoIngreso.FontStyle = FontStyles.Italic;

        }


        private void menuabrir_click(object sender, RoutedEventArgs e)
        {
            profesorFuncionarioList = new List<ProfesorFuncionario>();

            using (MiContexto context = new MiContexto())
            {
                //añadimos la lista a la tabla y guardamos los cambios
                profesorFuncionarioList = context.ProfesorFuncionario.ToList();
                context.SaveChanges();


            }


            if (!profesorFuncionarioList.Any())
            {


                OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "txt files (*.txt) | *.txt |All files(*.*)|*.*";



                if (openFileDialog.ShowDialog() == true)
                {
                    if (File.Exists(openFileDialog.FileName))
                    {
                        try
                        {
                            var lineas = File.ReadLines(openFileDialog.FileName);

                            // Verifica si la base de datos aún no tiene ningún registro

                           

                            if (!profesorFuncionarioList.Any())
                            {
                                // Caso 1: La base de datos está vacía
                                foreach (var line in lineas)
                                {
                                    try
                                    {
                                        string[] datos = line.Split(";");

                                        // Verifica si hay minimo mas de un dato introducido
                                        if (datos.Length > 0)
                                        {
                                            ProfesorFuncionario pf = new ProfesorFuncionario();
                                            // Cargar datos del fichero en un profesor funcionario
                                            pf.SetNombre(datos[0]);
                                            pf.SetApellidos(datos[1]);
                                            pf.SetEdad(int.Parse(datos[2]));
                                            pf.SetEmail(datos[3]);
                                            pf.SetMateria(datos[4]);

                                            if (datos[5] == "De carrera")
                                                pf.TipoProfesor = Profesor.TipoFuncionario.DeCarrera;
                                            else
                                                pf.TipoProfesor = Profesor.TipoFuncionario.EnPracticas;

                                            pf.SetAnyoIngresoCuerpo(int.Parse(datos[6]));

                                            if (datos[7] == "false")
                                                pf.SetDestinoDefinitivo(false);
                                            else
                                                pf.SetDestinoDefinitivo(true);

                                            if (datos[8] == "SS")
                                                pf.SeguroMedico = iEmpleadoPublico.TipoMedico.SeguridadSocial;
                                            else
                                                pf.SeguroMedico = iEmpleadoPublico.TipoMedico.Muface;

                                            pf.SetRutaFoto(datos[9]);

                                            rutaProfesor = datos[9];

                                            // Añadir a la lista el profesor
                                            profesorFuncionarioList.Add(pf);
                                        }

                                    }
                                    catch (Exception exLinea)
                                    {
                                        MessageBox.Show($"Error al procesar línea: {exLinea.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                                    }
                                }

                                // Insertar en la base de datos todos los registros correspondientes a ProfesorFuncionario

                                using (MiContexto context = new MiContexto())
                                {
                                    //añadimos la lista a la tabla y guardamos los cambios
                                    context.ProfesorFuncionario.AddRange(profesorFuncionarioList);
                                    context.SaveChanges();


                                }


                                // Cargar el primer registro en el formulario
                                if (profesorFuncionarioList.Any())
                                {
                                    CargarDatos(profesorFuncionarioList[0]);
                                    ClaseWPFAuxiliar.ActivarBotonesExceptoGuardarCancelar(GridBotonesAvance, true);
                                    btnPrimero.IsEnabled = false;
                                    btnAnterior.IsEnabled = false;
                                    menuAgrupacion.IsEnabled = true;
                                    menuFiltros.IsEnabled = true;
                                }
                            }



                            else
                            {
                                // Caso 2: La base de datos contiene datos


                                // Leer de la base de datos los registros
                                using (MiContexto context = new MiContexto())
                                {
                                    //añadimos la lista a la tabla y guardamos los cambios
                                    context.ProfesorFuncionario.ToList();


                                }


                                // Cargar el primer registro de ProfesorFuncionario en el formulario
                                if (profesorFuncionarioList.Any())
                                {
                                    CargarDatos(profesorFuncionarioList[0]);
                                }
                            }

                            // Insertar en la base de datos todos los registros correspondientes a ProfesorExtendido
                            using (MiContexto context = new MiContexto())
                            {
                                List<ProfesorExtendido> profesorExtendidos = new List<ProfesorExtendido>();
                                profesorExtendidos = ProfesorExtendido.GetProfesores();
                                context.ProfesorExtendido.AddRange(profesorExtendidos);
                            }

                            // Añadir a la lista de ProfesorExtendido
                            listProfesoresExtendidos = ProfesorExtendido.GetProfesores();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        finally
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("El archivo no existe", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Hay registros en la base de datos", "INFO", MessageBoxButton.OK) ;
                CargarDatos(profesorFuncionarioList[0]);
                ClaseWPFAuxiliar.ActivarBotonesExceptoGuardarCancelar(GridBotonesAvance, true);
                btnPrimero.IsEnabled = false;
                btnAnterior.IsEnabled = false;
                menuAgrupacion.IsEnabled = true;
                menuFiltros.IsEnabled = true;
            }
        }
        private void CargarDatos(ProfesorFuncionario pf)
        {

            txtNombre.Text = pf.GetNombre();
            txtApellidos.Text = pf.GetApellidos();
            txtEmail.Text = pf.GetEmail();
            txtAnyoIngreso.Text = pf.GetAnyoIngresoCuerpo().ToString();
            comboEdad.SelectedItem = pf.GetEdad();
            checkDestDef.IsChecked = pf.GetDestinoDefinitivo();
            if (pf.SeguroMedico == iEmpleadoPublico.TipoMedico.SeguridadSocial)
            {
                listSMedico.SelectedIndex = 0;
            }
            else
            {
                listSMedico.SelectedIndex = 1;
            }
            listSMedico.SelectedValue = pf.SeguroMedico.ToString();
            if (pf.TipoProfesor == (Profesor.TipoFuncionario)1)
            {
                rdbPracticas.IsChecked = true;
            }
            else
            {
                rdbCarrera.IsChecked = true;
            }
            if (string.IsNullOrEmpty(pf.RutaFoto))
            {
                rutaProfesor = "no_disponible.png";
                imgFotoProfesor.Source = (new ImageSourceConverter()).ConvertFromString(rutaFija + rutaProfesor) as ImageSource;
            }
            else
            {
                rutaProfesor = pf.GetFoto();
                imgFotoProfesor.Source = (new ImageSourceConverter()).ConvertFromString(rutaFija + rutaProfesor) as ImageSource;
            }



        }


        private void menusalir_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void agrupacion1_checked(object sender, RoutedEventArgs e)
        {

            ClaseWPFAuxiliar.Agrupacion1(profesorFuncionarioList);
        }

        private void agrupacion2_checked(object sender, RoutedEventArgs e)
        {
            ClaseWPFAuxiliar.Agrupacion2(profesorFuncionarioList);
        }



        private void agrupacion3_cheched(object sender, RoutedEventArgs e)
        {
            ClaseWPFAuxiliar.Agrupacion3(profesorFuncionarioList);
        }
        private void agrupacion4_checked(object sender, RoutedEventArgs e)
        {
            ClaseWPFAuxiliar.Agrupacion4(profesorFuncionarioList);
        }
        int index = 0;
        private void btnSiguiente_action(object sender, RoutedEventArgs e)
        {
            if (index < profesorFuncionarioList.Count - 1)
            {
                index++;
                CargarDatos(profesorFuncionarioList[index]);
                UpdateNavigationButtons();
            }
        }




        private void btnFirst_action(object sender, RoutedEventArgs e)
        {
            if (profesorFuncionarioList.Count > 0)
            {
                index = 0;
                CargarDatos(profesorFuncionarioList[index]);
                UpdateNavigationButtons();
            }
        }

        private void btnAnterior_action(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                CargarDatos(profesorFuncionarioList[index]);
                UpdateNavigationButtons();
            }
        }

        private void UpdateNavigationButtons()
        {
            btnAnterior.IsEnabled = index > 0;
            btnSiguiente.IsEnabled = index < profesorFuncionarioList.Count - 1;
            btnPrimero.IsEnabled = index > 0;
            btnUltimo.IsEnabled = index < profesorFuncionarioList.Count - 1;
        }


        private void btnLast_action(object sender, RoutedEventArgs e)
        {

            if (profesorFuncionarioList.Count > 0)
            {
                index = profesorFuncionarioList.Count - 1;
                CargarDatos(profesorFuncionarioList[index]);
                UpdateNavigationButtons();
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            modoCreacion = true;
            DeshabilitarBotones(false);
            btnCancelar.IsEnabled=true;
            btnGuardar.IsEnabled=true;
            btnAgregar.IsEnabled=false;
            imgFotoProfesor.Visibility = Visibility.Collapsed;
            GridCentral.IsEnabled = true;
            txtRutaFoto.Visibility = Visibility.Visible;
            lblRutaFoto.Visibility=Visibility.Visible;

            ClaseWPFAuxiliar.ActivarDesactivarTextBox(GridCentral, true);
            ClaseWPFAuxiliar.LimpiarTextBoxs(GridCentral);


        }

        private ProfesorFuncionario CrearProfesorDesdeTextBoxes()
        {
            ProfesorFuncionario profesor = new ProfesorFuncionario();

            profesor.SetNombre(txtNombre.Text);
            profesor.SetApellidos(txtApellidos.Text);
            profesor.SetAnyoIngresoCuerpo(Convert.ToInt32(txtAnyoIngreso.Text));

           
           
            profesor.SetEdad(Convert.ToInt32(comboEdad.SelectedItem));


            profesor.SetEmail(txtEmail.Text);
            

            // Configuración de las propiedades adicionales
            profesor.TipoProfesor = rdbCarrera.IsChecked == true ? Profesor.TipoFuncionario.DeCarrera : Profesor.TipoFuncionario.EnPracticas;
            profesor.SetDestinoDefinitivo(checkDestDef.IsChecked == true);

            // Lógica para obtener el valor del seguro médico
            if (listSMedico.SelectedValue != null)
            {
                profesor.SeguroMedico = listSMedico.SelectedValue.ToString() == "SS" ?
                    iEmpleadoPublico.TipoMedico.SeguridadSocial : iEmpleadoPublico.TipoMedico.Muface;
            }

            profesor.SetRutaFoto(txtRutaFoto.Text);

            return profesor;
        }

        private void txtApellidos_LostFocus(object sender, RoutedEventArgs e)
        {
            GenerarEmailSiNecesario();
        }

        private void GenerarEmailSiNecesario()
        {
            string nombre = txtNombre.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();

            // Verificar si ambos campos tienen valores
            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellidos))
            {
                string email = Persona.CrearEmail(nombre, apellidos);
                txtEmail.Text = email;
            }
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            GenerarEmailSiNecesario();
        }


        private void btnActualizar_action(object sender, RoutedEventArgs e)
        {
            DeshabilitarBotonesControl(false);
            modoCreacion = false; //con este boolean hacemos que al guardar no cree un profesor nuevo y actualice el seleccionado

            GridCentral.IsEnabled = true;
            txtAnyoIngreso.IsEnabled = true;
            txtRutaFoto.IsEnabled = true;
            comboEdad.IsEnabled = true;
            rdbCarrera.IsEnabled = true; 
            rdbPracticas.IsEnabled = true;
            checkDestDef.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            imgFotoProfesor.Visibility = Visibility.Collapsed;
            txtRutaFoto.Visibility = Visibility.Visible;
            lblRutaFoto.Visibility= Visibility.Visible;
            txtRutaFoto.IsEnabled= true;
            
            checkDestDef.IsChecked = false;
            txtAnyoIngreso.Clear();
            txtRutaFoto.Clear();
       
        }

        private void ActualizarProfesor(ProfesorFuncionario profesorSeleccionado)
        {
            try
            {
                // Actualizar las propiedades del profesor seleccionado
                profesorSeleccionado.SetAnyoIngresoCuerpo(Convert.ToInt32(txtAnyoIngreso.Text));
                profesorSeleccionado.SetEdad(Convert.ToInt32(comboEdad.SelectedItem));
                profesorSeleccionado.TipoProfesor = rdbCarrera.IsChecked == true ? Profesor.TipoFuncionario.DeCarrera : Profesor.TipoFuncionario.EnPracticas;
                profesorSeleccionado.SetDestinoDefinitivo(checkDestDef.IsChecked == true);

                // Lógica para obtener el valor del seguro médico
                if (listSMedico.SelectedValue != null)
                {
                    profesorSeleccionado.SeguroMedico = listSMedico.SelectedValue.ToString() == "SS" ?
                        iEmpleadoPublico.TipoMedico.SeguridadSocial : iEmpleadoPublico.TipoMedico.Muface;
                }

                profesorSeleccionado.SetRutaFoto(txtRutaFoto.Text);

                using (MiContexto context = new MiContexto())
                { 
                        context.ProfesorFuncionario.Update(profesorSeleccionado);
                        context.SaveChanges();
                    
                }

                MessageBox.Show("Operación de actualización exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Cargar el primer registro después de la actualización
                Cancelar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el profesor: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void btnBorrar_action(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Deseas borrar este elemento?", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                using (MiContexto context = new MiContexto())
                {
                    context.ProfesorFuncionario.Remove(profesorFuncionarioList[index]);
                    context.SaveChanges();
                }
                profesorFuncionarioList.RemoveAt(index);
                MessageBox.Show("Operacion de borrado con exito", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarDatos(profesorFuncionarioList[0]);

            }

        }

        

        private void btnGuardar_action(object sender, RoutedEventArgs e) 
        {


            if(modoCreacion)
            {

                //if (ClaseWPFAuxiliar.VerificarTextBoxNoVacios(GridCentral))
                //{
                ProfesorFuncionario pf = new ProfesorFuncionario();
                pf = CrearProfesorDesdeTextBoxes();

                if (ClaseWPFAuxiliar.EsNumeroValido(txtAnyoIngreso.Text))
                {
                    using (MiContexto context = new MiContexto())
                    {
                        context.ProfesorFuncionario.Add(pf);
                        context.SaveChanges();

                    }
                    profesorFuncionarioList.Add(pf);
                    MessageBox.Show("Operacion de insercion con exito", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Introduce un año de ingreso valido", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }



                Cancelar();

                //}
                //else
                //{
                //MessageBox.Show("Por favor, completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //}

            }
            if(!modoCreacion) { }
            {
                ActualizarProfesor(profesorFuncionarioList[index]);
            }


        }

        private void btnCancelar_action(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Deseas cancelar la operacion?", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
               Cancelar();
            }

        }

        public void Cancelar()
        {

            CargarDatos(profesorFuncionarioList[0]);
            ClaseWPFAuxiliar.ActivarDesactivarTextBox(GridCentral, false);
            DeshabilitarBotones(true);
            btnAgregar.IsEnabled = true;
            lblRutaFoto.Visibility = Visibility.Collapsed;
            txtRutaFoto.Visibility = Visibility.Collapsed;
            imgFotoProfesor.Visibility = Visibility.Visible;
            btnCancelar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
        }

        
    }


}

