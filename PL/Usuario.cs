using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace PL
{
    public class Usuario
    {
        public static void CargaMasiva()
        {
            string file = @"C:\Users\digis\Documents\Reyes Jose Salazar Meza\RSalazarProgramacionNCapas\PL_MVC\Content\CargaMasiva.txt";
            bool FirstLine = true;

            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            if (File.Exists(file))
            {
                StreamReader Textfile = new StreamReader(file);
                string line;

                while ((line = Textfile.ReadLine()) != null)
                {
                    Console.WriteLine(line);

                    if (FirstLine)
                    {
                        FirstLine = false;
                        Textfile.ReadLine();
                    }
                        Console.WriteLine(line);
                        string[] datos = line.Split('|');

                        ML.Producto producto = new ML.Producto();
                        producto.Nombre = datos[0];
                        producto.PrecioUnitario = Decimal.Parse(datos[1]);
                        producto.Stock = datos[2];
                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = int.Parse(datos[3]);
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = int.Parse(datos[4]);
                        producto.Descripcion = datos[5];

                        ML.Result result = BL.Producto.AddEF(producto);

                        if (!result.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el producto: " + producto.Nombre + "Error: " + result.ErrorMessage);
                        }
                }
                Textfile.Close();
                Console.ReadKey();
            }
            TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\Reyes Jose Salazar Meza\RSalazarProgramacionNCapas\Errores.txt");
            foreach (string error in resultErrores.Objects)
            {
                tw.WriteLine(error);
            }
            tw.Close();
            if (resultErrores.Objects.Count > 0)
            {
                Console.WriteLine("Error al Insertar los registros, consultar el log");
            }
        }
        public static void GetAll()
        {
            ServiceTest.UsuarioClient servicioPrueba = new ServiceTest.UsuarioClient();
            ML.Usuario usuario = new ML.Usuario();

            var resultGetAll = servicioPrueba.GetAll(usuario);

            Console.WriteLine("\n Vista Usuario");
            Console.WriteLine();

            if (resultGetAll.Correct)
            {
                foreach (ML.Usuario usuarios in resultGetAll.Objects)
                {
                    Console.WriteLine("IdUsuario: " + usuarios.IdUsuario);
                    Console.WriteLine("UserName: " + usuarios.UserName);
                    Console.WriteLine("Nombre: " + usuarios.Nombre);
                    Console.WriteLine("Apellido Paterno: " + usuarios.ApellidoPaterno);
                    Console.WriteLine("Apellido Materno: " + usuarios.ApellidoMaterno);
                    Console.WriteLine("Email: " + usuarios.Email);
                    Console.WriteLine("Sexo: " + usuarios.Sexo);
                    Console.WriteLine("Telefono: " + usuarios.Telefono);
                    Console.WriteLine("Celular: " + usuarios.Celular);
                    Console.WriteLine("Fecha de Nacimiento: " + usuarios.FechaNacimiento);
                    Console.WriteLine("CURP: " + usuarios.CURP);

                    Console.WriteLine("--------");
                }
                Console.WriteLine("Los registros fueron recuperados exitosamente" , resultGetAll.Objects);
            }
            else
            {
                Console.WriteLine("Ocurrió un error al traer la información del usuario: " + resultGetAll.ErrorMessage);
            }
        }//Fin GetAll
        public static void GetById()
        {
            ServiceTest.UsuarioClient servicioPrueba = new ServiceTest.UsuarioClient();
            ML.Usuario usuario = new ML.Usuario();

            

            Console.WriteLine("\n Buscar Usuario");
            Console.WriteLine("");
            Console.WriteLine("Ingrese el Id del usuario");
            usuario.IdUsuario = int.Parse(Console.ReadLine());
            var resultGetById = servicioPrueba.GetById(usuario.IdUsuario);
            if (resultGetById.Correct)
            {
                Console.WriteLine("IdUsuario: " + ((ML.Usuario)resultGetById.Object).IdUsuario); //unboxing
                Console.WriteLine("UserName: " + ((ML.Usuario)resultGetById.Object).UserName); //unboxing
                Console.WriteLine("Nombre: " + ((ML.Usuario)resultGetById.Object).Nombre); //unboxing
                Console.WriteLine("ApellidoPaterno: " + ((ML.Usuario)resultGetById.Object).ApellidoPaterno); //unboxing
                Console.WriteLine("ApellidoMaterno: " + ((ML.Usuario)resultGetById.Object).ApellidoMaterno); //unboxing
                Console.WriteLine("Email: " + ((ML.Usuario)resultGetById.Object).Email); //unboxing
                Console.WriteLine("Sexo: " + ((ML.Usuario)resultGetById.Object).Sexo); //unboxing
                Console.WriteLine("Telefono: " + ((ML.Usuario)resultGetById.Object).Telefono); //unboxing
                Console.WriteLine("Celular: " + ((ML.Usuario)resultGetById.Object).Celular); //unboxing
                Console.WriteLine("FechaNacimiento: " + ((ML.Usuario)resultGetById.Object).FechaNacimiento); //unboxing
                Console.WriteLine("CURP: " + ((ML.Usuario)resultGetById.Object).CURP); //unboxing
                Console.WriteLine("--------");

                Console.WriteLine("El registro fue recuperado exitosamente", resultGetById.Object);
            }
            else
            {
                Console.WriteLine("Ocurrió un error al traer la información del usuario: " + resultGetById.ErrorMessage);
            }
        }//Fin GetById
        public static void Add()
        {
            ServiceTest.UsuarioClient servicioPrueba = new ServiceTest.UsuarioClient();
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\n Agregar Usuario");
            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Email del usuario");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingrese la Fecha de Nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el CURP del usuario");
            usuario.CURP = Console.ReadLine();

            Console.WriteLine("Ingresa el ID de la direccion");
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.IdDireccion = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el Password del usuario");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingresa el Rol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Ingrese el estatus del usuario");
            //usuario.Estatus = Convert.ToBoolean(Console.ReadLine().ToString());

            var resultAdd = servicioPrueba.Add(usuario);
                if(resultAdd.Correct)
                {
                    Console.WriteLine("El usuario se agrego correctamente");
                }
                else
                {
                    Console.WriteLine(resultAdd.ErrorMessage);
                }

        }//Fin Add
        public static void Update()
        {
            ServiceTest.UsuarioClient servicioPrueba = new ServiceTest.UsuarioClient();
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\n Actualizar Usuario");
            Console.WriteLine("Ingrese el ID del usuario");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el email del usuario");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingrese el sexo del usuario");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingrese el telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el celular del usuario");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingrese la fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el CURP del usuario");
            usuario.CURP = Console.ReadLine();

             Console.WriteLine("Ingresa el ID de la direccion");
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.IdDireccion = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el Password del usuario");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingresa el Rol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Ingrese el estatus del usuario");
            //usuario.Estatus = Convert.ToBoolean(Console.ReadLine().ToString());

            var resultUpdate = servicioPrueba.Update(usuario);
            if (resultUpdate.Correct)
                {
                    Console.WriteLine("El usuario se actualizo correctamente");
                }
                else
                {
                    Console.WriteLine(resultUpdate.ErrorMessage);
                }
        }//Fin Update
        public static void Delete()
        {
            ServiceTest.UsuarioClient servicioPrueba = new ServiceTest.UsuarioClient();
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\n Eliminar Usuario");
            Console.WriteLine("Ingrese el ID del usuario a eliminar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el ID de la direccion a eliminar");
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.IdDireccion = int.Parse(Console.ReadLine());
           
            var resultDelete = servicioPrueba.Delete(usuario.IdUsuario,usuario.Direccion.IdDireccion);
            
                if(resultDelete.Correct)
                {
                    Console.WriteLine("El usuario se elimino correctamente");
                }
                else
                {
                    Console.WriteLine(resultDelete.ErrorMessage);
                }
        }//Fin Delete
    }
}
 //STORE PROCEDURE
        //public static void GetAll()
        //{
        //    ML.Result result = BL.Usuario.GetAllEF();

        //    Console.WriteLine("\n Vista Usuario");
        //    Console.WriteLine();

        //    if (result.Correct)
        //    {
        //        foreach (ML.Usuario usuario in result.Objects)
        //        {
        //            Console.WriteLine("IdUsuario: " + usuario.IdUsuario);
        //            Console.WriteLine("UserName: " + usuario.UserName);
        //            Console.WriteLine("Nombre: " + usuario.Nombre);
        //            Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
        //            Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
        //            Console.WriteLine("Email: " + usuario.Email);
        //            Console.WriteLine("Sexo: " + usuario.Sexo);
        //            Console.WriteLine("Telefono: " + usuario.Telefono);
        //            Console.WriteLine("Celular: " + usuario.Celular);
        //            Console.WriteLine("Fecha de Nacimiento: " + usuario.FechaNacimiento);
        //            Console.WriteLine("CURP: " + usuario.CURP);

        //            Console.WriteLine("--------");
        //        }
        //        Console.WriteLine(result.Objects);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al traer la información del usuario: " + result.ErrorMessage);
        //    }
        //}//Fin GetAll
        //public static void GetById()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Buscar Usuario");
        //    Console.WriteLine("");
        //    Console.WriteLine("Ingrese el Id del usuario");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.GetByIdEF(usuario.IdUsuario);

        //    if (result.Correct)
        //    {
        //        Console.WriteLine("IdUsuario: " + ((ML.Usuario)result.Object).IdUsuario); //unboxing
        //        Console.WriteLine("UserName: " + ((ML.Usuario)result.Object).UserName); //unboxing
        //        Console.WriteLine("Nombre: " + ((ML.Usuario)result.Object).Nombre); //unboxing
        //        Console.WriteLine("ApellidoPaterno: " + ((ML.Usuario)result.Object).ApellidoPaterno); //unboxing
        //        Console.WriteLine("ApellidoMaterno: " + ((ML.Usuario)result.Object).ApellidoMaterno); //unboxing
        //        Console.WriteLine("Email: " + ((ML.Usuario)result.Object).Email); //unboxing
        //        Console.WriteLine("Sexo: " + ((ML.Usuario)result.Object).Sexo); //unboxing
        //        Console.WriteLine("Telefono: " + ((ML.Usuario)result.Object).Telefono); //unboxing
        //        Console.WriteLine("Celular: " + ((ML.Usuario)result.Object).Celular); //unboxing
        //        Console.WriteLine("FechaNacimiento: " + ((ML.Usuario)result.Object).FechaNacimiento); //unboxing
        //        Console.WriteLine("CURP: " + ((ML.Usuario)result.Object).CURP); //unboxing

        //        Console.WriteLine(result.Object);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al traer la información del usuario: " + result.ErrorMessage);
        //    }
        //}//Fin GetById

        //public static void Add()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Agregar Usuario");
        //    Console.WriteLine("Ingrese el UserName del usuario");
        //    usuario.UserName = Console.ReadLine();

        //    Console.WriteLine("Ingrese el nombre del usuario");
        //    usuario.Nombre = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido paterno del usuario");
        //    usuario.ApellidoPaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido materno del usuario");
        //    usuario.ApellidoMaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Email del usuario");
        //    usuario.Email = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Sexo del usuario");
        //    usuario.Sexo = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Telefono del usuario");
        //    usuario.Telefono = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Celular del usuario");
        //    usuario.Celular = Console.ReadLine();

        //    Console.WriteLine("Ingrese la Fecha de Nacimiento del usuario");
        //    usuario.FechaNacimiento = Console.ReadLine();

        //    Console.WriteLine("Ingrese el CURP del usuario");
        //    usuario.CURP = Console.ReadLine();

        //    Console.WriteLine("\n Agregar Direccion");

        //    Console.WriteLine("Ingresa el nombre de la calle");
        //    usuario.Direccion = new ML.Direccion();
        //    usuario.Direccion.Calle = Console.ReadLine();

        //    Console.WriteLine("Ingresa el Numero Interior de la calle");
        //    usuario.Direccion.NumeroInterior = int.Parse(Console.ReadLine());


        //    Console.WriteLine("Ingresa el Numero Exterior calle");
        //    usuario.Direccion.NumeroExterior = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Ingresa el Id de la Colonia");
        //    usuario.Direccion.Colonia = new ML.Colonia();
        //    usuario.Direccion.Colonia.IdColonia = int.Parse(Console.ReadLine());

        //    ML.Result resultDireccion = BL.Direccion.Add(usuario.Direccion);

        //    if (resultDireccion.Correct)
        //    {
        //        usuario.Direccion.IdDireccion = ((int)resultDireccion.Object); //unboxing de la direccion donde se asigna el ID

        //        ML.Result result = BL.Usuario.AddEF(usuario);
        //        if (result.Correct)
        //        {
        //            Console.WriteLine("El usuario ha sido insertado correctamente");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Ocurrió un error al insertar el usuario " + result.ErrorMessage);
        //            Console.ReadKey();
        //        }
        //    }
        //}//Fin Add
        //public static void Update()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Actualizar Usuario");
        //    Console.WriteLine("Ingrese el ID del usuario");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Ingrese el UserName del usuario");
        //    usuario.UserName = Console.ReadLine();

        //    Console.WriteLine("Ingrese el nombre del usuario");
        //    usuario.Nombre = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido paterno del usuario");
        //    usuario.ApellidoPaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido materno del usuario");
        //    usuario.ApellidoMaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el email del usuario");
        //    usuario.Email = Console.ReadLine();

        //    Console.WriteLine("Ingrese el sexo del usuario");
        //    usuario.Sexo = Console.ReadLine();

        //    Console.WriteLine("Ingrese el telefono del usuario");
        //    usuario.Telefono = Console.ReadLine();

        //    Console.WriteLine("Ingrese el celular del usuario");
        //    usuario.Celular = Console.ReadLine();

        //    Console.WriteLine("Ingrese la fecha de nacimiento del usuario");
        //    usuario.FechaNacimiento = Console.ReadLine();

        //    Console.WriteLine("Ingrese el CURP del usuario");
        //    usuario.CURP = Console.ReadLine();

        //    ML.Result result = BL.Usuario.UpdateEF(usuario);
        //    if (result.Correct)
        //    {
        //        Console.WriteLine("El usuario ha sido actualizado correctamente");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al actualizar el usuario " + result.ErrorMessage);
        //        Console.ReadKey();
        //    }
        //}//Fin Update
        //public static void Delete()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Eliminar Usuario");
        //    Console.WriteLine("Ingrese el ID del usuario a eliminar");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.DeleteEF(usuario);
        //    if (result.Correct)
        //    {
        //        Console.WriteLine("El usuario ha sido eliminado correctamente");
        //        Console.ReadKey();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al eliminar el usuario " + result.ErrorMessage);
        //        Console.ReadKey();

        //    }
        //}//Fin Delete
        //public static void AddEF()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Agregar Usuario");
        //    Console.WriteLine("Ingrese el UserName del usuario");
        //    usuario.UserName = Console.ReadLine();

        //    Console.WriteLine("Ingrese el nombre del usuario");
        //    usuario.Nombre = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido paterno del usuario");
        //    usuario.ApellidoPaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido materno del usuario");
        //    usuario.ApellidoMaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Email del usuario");
        //    usuario.Email = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Sexo del usuario");
        //    usuario.Sexo = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Telefono del usuario");
        //    usuario.Telefono = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Celular del usuario");
        //    usuario.Celular = Console.ReadLine();

        //    Console.WriteLine("Ingrese la Fecha de Nacimiento del usuario");
        //    usuario.FechaNacimiento = Console.ReadLine();

        //    Console.WriteLine("Ingrese el CURP del usuario");
        //    usuario.CURP = Console.ReadLine();

        //    Console.WriteLine("\n Agregar Direccion");

        //    Console.WriteLine("Ingresa el nombre de la calle");
        //    usuario.Direccion = new ML.Direccion();
        //    usuario.Direccion.Calle = Console.ReadLine();

        //    Console.WriteLine("Ingresa el Numero Interior de la calle");
        //    usuario.Direccion.NumeroInterior = Console.ReadLine();


        //    Console.WriteLine("Ingresa el Numero Exterior calle");
        //    usuario.Direccion.NumeroExterior = Console.ReadLine();

        //    Console.WriteLine("Ingresa el Id de la Colonia");
        //    usuario.Direccion.Colonia = new ML.Colonia();
        //    usuario.Direccion.Colonia.IdColonia = int.Parse(Console.ReadLine());

        //    ML.Result resultDireccion = BL.Direccion.Add(usuario.Direccion);

        //    if (resultDireccion.Correct)
        //    {
        //        usuario.Direccion.IdDireccion = ((int)resultDireccion.Object);

        //        ML.Result result = BL.Usuario.AddEF(usuario);
        //        if (result.Correct)
        //        {
        //            Console.WriteLine("El usuario ha sido insertado correctamente");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Ocurrió un error al insertar el usuario " + result.ErrorMessage);
        //            Console.ReadKey();
        //        }
        //    }
        //}//Fin AddEF
        //public static void UpdateEF()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Actualizar Usuario");
        //    Console.WriteLine("Ingrese el ID del usuario");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Ingrese el UserName del usuario");
        //    usuario.UserName = Console.ReadLine();

        //    Console.WriteLine("Ingrese el nombre del usuario");
        //    usuario.Nombre = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido paterno del usuario");
        //    usuario.ApellidoPaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido materno del usuario");
        //    usuario.ApellidoMaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el email del usuario");
        //    usuario.Email = Console.ReadLine();

        //    Console.WriteLine("Ingrese el sexo del usuario");
        //    usuario.Sexo = Console.ReadLine();

        //    Console.WriteLine("Ingrese el telefono del usuario");
        //    usuario.Telefono = Console.ReadLine();

        //    Console.WriteLine("Ingrese el celular del usuario");
        //    usuario.Celular = Console.ReadLine();

        //    Console.WriteLine("Ingrese la fecha de nacimiento del usuario");
        //    usuario.FechaNacimiento = Console.ReadLine();

        //    Console.WriteLine("Ingrese el CURP del usuario");
        //    usuario.CURP = Console.ReadLine();

        //    Console.WriteLine("Ingresa el ID de la direccion");
        //       usuario.Direccion = new ML.Direccion();
        //       usuario.Direccion.IdDireccion = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.UpdateEF(usuario);
        //    if (result.Correct)
        //    {
        //        Console.WriteLine("El usuario ha sido actualizado correctamente");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al actualizar el usuario " + result.ErrorMessage);
        //        Console.ReadKey();
        //    }
        //}//Fin Update
        //public static void DeleteEF(int IdUsuario)
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Eliminar Usuario");
        //    Console.WriteLine("Ingrese el ID del usuario a eliminar");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
        //    if (result.Correct)
        //    {
        //        Console.WriteLine("El usuario ha sido eliminado correctamente");
        //        Console.ReadKey();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al eliminar el usuario " + result.ErrorMessage);
        //        Console.ReadKey();

        //    }
        //}//Fin Delete
        //public static void GetByIdEF()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Buscar Usuario");
        //    Console.WriteLine("");
        //    Console.WriteLine("Ingrese el Id del usuario");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.GetByIdEF(usuario.IdUsuario);

        //    if (result.Correct)
        //    {
        //        Console.WriteLine("IdUsuario: " + ((ML.Usuario)result.Object).IdUsuario); //unboxing
        //        Console.WriteLine("Nombre: " + ((ML.Usuario)result.Object).Nombre); //unboxing
        //        Console.WriteLine("UserName: " + ((ML.Usuario)result.Object).UserName); //unboxing
        //        Console.WriteLine("ApellidoPaterno: " + ((ML.Usuario)result.Object).ApellidoPaterno); //unboxing
        //        Console.WriteLine("ApellidoMaterno: " + ((ML.Usuario)result.Object).ApellidoMaterno); //unboxing
        //        Console.WriteLine("Email: " + ((ML.Usuario)result.Object).Email); //unboxing
        //        Console.WriteLine("Sexo: " + ((ML.Usuario)result.Object).Sexo); //unboxing
        //        Console.WriteLine("Telefono: " + ((ML.Usuario)result.Object).Telefono); //unboxing
        //        Console.WriteLine("Celular: " + ((ML.Usuario)result.Object).Celular); //unboxing
        //        Console.WriteLine("FechaNacimiento: " + ((ML.Usuario)result.Object).FechaNacimiento); //unboxing
        //        Console.WriteLine("CURP: " + ((ML.Usuario)result.Object).CURP); //unboxing
        //        Console.WriteLine("IdDireccion: " + ((ML.Usuario)result.Object).Direccion.IdDireccion); //unboxing

        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al traer la información del usuario: " + result.ErrorMessage);
        //    }
        //}//Fin GetById
        //public static void GetAllEF()
        //{
        //    ML.Result result = BL.Usuario.GetAllEF();

        //    Console.WriteLine("\n Vista Tabla");
        //    Console.WriteLine();

        //    if (result.Correct)
        //    {
        //        foreach (ML.Usuario usuario in result.Objects)
        //        {
        //            Console.WriteLine("IdUsuario: " + usuario.IdUsuario);
        //            Console.WriteLine("UserName: " + usuario.UserName);
        //            Console.WriteLine("Nombre: " + usuario.Nombre);
        //            Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
        //            Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
        //            Console.WriteLine("Email: " + usuario.Email);
        //            Console.WriteLine("Sexo: " + usuario.Sexo);
        //            Console.WriteLine("Telefono: " + usuario.Telefono);
        //            Console.WriteLine("Celular: " + usuario.Celular);
        //            Console.WriteLine("Fecha de Nacimiento: " + usuario.FechaNacimiento);
        //            Console.WriteLine("CURP: " + usuario.CURP);
        //            Console.WriteLine("IdDireccion: " + usuario.Direccion.IdDireccion);
        //            Console.WriteLine("--------");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al traer la información del usuario: " + result.ErrorMessage);
        //    }
        //}//Fin GetAll

