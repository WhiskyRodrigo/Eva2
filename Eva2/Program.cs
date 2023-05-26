using Eva2.Comunicacion;
using SMedidorModel.DAL;
using SMedidorModel.DTO;
using ServidorSocketUtils.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Eva2
{
    public class Program
    {
        private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();
        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Seleccione una opcion");
            Console.WriteLine("1. Ingresar \n 2.Mostrar \n 0.Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar(); //...
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;

        }
        static void IniciarServidor()
        {

        }
        static void Main(string[] args)
        {
            //1. Iniciar el servidor Socket en el puerto 3000
            //2. El puerto tiene que ser configurable el App.Config
            //3. Cuando reciba un cliente, tiene que solicitar a ese cliente el nombre, texto y agregar el mensaje con el tipo TCO
            //
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Ingresa el puerto: (pulsa enter para usar por defecto el puerto {0})", puerto);
            int puertoIngresado;
            try
            {
                puertoIngresado = Convert.ToInt32(Console.ReadLine().Trim());
            }
            catch (Exception)
            {
                puertoIngresado = puerto;
            }
            HebraServidor hebra = new HebraServidor(puertoIngresado);
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            //1. ¿como atender mas de un cliente a las vez?
            //2. ¿como evito que dos clientes ingresen al archivo a la vez?
            //3. ¿como evitar el bloqueo mutuo?
            while (Menu());
        }
    }
        static void Ingresar()
        {
            Console.WriteLine("Ingrese numero de Medidor: ");
            Int32 nroMedidor = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Ingrese Fecha: ");
            string fecha = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese Fecha: ");
            decimal valorConsumo = Convert.ToDecimal(Console.ReadLine().Trim());
            Medidor medidor = new Medidor()
            {
                NroMedidor = nroMedidor,
                FechaM = fecha,
                ValorConsumo = valorConsumo
            };
            lock (medidorDAL)
            {
                medidorDAL.AgregarMedidor(medidor);
            }
        }
        static void Mostrar()
        {
            List<Medidor> medidor = null;
            lock (medidorDAL)
            {
                medidor = medidorDAL.ObtenerMedidor();
            }
            foreach (Medidor mensaje in medidor)
            {
                Console.WriteLine(medidor);
            }
        }
    }
}
