using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class Persona
{
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Id { get;  set; }
    public int Edad { get;  set; }
    public string RutaFoto { get;set; }

    public void SetEdad(int value)
    {
        if( value > 0 || value > 120)
        {
            Edad = value;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }
    public string SetNombre(string nombre) => this.Nombre = nombre;
    public string SetApellidos(string apellidos) => this.Apellidos = apellidos;
    public string SetRutaFoto(string foto) => this.RutaFoto = foto;
    public string SetEmail(string email)
    {
        if (email.Contains("trass.com"))
        {
            Id = email;
        }
        else
        {
           Id = CrearEmail(this.Nombre, this.Apellidos);
        }
        return Id;
    }

    public string GetNombre() { return Nombre; }
    public string GetApellidos() { return Apellidos; }
    public string GetFoto() { return RutaFoto; }
    public int GetEdad()
    {
        return Edad;
    }

    public String GetEmail()
    {
        return Id;
    }

    public override string ToString()
    {
        return "nombre: "+Nombre +" apellidos: "+Apellidos+ " edad:  "+Edad+ "email: "+ Id ;
    }

    static string[] DividirApellido(string apellido)
    {
        return apellido.Split(' ');
    }


    public static string CrearEmail(string nombre, string apellido)
    {
        string[] apellidos = DividirApellido(apellido);

        string email = "";
        string dosCaracteresApellido = apellidos[0].Substring(0, Math.Min(2, apellidos[0].Length)); // Dos primeros caracteres del primer apellido
        string dosCaracteresApellido2 = apellidos.Length > 1 ? apellidos[1].Substring(0, Math.Min(2, apellidos[1].Length)) : dosCaracteresApellido; // Dos primeros caracteres del segundo apellido si existe, de lo contrario, usa el primer apellido
        string letraNombre = nombre.Substring(0, 1);
        string dominio = "@trass.com";

        email = dosCaracteresApellido.ToLower() + dosCaracteresApellido2.ToLower() + letraNombre.ToLower() + dominio;

        return email;
    }


    public override bool Equals(object? obj)
    {
        return obj is Persona persona &&
               Nombre == persona.Nombre &&
               Apellidos == persona.Apellidos &&
               Id == persona.Id &&
               Edad == persona.Edad;
    }


    public static Persona operator > (Persona p1, Persona p2)
    {
        if(p1.GetEdad() >  p2.GetEdad()) {
            return p1;
        }
        else
        {
            return p2;
        }
    }

    public static Persona operator < (Persona p1, Persona p2)
    {
        if (p1.GetEdad() < p2.GetEdad())
            return p1;
        else  return p2; 
    }
}
