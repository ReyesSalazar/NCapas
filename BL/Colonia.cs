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
    public class Colonia
    {
        public static Result GetByIdEF(int IdMunicipio)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                        ML.Colonia colonia = new ML.Colonia();
                        colonia.IdColonia = obj.IdColonia;
                        colonia.Nombre = obj.Nombre;
                        colonia.CodigoPostal = obj.CodigoPostal;
                        colonia.Municipio = new ML.Municipio();
                        colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;

                        result.Objects.Add(colonia);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Colonia";
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
    }
}
