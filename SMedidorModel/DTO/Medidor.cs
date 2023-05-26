using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMedidorModel.DTO
{
    public class Medidor
    {
        private int nroMedidor;
        private string Fecha;
        private double valorConsumo;

        public int NroMedidor { get => nroMedidor; set => nroMedidor = value; }
        public string FechaM { get => Fecha; set => Fecha = value; }
        public double ValorConsumo { get => valorConsumo; set => valorConsumo = value; }

    }
}



