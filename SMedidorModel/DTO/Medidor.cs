using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMedidorModel.DTO
{
    public class Medidor
    {
        private int numero;
        private string fecha;
        private double valorConsumo;

        public int Numero { get => numero; set => numero = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public double ValorConsumo { get => valorConsumo; set => valorConsumo = value; }
        public override string ToString()
        {
            return numero + "|" + fecha + "|" + valorConsumo;
        }
    }
}



