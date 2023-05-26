using SMedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMedidorModel.DAL
{
    public interface IMedidorDAL
    {
        void AgregarMedidor(Medidor medidor);
        List<Medidor> ObtenerMedidor();

    }
}
