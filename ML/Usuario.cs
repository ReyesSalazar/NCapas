using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; } //2,000,000,000
        [Required(ErrorMessage = "Ingrese el UserName")] //decoradores
        public string UserName { get; set; }
        [Required(ErrorMessage = "Ingrese el Nombre")] //decoradores
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el Apellido Paterno")] //decoradores
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "Ingrese el Apellido Materno")] //decoradores
        public string ApellidoMaterno { get; set; } //{0..255}
        [EmailAddress] //Expresiones regulares 
        public string Email { get; set; } 
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        [Required(ErrorMessage = "Ingrese la Fecha de Nacimiento")] //decoradores
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; } 
        public string CURP { get; set; }
        public ML.Direccion Direccion { get; set; }
        public List<object> Usuarios { get; set; }
        [Required(ErrorMessage = "Ingrese el Password")] //decoradores
        public string Password { get; set; }
        public ML.Rol Rol { get; set; }
        public bool Estatus { get; set; }
        public byte[] Imagen { get; set; }
        public string Token { get; set; }
    }
}
