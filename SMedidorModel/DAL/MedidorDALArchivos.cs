using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMedidorModel.DTO;

namespace SMedidorModel.DAL
{
    public class MedidorDALArchivos : IMedidorDAL
    {
        //Implementar Singleton: Ayuda a trabajar con las hebras y permite ir implementandolas, requiere constructor atributos y metodos para la instancia
        //1. Constructor tiene que ser private
        private MedidorDALArchivos() { }

        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static MedidorDALArchivos instancia;

        //3. Tener un metodo getInstance, que devuelva una referencia al atributo
        public static IMedidorDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALArchivos();
            }
            return instancia;
        }
        //como vamos a hacer para que 2 hebras no accedan a la vez a este archivo?


        private static string url = Directory.GetCurrentDirectory(); //trae la ruta del proyecto
        private static string archivo = url + "/lecturas.txt"; // concatena los datos 

        public void AgregarMensaje(Medidor medidor)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(archivo, true))
                {
                    write.WriteLine(medidor.NroMedidor + " ; "+ medidor.FechaM +" ; " + medidor.ValorConsumo);
                    write.Flush();
                }
            }
            catch(Exception ex) 
            {

            }
        }

        public List<Medidor> ObtenerMensajes()
        {
            List<Medidor> lista = new List<Medidor>();
            try
            {
                using (StreamReader read  = new StreamReader(archivo, true))
                {
                    string texto = "";
                    do
                    {
                        texto = read.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            Medidor mensaje = new Medidor()
                            {
                                NroMedidor = Convert.ToInt32(arr[0]),
                                FechaM = arr[1],
                                ValorConsumo = Convert.ToInt32(arr[2])
                            };
                            lista.Add(mensaje);
                        }
                    } while (texto != null);
                }

            }catch(Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
