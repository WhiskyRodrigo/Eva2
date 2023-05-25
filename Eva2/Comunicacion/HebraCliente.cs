using MensajeroModel.DAL;
using MensajeroModel.DTO;
using ServidorSocketUtils.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva2.Comunicacion
{
   public class HebraCliente
    {
        private ClienteCom clienteCom;
        private IMensajesDAL mensajesDAL = MensajesDALArchivos.GetInstancia();
        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }
        public void ejecutar()
        {
            //Ahora traemos el codigo que atiende al cliente
            clienteCom.Escribir("Ingrese nombre: ");
            string nombre = clienteCom.Leer();
            clienteCom.Escribir("Ingrese texto: ");
            string texto = clienteCom.Leer();
            Mensaje mensaje = new Mensaje()
            {
                Nombre = nombre,
                Texto = texto,
                Tipo = "TCP"
            };
            lock (mensajesDAL) 
            {        
            mensajesDAL.AgregarMensaje(mensaje);
            }
            clienteCom.Desconectar();
        }
    }
}
