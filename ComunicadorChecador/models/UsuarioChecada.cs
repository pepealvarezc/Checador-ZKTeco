using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunicadorChecador.models
{
    public class UsuarioChecada
    {
        private MarcajeOperativo a;
        public int NumeroCredencial { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public int Hora { get; set; }
        public int Minuto { get; set; }
        public int Segundo { get; set; }
        public MarcajeOperativo MarcajeOperativo
        {
            get => this.a;
            set => this.a = value;
        }
        public override string ToString() => this.NumeroCredencial.ToString() + "|" + this.Dia.ToString().PadLeft(2, '0') + "/" + this.Mes.ToString().PadLeft(2, '0') + "/" + this.Anio.ToString() + " " + this.Hora.ToString() + ":" + this.Minuto.ToString() + ":" + this.Segundo.ToString().PadLeft(2, '0') + (this.MarcajeOperativo != null ? "|" + this.MarcajeOperativo.ToString() : string.Empty);
    }
}
