using MensajeroModel.DAL;
using MensajeroModel.DTO;
using ServidorSocketUtils.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mensajero.Comunicacion
{
    public class HebraServidor
    {
        private IMensajesDAL mensajesDAL = MensajesDALArchivos.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.WriteLine("S: servidor iniciado en puerto {0}", puerto);

            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando cliente.... ");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido");
                    ClienteCom clienteCom = new ClienteCom(cliente);
                    
                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.ejecutar));
                    t.IsBackground = true;
                    t.Start();
      
                }
            }
            else
            {
                Console.WriteLine("FALLO EN LA MATRIZ, ERROR #329-130 no se puede iniciar server en {0}", puerto);
            }
        }
    }
}