//---------------------------------------------------------------//
        //public static void AddLINQ()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Agregar Usuario");
        //    Console.WriteLine("Ingrese el UserName del usuario");
        //    usuario.UserName = Console.ReadLine();

        //    Console.WriteLine("Ingrese el nombre del usuario");
        //    usuario.Nombre = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido paterno del usuario");
        //    usuario.ApellidoPaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido materno del usuario");
        //    usuario.ApellidoMaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Email del usuario");
        //    usuario.Email = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Sexo del usuario");
        //    usuario.Sexo = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Telefono del usuario");
        //    usuario.Telefono = Console.ReadLine();

        //    Console.WriteLine("Ingrese el Celular del usuario");
        //    usuario.Celular = Console.ReadLine();

        //    Console.WriteLine("Ingrese la Fecha de Nacimiento del usuario");
        //    usuario.FechaNacimiento = Console.ReadLine();

        //    Console.WriteLine("Ingrese el CURP del usuario");
        //    usuario.CURP = Console.ReadLine();

        //    Console.WriteLine("\n Agregar Direccion");

        //    Console.WriteLine("Ingresa el nombre de la calle");
        //    usuario.Direccion = new ML.Direccion();
        //    usuario.Direccion.Calle = Console.ReadLine();

        //    Console.WriteLine("Ingresa el Numero Interior de la calle");
        //    usuario.Direccion.NumeroInterior = int.Parse(Console.ReadLine());


        //    Console.WriteLine("Ingresa el Numero Exterior calle");
        //    usuario.Direccion.NumeroExterior = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Ingresa el Id de la Colonia");
        //    usuario.Direccion.Colonia = new ML.Colonia();
        //    usuario.Direccion.Colonia.IdColonia = int.Parse(Console.ReadLine());

        //    ML.Result resultDireccion = BL.Direccion.Add(usuario.Direccion);

        //    if (resultDireccion.Correct)
        //    {
        //        usuario.Direccion.IdDireccion = ((int)resultDireccion.Object);

        //        ML.Result result = BL.Usuario.AddLINQ(usuario);
        //        if (result.Correct)
        //        {
        //            Console.WriteLine("El usuario ha sido insertado correctamente");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Ocurrió un error al insertar el usuario " + result.ErrorMessage);
        //            Console.ReadKey();
        //        }
        //    }
        //}//Fin AddEFLINQ
        //public static void UpdateLINQ()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Actualizar Usuario");
        //    Console.WriteLine("Ingrese el ID del usuario");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Ingrese el UserName del usuario");
        //    usuario.UserName = Console.ReadLine();

        //    Console.WriteLine("Ingrese el nombre del usuario");
        //    usuario.Nombre = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido paterno del usuario");
        //    usuario.ApellidoPaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el apellido materno del usuario");
        //    usuario.ApellidoMaterno = Console.ReadLine();

        //    Console.WriteLine("Ingrese el email del usuario");
        //    usuario.Email = Console.ReadLine();

        //    Console.WriteLine("Ingrese el sexo del usuario");
        //    usuario.Sexo = Console.ReadLine();

        //    Console.WriteLine("Ingrese el telefono del usuario");
        //    usuario.Telefono = Console.ReadLine();

        //    Console.WriteLine("Ingrese el celular del usuario");
        //    usuario.Celular = Console.ReadLine();

        //    Console.WriteLine("Ingrese la fecha de nacimiento del usuario");
        //    usuario.FechaNacimiento = Console.ReadLine();

        //    Console.WriteLine("Ingrese el CURP del usuario");
        //    usuario.CURP = Console.ReadLine();

        //    Console.WriteLine("Ingresa el ID de la direccion");
        //    usuario.Direccion = new ML.Direccion();
        //    usuario.Direccion.IdDireccion = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.UpdateLINQ(usuario);
        //    if (result.Correct)
        //    {
        //        Console.WriteLine("El usuario ha sido actualizado correctamente");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al actualizar el usuario " + result.ErrorMessage);
        //        Console.ReadKey();
        //    }
        //}//Fin UpdateLINQ
        //public static void GetAllLINQ()
        //{
        //    ML.Result result = BL.Usuario.GetAllLINQ();

        //    Console.WriteLine("\n Vista Tabla");
        //    Console.WriteLine();

        //    if (result.Correct)
        //    {
        //        foreach (ML.Usuario usuario in result.Objects)
        //        {
        //            Console.WriteLine("IdUsuario: " + usuario.IdUsuario);
        //            Console.WriteLine("UserName: " + usuario.UserName);
        //            Console.WriteLine("Nombre: " + usuario.Nombre);
        //            Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
        //            Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
        //            Console.WriteLine("Email: " + usuario.Email);
        //            Console.WriteLine("Sexo: " + usuario.Sexo);
        //            Console.WriteLine("Telefono: " + usuario.Telefono);
        //            Console.WriteLine("Celular: " + usuario.Celular);
        //            Console.WriteLine("Fecha de Nacimiento: " + usuario.FechaNacimiento);
        //            Console.WriteLine("CURP: " + usuario.CURP);
        //            Console.WriteLine("IdDireccion: " + usuario.Direccion.IdDireccion);
        //            Console.WriteLine("--------");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al traer la información del usuario: " + result.ErrorMessage);
        //    }
        //}//Fin GetAllLINQ
        //public static void DeleteLINQ()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Eliminar Usuario");
        //    Console.WriteLine("Ingrese el ID del usuario a eliminar");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.DeleteLINQ(usuario);
        //    if (result.Correct)
        //    {
        //        Console.WriteLine("El usuario ha sido eliminado correctamente");
        //        Console.ReadKey();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al eliminar el usuario " + result.ErrorMessage);
        //        Console.ReadKey();

        //    }
        //}//Fin Delete
        //public static void GetByIdLINQ()
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("\n Buscar Usuario");
        //    Console.WriteLine("");
        //    Console.WriteLine("Ingrese el Id del usuario");
        //    usuario.IdUsuario = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Usuario.GetByIdLINQ(usuario.IdUsuario);

        //    if (result.Correct)
        //    {
        //        Console.WriteLine("IdUsuario: " + ((ML.Usuario)result.Object).IdUsuario); //unboxing
        //        Console.WriteLine("Nombre: " + ((ML.Usuario)result.Object).Nombre); //unboxing
        //        Console.WriteLine("UserName: " + ((ML.Usuario)result.Object).UserName); //unboxing
        //        Console.WriteLine("ApellidoPaterno: " + ((ML.Usuario)result.Object).ApellidoPaterno); //unboxing
        //        Console.WriteLine("ApellidoMaterno: " + ((ML.Usuario)result.Object).ApellidoMaterno); //unboxing
        //        Console.WriteLine("Email: " + ((ML.Usuario)result.Object).Email); //unboxing
        //        Console.WriteLine("Sexo: " + ((ML.Usuario)result.Object).Sexo); //unboxing
        //        Console.WriteLine("Telefono: " + ((ML.Usuario)result.Object).Telefono); //unboxing
        //        Console.WriteLine("Celular: " + ((ML.Usuario)result.Object).Celular); //unboxing
        //        Console.WriteLine("FechaNacimiento: " + ((ML.Usuario)result.Object).FechaNacimiento); //unboxing
        //        Console.WriteLine("CURP: " + ((ML.Usuario)result.Object).CURP); //unboxing
        //        Console.WriteLine("IdDireccion: " + ((ML.Usuario)result.Object).Direccion.IdDireccion); //unboxing

        //    }
        //    else
        //    {
        //        Console.WriteLine("Ocurrió un error al traer la información del usuario: " + result.ErrorMessage);
        //    }
        //}//Fin GetByIdLINQ