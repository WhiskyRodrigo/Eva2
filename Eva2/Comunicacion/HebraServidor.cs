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

namespace Eva2.Comunicacion
{
    public class HebraServidor
    {
        private IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();
        private int puerto;
        public HebraServidor(int puerto)
        {
            this.puerto = puerto;
        }
        public void Ejecutar()
        {
            ServerSocket servidor = new ServerSocket(this.puerto);
            Console.WriteLine("S: Servidor iniciado en puerto {0}", this.puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando cliente....");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido");
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("FALLO EN LA MATRIZ, ERROR #329-130 ABORTED,{0} no puede ser iniciado", puerto);
            }
        }
    }
}
