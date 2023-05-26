using SMedidorModel.DAL;
using SMedidorModel.DTO;
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
        private IMedidorDAL ImedidorDAL = MedidorDALArchivos.GetInstancia();
        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }
        public void Ejecutar()
        {
            //ahora traemos el codigo que atiende al cliente
            clienteCom.Escribir("Ingrese número de medidor: ");
            Int32 nroMedidor = Convert.ToInt32(clienteCom.Leer());
            clienteCom.Escribir("Ingrese Fecha: ");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese valor de Consumo: ");
            decimal valorConsumo = Convert.ToDecimal(clienteCom.Leer());
            Medidor medidor = new Medidor()
            {
                NroMedidor = nroMedidor,
                Fecha = fecha,
                ValorConsumo = valorConsumo
            };
            lock (ImedidorDAL) 
            {        
            IMedidorDAL.AgregarMedidor(medidor);
            }
            clienteCom.Desconectar();
        }
    }
}
