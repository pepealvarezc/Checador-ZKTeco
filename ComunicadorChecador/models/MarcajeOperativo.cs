using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunicadorChecador.models
{
    public class MarcajeOperativo
    {
        public int NumeroDispositivo { get; set; }
        public int Parametro1 { get; set; }
        public int Parametro2 { get; set; }
        public int Parametro3 { get; set; }
        public int Parametro4 { get; set; }
        public int Operacion { get; set; }
        public override string ToString() => this.NumeroDispositivo.ToString() + "|" + this.Operacion.ToString() + "|" + (object)this.Parametro1 + "|" + this.Parametro2.ToString() + "|" + this.Parametro3.ToString() + "|" + this.Parametro4.ToString();
    }
}
