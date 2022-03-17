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
namespace BL
{
    public class Direccion
    {
        public static Result Add(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DireccionAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[5];

                        collection[0] = new SqlParameter("Calle", SqlDbType.VarChar);
                        collection[0].Value = direccion.Calle;

                        collection[1] = new SqlParameter("NumeroExterior", SqlDbType.VarChar);
                        collection[1].Value = direccion.NumeroExterior;

                        collection[2] = new SqlParameter("NumeroInterior", SqlDbType.VarChar);
                        collection[2].Value = direccion.NumeroInterior;

                        collection[3] = new SqlParameter("IdColonia", SqlDbType.Int);
                        collection[3].Value = direccion.Colonia.IdColonia;

                        collection[4] = new SqlParameter("IdDireccion", SqlDbType.Int);
                        collection[4].Direction = ParameterDirection.Output;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        direccion.IdDireccion = int.Parse((collection[4].Value).ToString());

                        if (RowsAffected > 0)
                        {
                            result.Object = direccion.IdDireccion;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de insertar la direccion";
                        }
                        cmd.Connection.Close();
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
        }

        //public static Result AddEF(ML.Direccion direccion)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
        //        {
        //            var query = context.DireccionAdd(direccion.Calle, direccion.NumeroInterior, direccion.NumeroExterior, direccion.Colonia.IdColonia, ParameterDirection.Output);

        //            if (query >= 1)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se insertó el registro";
        //            }

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
        //}//Fin AddEF
        public static Result GetByIdEF(int IdColonia)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.DireccionGetByIdColonia(IdColonia).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {

                        ML.Direccion direccion = new ML.Direccion();
                        direccion.IdDireccion = query.IdDireccion;
                        direccion.Calle = query.Calle;
                        direccion.NumeroInterior = query.NumeroInterior;
                        direccion.NumeroExterior = query.NumeroExterior;
                        direccion.Colonia = new ML.Colonia();
                        direccion.Colonia.IdColonia = query.IdColonia.Value;

                        result.Object = direccion;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Direccion";
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

        public static Result DeleteEF(int IdDireccion)
        {
            Result result = new Result();

            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.DireccionDelete(IdDireccion);
                    if (query >= 1)
                    {
                        result.Correct = true;
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
    }
}