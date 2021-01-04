using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZKSoftwareAPI;

namespace ComunicadorChecador.models
{
    public class RelojZK
    {
        public RelojZK()
        {
            Conectar();
        }


        ZKSoftware dispositivo = new ZKSoftware(Modelo.X628C);

        private bool Conectar()
        {
            if( dispositivo.DispositivoConectar("192.168.0.6", 0, true) )
            {
                return true;
            } else
            {
                return false;
            }


        }

        public void Consultar(NumeroDe numeroDe)
        {
            if( dispositivo.DispositivoConsultar(numeroDe) )
            {
                MessageBox.Show(dispositivo.ResultadoConsulta.ToString());
            } else
            {
                MessageBox.Show(dispositivo.ERROR.ToString());

            }

            dispositivo.DispositivoDesconectar();
        }

    }
}
