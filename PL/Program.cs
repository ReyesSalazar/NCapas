using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PL
{
    public class Program
    {
        static void Main(string[] args)
            {
            bool repetir = true;
            while (repetir)
            {
                char a;
                Console.WriteLine("\nSelecciona La letra de la operacion a realizar");
                Console.WriteLine("\na. Mostrar todos los usuarios");
                Console.WriteLine("b. Buscar usuario");
                Console.WriteLine("c. Agregar Usuario ");
                Console.WriteLine("d. Actualizar Usuario");
                Console.WriteLine("e. Eliminar usuario");
                Console.Write("-------------------------");
                Console.WriteLine("\nf. Mostrar todos los Productos");
                Console.WriteLine("g. Buscar producto");
                Console.WriteLine("h. Agregar producto ");
                Console.WriteLine("i. Actualizar producto");
                Console.WriteLine("j. Eliminar producto");
                a = Convert.ToChar(Console.ReadLine());

                switch (Char.ToLower(a))
                {
                    case 'a':
                        PL.Usuario.GetAll();

                        break;
                    case 'b':
                        PL.Usuario.GetById();

                        break;
                    case 'c':
                        PL.Usuario.Add();

                        break;
                    case 'd':
                        PL.Usuario.Update();

                        break;
                    case 'e':
                        PL.Usuario.Delete();

                        break;
                    case 'f':
                        PL.Producto.GetAll();

                        break;
                    case 'g':
                        PL.Producto.GetById();

                        break;
                    case 'h':
                        PL.Producto.Add();

                        break;
                    case 'i':
                        PL.Producto.Update();

                        break;
                    case 'j':
                        PL.Producto.Delete();

                        break;

                    default:
                        Console.WriteLine("\nDebe ingresar una letra valida");
                        repetir = true;
                        break;
                }
            }
           }//Fin Main
    }
}