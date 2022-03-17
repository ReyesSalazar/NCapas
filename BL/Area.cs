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
    public class Area
    {
        public static Result GetAllEF()
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.AreaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = obj.IdArea;
                            area.Nombre = obj.Nombre;


                            result.Objects.Add(area);
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
        //public static Result GetByIdEF(int IdArea)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
        //        {

        //            var query = context.AreaGetById(IdArea).FirstOrDefault();

        //            result.Objects = new List<object>();

        //            if (query != null)
        //            {

        //                ML.Area area = new ML.Area();
        //                area.IdArea = query.IdArea;
        //                area.Nombre = query.Nombre;
                        
        //                result.Object = area;


        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Area";
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
        //}//Fin GetByIdEF
    }
}
