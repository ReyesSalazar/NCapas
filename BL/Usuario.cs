using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ML;
using DL_EF;
using System.Data.Entity.Core;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        //public static ML.Result GetAll()
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
        //        {
        //            //string query = "SELECT IdUsuario, UserName, Nombre, ApellidoPaterno, ApellidoMaterno, CodigoPostal, Email, Sexo, Telefono, Celular, FechaNacimiento, CURP FROM Usuario";
        //            //using (SqlCommand cmd = new SqlCommand())
        //            //{
        //               // cmd.CommandText = query;
        //                SqlCommand cmd = new SqlCommand("UsuarioGetAll", context);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Connection = context;

        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    DataTable tableUsuario = new DataTable();

        //                    da.Fill(tableUsuario);

        //                    cmd.Connection.Open();

        //                    if (tableUsuario.Rows.Count > 0)
        //                    {
        //                        result.Objects = new List<object>();
        //                        foreach (DataRow row in tableUsuario.Rows) //
        //                        {
        //                            ML.Usuario usuario = new ML.Usuario();
        //                            usuario.IdUsuario = int.Parse(row[0].ToString());
        //                            usuario.UserName = row[1].ToString();
        //                            usuario.Nombre = row[2].ToString();
        //                            usuario.ApellidoPaterno = row[3].ToString();
        //                            usuario.ApellidoMaterno = row[4].ToString();
        //                            usuario.Email = row[5].ToString();
        //                            usuario.Sexo = row[6].ToString();
        //                            usuario.Telefono = row[7].ToString();
        //                            usuario.Celular = row[8].ToString();
        //                            usuario.FechaNacimiento = row[9].ToString();
        //                            usuario.CURP = row[10].ToString();

        //                            result.Objects.Add(usuario);
        //                        }
        //                        result.Correct = true;
        //                    }
        //                    else
        //                    {
        //                        result.Correct = false;
        //                        result.ErrorMessage = "No se encontraron registros en la tabla Materia";
        //                    }
        //                    //inicializador condición incremento
        //                //}
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;

        //}//Fin GetAll
        //public static ML.Result GetById(int IdUsuario)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
        //        {
        //            //string query = "UsuarioGetById";

        //            //using (SqlCommand cmd = new SqlCommand())
        //            //{
        //            SqlCommand cmd = new SqlCommand("UsuarioGetById", context);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //              //  cmd.CommandText = query;
        //                cmd.Connection = context;
                        
        //                SqlParameter[] collection = new SqlParameter[1];

        //                collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
        //                collection[0].Value = IdUsuario;

        //                cmd.Parameters.AddRange(collection);

        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    DataTable tableUsuario = new DataTable();

        //                    da.Fill(tableUsuario);

        //                    cmd.Connection.Open();

        //                    if (tableUsuario.Rows.Count > 0)
        //                    {
        //                        result.Objects = new List<object>();

        //                        DataRow row = tableUsuario.Rows[0];

        //                        ML.Usuario usuario = new ML.Usuario();

        //                        usuario.IdUsuario = int.Parse(row[0].ToString());
        //                        usuario.UserName = row[1].ToString();
        //                        usuario.Nombre = row[2].ToString();
        //                        usuario.ApellidoPaterno = row[3].ToString();
        //                        usuario.ApellidoMaterno = row[4].ToString();
        //                        usuario.Email = row[5].ToString();
        //                        usuario.Sexo = row[6].ToString();
        //                        usuario.Telefono = row[7].ToString();
        //                        usuario.Celular = row[8].ToString();
        //                        usuario.FechaNacimiento = row[9].ToString();
        //                        usuario.CURP = row[10].ToString();

        //                        result.Object = usuario;  //boxing    --unboxing

        //                        result.Correct = true;
        //                    }
        //                    else
        //                    {
        //                        result.Correct = false;
        //                        result.ErrorMessage = "No se encontraron registros en la tabla Materia";
        //                    }
        //                    //inicializador condición incremento
        //                //}
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}//Fin GetById

        //public static ML.Result Add(ML.Usuario usuario)
        //{
        //    ML.Result result = new ML.Result();           
         
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
        //        {
        //                SqlCommand cmd = new SqlCommand("UsuarioAdd", context);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Connection = context;

        //                SqlParameter[] collection = new SqlParameter[11];

        //                collection[0] = new SqlParameter("UserName", SqlDbType.VarChar);
        //                collection[0].Value = usuario.UserName;

        //                collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
        //                collection[1].Value = usuario.Nombre;

        //                collection[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
        //                collection[2].Value = usuario.ApellidoPaterno;

        //                collection[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
        //                collection[3].Value = usuario.ApellidoMaterno;

        //                collection[4] = new SqlParameter("Email", SqlDbType.VarChar);
        //                collection[4].Value = usuario.Email;

        //                collection[5] = new SqlParameter("Sexo", SqlDbType.VarChar);
        //                collection[5].Value = usuario.Sexo;

        //                collection[6] = new SqlParameter("Telefono", SqlDbType.VarChar);
        //                collection[6].Value = usuario.Telefono;

        //                collection[7] = new SqlParameter("Celular", SqlDbType.VarChar);
        //                collection[7].Value = usuario.Celular;

        //                collection[8] = new SqlParameter("FechaNacimiento", SqlDbType.VarChar);
        //                collection[8].Value = usuario.FechaNacimiento;

        //                collection[9] = new SqlParameter("CURP", SqlDbType.VarChar);
        //                collection[9].Value = usuario.CURP;

        //                collection[10] = new SqlParameter("IdDireccion", SqlDbType.Int);
        //                collection[10].Value = usuario.Direccion.IdDireccion;

        //                cmd.Parameters.AddRange(collection);

        //                cmd.Connection.Open();

        //                int RowsAffected= cmd.ExecuteNonQuery();
        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "Ocurrió un error al momento de insertar el usuario";
        //                }

        //            //}
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}//Fin Add
        //public static ML.Result Update(ML.Usuario usuario)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
        //        {
        //           // using (SqlCommand cmd = new SqlCommand())
        //            //{

        //                //cmd.CommandText = "UPDATE Usuario SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, CodigoPostal = @CodigoPostal WHERE IdUsuario = @IdUsuario ";
        //                SqlCommand cmd = new SqlCommand("UsuarioUpdateSP", context);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Connection = context;

        //                SqlParameter[] collection = new SqlParameter[11];

        //                collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
        //                collection[0].Value = usuario.IdUsuario;

        //                collection[1] = new SqlParameter("UserName", SqlDbType.VarChar);
        //                collection[1].Value = usuario.UserName;

        //                collection[2] = new SqlParameter("Nombre", SqlDbType.VarChar);
        //                collection[2].Value = usuario.Nombre;

        //                collection[3] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
        //                collection[3].Value = usuario.ApellidoPaterno;

        //                collection[4] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
        //                collection[4].Value = usuario.ApellidoMaterno;

        //                collection[5] = new SqlParameter("Email", SqlDbType.VarChar);
        //                collection[5].Value = usuario.Email;

        //                collection[6] = new SqlParameter("Sexo", SqlDbType.VarChar);
        //                collection[6].Value = usuario.Sexo;

        //                collection[7] = new SqlParameter("Telefono", SqlDbType.VarChar);
        //                collection[7].Value = usuario.Telefono;

        //                collection[8] = new SqlParameter("Celular", SqlDbType.VarChar);
        //                collection[8].Value = usuario.Celular;

        //                collection[9] = new SqlParameter("FechaNacimiento", SqlDbType.VarChar);
        //                collection[9].Value = usuario.FechaNacimiento;

        //                collection[10] = new SqlParameter("CURP", SqlDbType.VarChar);
        //                collection[10].Value = usuario.CURP;

        //                cmd.Parameters.AddRange(collection);

        //                cmd.Connection.Open();

        //                int RowsAffected = cmd.ExecuteNonQuery();
        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "Ocurrió un error al momento de actualizar el usuario";
        //                }
        //           // }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}//Fin Update
        //public static ML.Result Delete(ML.Usuario usuario)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
        //        {
        //            //using (SqlCommand cmd = new SqlCommand())
        //           // {

        //                //cmd.CommandText = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario ";
        //                SqlCommand cmd = new SqlCommand("UsuarioDeleteSP", context);
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Connection = context;

        //                SqlParameter[] collection = new SqlParameter[1];

        //                collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
        //                collection[0].Value = usuario.IdUsuario;

        //                cmd.Parameters.AddRange(collection);

        //                cmd.Connection.Open();

        //                int RowsAffected = cmd.ExecuteNonQuery();
        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "Ocurrió un error al momento de eliminar el usuario";
        //                }

        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}//Fin Delete
        public static Result AddEF(ML.Usuario usuario)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Direccion.IdDireccion, usuario.Password, usuario.Rol.IdRol, usuario.Estatus, usuario.Imagen);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }//Fin AddEF
        public static Result UpdateEF(ML.Usuario usuario)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.UsuarioUpdate(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Direccion.IdDireccion, usuario.Password, usuario.Rol.IdRol, usuario.Estatus, usuario.Imagen,usuario.IdUsuario);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }//Fin UpdateEF
        public static Result DeleteEF(int IdUsuario, int IdDireccion)
        {
            Result result = new Result();

            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.UsuarioDelete(IdUsuario);
                    if (query >= 1)
                    {
                        ML.Result queryDireccion = BL.Direccion.DeleteEF(IdDireccion);
                        if (queryDireccion.Correct)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se eliminó el registro";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }//Fin DeleteEF
        public static Result GetByIdEF(int IdUsuario)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.UsuarioGetById(IdUsuario).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.UserName = query.UserName;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.FechaNacimiento = query.FechaNacimiento.Date.ToString("dd/MM/yyyy");
                        usuario.CURP = query.CURP;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion.Value;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.Nombre;
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.Nombre;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.Nombre;
                        usuario.Password = query.Password;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.Estatus = Convert.ToBoolean(query.Estatus);
                        usuario.Imagen = query.Imagen;

                        result.Object = usuario;
                            

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Usuario";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }//Fin GetByIdEF
        public static Result GetAllEF(ML.Usuario usuario)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    if (usuario.Nombre == null)
                    {
                        usuario.Nombre = "";
                    }
                    if (usuario.ApellidoPaterno == null)
                    {
                        usuario.ApellidoPaterno = "";
                    }
                    if (usuario.ApellidoMaterno == null)
                    {
                        usuario.ApellidoMaterno = "";
                    }
                    //var query = context.UsuarioGetAll().ToList();
                    var query = context.UsuarioGetAll(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno);

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuarios = new ML.Usuario();
                            usuarios.IdUsuario = obj.IdUsuario;
                            usuarios.UserName = obj.UserName;
                            usuarios.Nombre = obj.Nombre;
                            usuarios.ApellidoPaterno = obj.ApellidoPaterno;
                            usuarios.ApellidoMaterno = obj.ApellidoMaterno;
                            usuarios.Email = obj.Email;
                            usuarios.Sexo = obj.Sexo;
                            usuarios.Telefono = obj.Telefono;
                            usuarios.Celular = obj.Celular;
                            usuarios.FechaNacimiento = obj.FechaNacimiento.Date.ToString("dd/MM/yyyy");  //Value.ToString("dd/MM/yyyy");
                            usuarios.CURP = obj.CURP;
                            usuarios.Direccion = new ML.Direccion();
                            usuarios.Direccion.IdDireccion = obj.IdDireccion.Value;
                            usuarios.Direccion.Calle = obj.Calle;
                            usuarios.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuarios.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuarios.Direccion.Colonia = new ML.Colonia();
                            usuarios.Direccion.Colonia.Nombre = obj.ColoniaNombre;
                            usuarios.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuarios.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarios.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre;

                            usuarios.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarios.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre;

                            usuarios.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuarios.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre;
                            
                            usuarios.Password = obj.Password;
                            usuarios.Rol = new ML.Rol();
                            usuarios.Rol.Nombre = obj.RolNombre;
                            usuarios.Estatus =  Convert.ToBoolean(obj.Estatus);
                            usuarios.Imagen = obj.Imagen;

                            result.Objects.Add(usuarios);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        } //Fin GetAllEF

//-----------------------------------------------------------------------------------------//
        //public static Result AddLINQ(ML.Usuario usuario)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        using (RSalazarProgramacionNCapasEntities context = new RSalazarProgramacionNCapasEntities())
        //        {
        //            DL_EF.Usuario usuarioDL = new DL_EF.Usuario();

        //            usuarioDL.UserName = usuario.UserName;
        //            usuarioDL.Nombre = usuario.Nombre;
        //            usuarioDL.ApellidoPaterno = usuario.ApellidoPaterno;
        //            usuarioDL.ApellidoMaterno = usuario.ApellidoMaterno;
        //            usuarioDL.Email = usuario.Email;
        //            usuarioDL.Sexo = usuario.Sexo;
        //            usuarioDL.Telefono = usuario.Telefono;
        //            usuarioDL.Celular = usuario.Celular;
        //            usuarioDL.FechaNacimiento = usuario.FechaNacimiento;
        //            usuarioDL.CURP = usuario.CURP;
        //            usuarioDL.IdDireccion = usuario.Direccion.IdDireccion;

        //            context.Usuarios.Add(usuarioDL);
        //            context.SaveChanges();
        //            result.Correct = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}//Fin AddLINQ
        //public static Result UpdateLINQ(ML.Usuario usuarioItem)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        using (RSalazarProgramacionNCapasEntities context = new RSalazarProgramacionNCapasEntities())
        //        {
        //            var UsuarioUpdate = (from usuario in context.Usuarios
        //                                 where usuario.IdUsuario == usuarioItem.IdUsuario
        //                                 select usuario).FirstOrDefault();

        //            if (UsuarioUpdate != null)
        //            {
        //                UsuarioUpdate.UserName = usuarioItem.UserName;
        //                UsuarioUpdate.Nombre = usuarioItem.Nombre;
        //                UsuarioUpdate.ApellidoPaterno = usuarioItem.ApellidoPaterno;
        //                UsuarioUpdate.ApellidoMaterno = usuarioItem.ApellidoMaterno;
        //                UsuarioUpdate.Email = usuarioItem.Email;
        //                UsuarioUpdate.Sexo = usuarioItem.Sexo;
        //                UsuarioUpdate.Telefono = usuarioItem.Telefono;
        //                UsuarioUpdate.Celular = usuarioItem.Celular;
        //                UsuarioUpdate.FechaNacimiento = usuarioItem.FechaNacimiento;
        //                UsuarioUpdate.CURP = usuarioItem.CURP;
        //                UsuarioUpdate.IdDireccion = usuarioItem.Direccion.IdDireccion;

        //                int RowsAffected = context.SaveChanges();

        //                if (RowsAffected >= 1)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                }
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontró el registro en la tabla de Usuario";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}//Fin UpdateLINQ
        //public static Result DeleteLINQ(ML.Usuario usuarioItem)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
        //        {
        //            var query = (from usuario in context.Usuarios
        //                          where usuario.IdUsuario == usuarioItem.IdUsuario
        //                          select usuario).FirstOrDefault();

        //            context.Usuarios.Remove(query);
                    

        //            if (query != null)
        //            {
        //                result.Correct = true;
        //                context.SaveChanges();
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se eliminó el registro";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}//Fin DeleteEF
        //public static Result GetAllLINQ()
        //{
        //    ML.Result result = new ML.Result();
        //    //using
        //    try
        //    {
        //        using (RSalazarProgramacionNCapasEntities context = new RSalazarProgramacionNCapasEntities())
        //        {

        //            var UsuariosList = (from usuario in context.Usuarios //select * from usuario 
        //                                select new
        //                                {
        //                                    usuario.IdUsuario,
        //                                    usuario.UserName,
        //                                    usuario.Nombre,
        //                                    usuario.ApellidoPaterno,
        //                                    usuario.ApellidoMaterno,
        //                                    usuario.Email,
        //                                    usuario.Sexo,
        //                                    usuario.Telefono,
        //                                    usuario.Celular,
        //                                    usuario.FechaNacimiento,
        //                                    usuario.CURP,
        //                                    usuario.IdDireccion
        //                                }
        //                                ).ToList();


        //            if (UsuariosList.Count >= 1)
        //            {
        //                result.Objects = new List<object>();
        //                foreach (var usuario in UsuariosList)
        //                {
        //                    ML.Usuario usuarioItem = new ML.Usuario();

        //                    usuarioItem.IdUsuario = usuario.IdUsuario;
        //                    usuarioItem.UserName = usuario.UserName;
        //                    usuarioItem.Nombre = usuario.Nombre;
        //                    usuarioItem.ApellidoPaterno = usuario.ApellidoPaterno;
        //                    usuarioItem.ApellidoMaterno = usuario.ApellidoMaterno;
        //                    usuarioItem.Email = usuario.Email;
        //                    usuarioItem.Sexo = usuario.Sexo;
        //                    usuarioItem.Telefono = usuario.Telefono;
        //                    usuarioItem.Celular = usuario.Celular;
        //                    usuarioItem.FechaNacimiento = usuario.FechaNacimiento;
        //                    usuarioItem.CURP = usuario.CURP;
        //                    usuarioItem.Direccion = new ML.Direccion();
        //                    usuarioItem.Direccion.IdDireccion = usuario.IdDireccion.Value;

        //                    result.Objects.Add(usuarioItem);
        //                }


        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontraron registros en la tabla de Usuario";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;


        //}//Fin GetAllLINQ
        //public static Result GetByIdLINQ(int IdUsuario)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
        //        {

        //            //var query = from usuario in context.Usuarios 
        //            //            select usuario.IdUsuario ;
        //            var query = (from Usuario in context.Usuarios
        //                         where Usuario.IdUsuario == IdUsuario
        //                         select Usuario).FirstOrDefault();

                    
                   
        //            if (query != null )
        //            {
        //                result.Objects = new List<object>();
        //                    ML.Usuario GetId = new ML.Usuario();
        //                    GetId.IdUsuario = query.IdUsuario;
        //                    //GetId.Nombre = query.Nombre;
        //                    //GetId.UserName = query.UserName;
        //                    //GetId.ApellidoPaterno = query.ApellidoPaterno;
        //                    //GetId.ApellidoMaterno = query.ApellidoMaterno;
        //                    //GetId.Email = query.Email;
        //                    //GetId.Sexo = query.Sexo;
        //                    //GetId.Telefono = query.Telefono;
        //                    //GetId.Celular = query.Celular;
        //                    //GetId.FechaNacimiento = query.FechaNacimiento;
        //                    //GetId.CURP = query.CURP;
        //                    //GetId.Direccion = new ML.Direccion();
        //                    //GetId.Direccion.IdDireccion = query.IdDireccion.Value;

        //                    result.Objects.Add(GetId);
                        
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontraron registros";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}//Fin GetByIdEF

        public static ML.Result ValidarRegistros(List<object> Objects)
        {
            ML.Result result = new Result();
            result.Objects = new List<object>();
            string errorMessage;
            int contador = 1;
            foreach (ML.Usuario usuario in Objects)
            {
                errorMessage = "";
                errorMessage += (usuario.UserName == "") ? "Falta el UserName" : "";
                errorMessage += (usuario.Nombre == "") ? "Falta el nombre" : "";
                errorMessage += (usuario.ApellidoPaterno == "") ? "Falta el Apellido Paterno" : "";
                errorMessage += (usuario.ApellidoMaterno == "") ? "Falta el Apellido Materno" : "";
                errorMessage += (usuario.Email == "") ? "Falta el Email" : "";
                errorMessage += (usuario.Sexo == "") ? "Falta el Sexo" : "";
                errorMessage += (usuario.Telefono == "") ? "Falta el Teléfono" : "";
                errorMessage += (usuario.Celular == "") ? "Falta el Celular" : "";
                errorMessage += (usuario.FechaNacimiento == "") ? "Falta la Fecha de Nacimiento" : "";
                errorMessage += (usuario.CURP == "") ? "Falta el CURP" : "";
                usuario.Direccion = new ML.Direccion();
                errorMessage += (usuario.Direccion.IdDireccion.ToString() == "") ? "Falta el IdDireccion" : "";
                errorMessage += (usuario.Password == "") ? "Falta el Password" : "";
                usuario.Rol = new ML.Rol();
                errorMessage += (usuario.Rol.IdRol.ToString() == "") ? "Falta el IdRol" : "";
                errorMessage += (usuario.Estatus.ToString() == "") ? "Falta Estatus" : "";

                if (errorMessage != "")
                {
                    ML.ErrorExcel error = new ErrorExcel();
                    error.Message = errorMessage;
                    error.IdRegistro = contador;
                    result.Objects.Add(error);
                }
                contador++;
            }
            return result;
        }
        public static ML.Result GetAllByExcel(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.Connection.Open();
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);


                        DataTable tableUsuarios = new DataTable();
                        da.Fill(tableUsuarios);

                        //result.Object = tableUsuario;

                        if (tableUsuarios.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuarios.Rows) //
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Sexo = row[5].ToString();
                                usuario.Telefono = row[6].ToString();
                                usuario.Celular = row[7].ToString();
                                usuario.FechaNacimiento = row[8].ToString();
                                usuario.CURP = row[9].ToString();
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.IdDireccion = Convert.ToInt32(row[10].ToString());
                                usuario.Password = row[11].ToString();
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = Convert.ToInt32(row[12].ToString());
                                usuario.Estatus = Convert.ToBoolean(row[13]);
                                usuario.Imagen = null;

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static Result UserNameGetById(ML.Usuario usuario)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.UserNameGetById(usuario.UserName).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {

                        ML.Usuario usuarios = new ML.Usuario();
                        usuarios.IdUsuario = query.IdUsuario;
                        usuarios.UserName = query.UserName;
                        usuarios.Nombre = query.Nombre;
                        usuarios.ApellidoPaterno = query.ApellidoPaterno;
                        usuarios.ApellidoMaterno = query.ApellidoMaterno;
                        usuarios.Email = query.Email;
                        usuarios.Sexo = query.Sexo;
                        usuarios.Telefono = query.Telefono;
                        usuarios.Celular = query.Celular;
                        usuarios.FechaNacimiento = query.FechaNacimiento.Date.ToString("dd/MM/yyyy");  //Value.ToString("dd/MM/yyyy");
                        usuarios.CURP = query.CURP;
                        usuarios.Direccion = new ML.Direccion();
                        usuarios.Direccion.IdDireccion = query.IdDireccion.Value;
                        usuarios.Password = query.Password;
                        usuarios.Rol = new ML.Rol();
                        usuarios.Rol.IdRol = query.IdRol.Value;
                        usuarios.Estatus = Convert.ToBoolean(query.Estatus);
                        usuarios.Imagen = query.Imagen;

                        result.Object = usuarios;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existe el usuario";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }//Fin GetByIdEF

        //public static ML.Result Login(ML.Usuario usuario)
        //{
        //    ML.Result result = new ML.Result();
        //    //try
        //    //{
        //    using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
        //    {
        //        var usuarios = context.Loggin(usuario.UserName, usuario.Password).FirstOrDefault();

        //        if (usuarios != null)
        //        {
        //            ML.Usuario usuarioitem = new ML.Usuario();

        //            usuarioitem.UserName = usuarios.Username;
        //            usuarioitem.Password = usuarios.Password;

        //            result.Correct = true;
        //        }
        //        else
        //        {
        //            result.Correct = false;
        //        }
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    result.MessageError = ex.Message;
        //        //    result.Correct = false;
        //        //}
        //        return result;
        //    }
        //}
    }
}