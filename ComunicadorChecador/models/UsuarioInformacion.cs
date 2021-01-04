using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComunicadorChecador.models.ZKBio;

namespace ComunicadorChecador.models
{
    public class UsuarioInformacion
    {
        public int NumeroCredencial { get; set; }
        public string Nombre { get; set; }
        public Permiso Permiso { get; set; }
        public string Contrasenia { get; set; }
        public bool Activo { get; set; }
        public List<UsuarioHuella> Huellas { get; set; }
        public override string ToString() => this.NumeroCredencial.ToString() + (this.Nombre == string.Empty ? string.Empty : " - " + this.Nombre);
    }
}
