using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zkemkeeper;

namespace ComunicadorChecador.models
{
    public class ZKBio
    {
        private string a;
        private int b;
        private List<UsuarioInformacion> c;
        private UsuarioInformacion d;
        private List<UsuarioChecada> e;
        private UsuarioInformacion f;
        private bool g;
        private int h;
        private CZKEM i;
        private Modelo j;

        #region Enum Basicos
        public enum Estatus
        {
            Habilitar,
            Deshabilitar,
        }
        public enum NumeroDe
        {
            AdministradoresRegistrados = 1,
            UsuariosRegistrados = 2,
            HuellasRegistradas = 3,
            ContraseniasRegistradas = 4,
            RegistrosDeOperativos = 5,
            RegistrosDeAsistencias = 6,
            CapacidadTotalHuellas = 7,
            CapacidadTotalUsuarios = 8,
            CapacidadTotalAsistencias = 9,
            CapacidadActualHuellas = 10,
            CapacidadActualUsuarios = 11,
            CapacidadActualAsistencias = 12,
            FotosRegistradas = 13,
            CapacidadTotalFotos = 14
        }
        public enum Modelo
        {
            X628C,
            RelojGenerico
        }
        public enum Permiso
        {
            UsuarioNormal,
            UsuarioEnrolador,
            UsuarioSupervisor,
            UsuarioAdministrador,
        }
        #endregion
        #region Funciones relacionadas reloj
        public bool Beep()
        {
            this.a = string.Empty;
            this.g = false;
            if (this.i.Beep(100))
            {
                this.g = true;
            }
            else
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: Beep) - Error al enviar Beep. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            }
            return this.g;
        }
        private string GenerarMensajeDeError(int A_0)
        {
            this.a = string.Empty;
            switch (A_0)
            {
                case -100:
                    this.a = "Operación fallida o el dato no existe.";
                    break;
                case -10:
                    this.a = "La longitud del dato transmitido no es correcto.";
                    break;
                case -7:
                    this.a = "No se encontró conexión con el dispositivo.";
                    break;
                case -5:
                    this.a = "El dato ya existe.";
                    break;
                case -4:
                    this.a = "El espacio no es suficiente.";
                    break;
                case -3:
                    this.a = "Error de tamaño.";
                    break;
                case -2:
                    this.a = "Error en el archivo (read/write).";
                    break;
                case -1:
                    this.a = "El SDK no está inicializado.";
                    break;
                case 0:
                    this.a = "El dato no se encuentra o está repetido.";
                    break;
                case 1:
                    this.a = "Operación correcta.";
                    break;
                case 4:
                    this.a = "Parámetro incorrecto.";
                    break;
                case 101:
                    this.a = "Error en la asignación del bufer.";
                    break;
            }
            return this.a;
        }
        #endregion
        #region Funciones principales de reloj

        public bool ConectarReloj(string ip, int puerto, int intentosDeConexion, bool beep)
        {
            if (intentosDeConexion == 0)
                intentosDeConexion = 1;
            int num = 1;
            this.a = string.Empty;
            this.g = false;
            this.i = new CZKEM();
            while (num <= intentosDeConexion)
            {
                if (this.i.Connect_Net(ip, puerto))
                {
                    this.g = true;
                    if (beep)
                        this.Beep();
                    num = intentosDeConexion + 1;
                    
                    return this.g;
                }
                else
                {
                    ++num;
                    this.i.GetLastError(ref this.h);
                    this.a = "(Método: Conectar) - Error al conectar el dispositivo. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                    return this.g;
                }
            }
            return this.g;
        }
        public bool DesconectarReloj()
        {
            if (this.i == null)
                return false;
            this.i.Disconnect();

            return true;
        }
        public bool ConsultarEstadoReloj(NumeroDe DatoConsultar)
        {
            this.a = string.Empty;
            this.g = false;
            this.b = 0;
            if (this.i.GetDeviceStatus(1, (int)DatoConsultar, ref this.b))
            {
                this.g = true;
            }
            else
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: Consultar) - Error al Consultar: " + DatoConsultar.ToString() + ". Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            }
            return this.g;
        }

        public bool CambiarEstatusReloj(Estatus Estatus)
        {
            this.a = string.Empty;
            this.g = false;
            if (this.i.EnableDevice(1, Estatus != Estatus.Deshabilitar))
            {
                this.g = true;
            }
            else
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: CambiarEstatus) - Error al " + Estatus.ToString() + " el dispositivo.  Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            }
            return this.g;
        }

        public List<UsuarioChecada> ObtenerChecadaAsistenciaReloj()
        {
            this.a = string.Empty;
            this.g = false;
            this.e = new List<UsuarioChecada>();
            if (!this.i.ReadGeneralLogData(1))
            {
                this.i.GetLastError(ref this.h);
                if( this.h == 0)
                {
                    this.a = "(Método: DispositivoObtenerRegistrosAsistencias) - No se encontro informacion que obtener";
                } else
                {
                    this.a = "(Método: DispositivoObtenerRegistrosAsistencias) - Error al leer el log de marcajes. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                }
                return this.e;
            } 
            string dwEnrollNumber = string.Empty;
            int dwVerifyMode = 0;
            int dwInOutMode = 0;
            int dwYear = 0;
            int dwMonth = 0;
            int dwDay = 0;
            int dwHour = 0;
            int dwMinute = 0;
            int dwSecond = 0;
            int dwWorkCode = 0;
            while (this.i.SSR_GetGeneralLogData(1, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode))
                this.e.Add(new UsuarioChecada()
                {
                    NumeroCredencial = int.Parse(dwEnrollNumber),
                    Anio = dwYear,
                    Mes = dwMonth,
                    Dia = dwDay,
                    Hora = dwHour,
                    Minuto = dwMinute,
                    Segundo = dwSecond
                });
            this.g = true;
            return this.e;
        }

        public bool BorrarRegistrosDeAsistenciaDelReloj()
        {
            this.a = string.Empty;
            this.g = false;
            if (this.i.ClearGLog(1))
            {
                this.g = true;
            }
            else
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: DispositivoBorrarRegistrosAsistencias) - Error al borrar los registros de asistencias. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            }
            return this.g;
        }

        public List<UsuarioChecada> ObtenerChecadaHistorialDeMovimientosDelReloj()
        {
            this.a = string.Empty;
            this.g = false;
            this.e = new List<UsuarioChecada>();
            if (!this.i.ReadSuperLogData(1))
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: DispositivoObtenerRegistrosOperativos) - Error al leer el log de marcajes operativos. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                return this.e;
            }
            int dwSEnrollNumber = 0;
            int dwTMachineNumber = 0;
            int dwYear = 0;
            int dwMonth = 0;
            int dwDay = 0;
            int dwHour = 0;
            int dwMinute = 0;
            int dwManipulation = 0;
            int Params1 = 0;
            int Params2 = 0;
            int Params3 = 0;
            int Params4 = 0;
            while (this.i.GetSuperLogData(1, ref dwTMachineNumber, ref dwSEnrollNumber, ref Params4, ref Params1, ref Params2, ref dwManipulation, ref Params3, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute))
            {
                UsuarioChecada usuarioMarcaje = new UsuarioChecada()
                {
                    NumeroCredencial = dwSEnrollNumber,
                    Anio = dwYear,
                    Mes = dwMonth,
                    Dia = dwDay,
                    Hora = dwHour,
                    Minuto = dwMinute,
                    MarcajeOperativo = new MarcajeOperativo()
                };
                usuarioMarcaje.MarcajeOperativo.NumeroDispositivo = dwTMachineNumber;
                usuarioMarcaje.MarcajeOperativo.Operacion = dwManipulation;
                usuarioMarcaje.MarcajeOperativo.Parametro1 = Params1;
                usuarioMarcaje.MarcajeOperativo.Parametro2 = Params2;
                usuarioMarcaje.MarcajeOperativo.Parametro3 = Params3;
                usuarioMarcaje.MarcajeOperativo.Parametro4 = Params4;
                this.e.Add(usuarioMarcaje);
            }
            this.g = true;
            return this.e;
        }

        public bool BorrarMovimientosDelReloj()
        {
            this.a = string.Empty;
            this.g = false;
            if (this.i.ClearSLog(1))
            {
                this.g = true;
            }
            else
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: DispositivoBorrarRegistrosOperativos) - Error al eliminar los registros operativos. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            }
            return this.g;
        }

        public bool CambiarHoraDelreloj()
        {
            this.a = string.Empty;
            this.g = false;
            if (!this.CambiarEstatusReloj(Estatus.Deshabilitar))
                return this.g;
            if (this.i.SetDeviceTime(1))
            {
                if (this.CambiarEstatusReloj(Estatus.Habilitar))
                    this.g = true;
            }
            else
                this.a = "(Método: DispositivoCambiarHoraAutomatico) - Error al cambiar la hora del dispositivo. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            return this.g;
        }

        public bool CambiarHoraDelRelojManual(int iDia, int iMes, int iAnio, int iHora, int iMinuto, int iSegundo)
        {
            this.a = string.Empty;
            this.g = false;
            if (!this.CambiarEstatusReloj(Estatus.Deshabilitar))
                return this.g;
            if (this.i.SetDeviceTime2(1, iAnio, iMes, iDia, iHora, iMinuto, iSegundo))
            {
                if (this.CambiarEstatusReloj(Estatus.Habilitar))
                    this.g = true;
            }
            else
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: DispositivoCambiarHoraManual) - Error al cambiar la hora del dispositivo de forma manual. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            }
            return this.g;
        }


        public bool AgregarUsuario(int NumeroCredencial, string UsuarioNombre, Permiso TipoPermiso, int indexHuella, string b64Huella)
        {
            this.a = string.Empty;
            this.g = false;
            if (this.i.SSR_SetUserInfo(1, NumeroCredencial.ToString(), UsuarioNombre, string.Empty, (int)TipoPermiso, true))
            {
                if (this.i.SetUserTmpExStr(1, NumeroCredencial.ToString(), indexHuella, 1, b64Huella))
                {
                    this.g = true;
                }
                else
                {
                    this.i.GetLastError(ref this.h);
                    this.a = "(Método: UsuarioEnrolar) - Error al guardar la huella. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                }
            }
            else
            {
                this.i.GetLastError(ref this.h);
                this.a = "(Método: UsuarioEnrolar) - Error al enrolar el usuario. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            }
            return this.g;
        }

        public bool BorrarUsuarioPorId(int NumeroCredencial)
        {
            this.a = string.Empty;
            this.g = false;
            if (this.i.SSR_DeleteEnrollDataExt(1, NumeroCredencial.ToString(), 12))
                this.g = true;
            else
                this.a = "(Método: UsuarioBorrar) - Error al eliminar el usuario. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            return this.g;
        }

        public List<UsuarioInformacion> BuscarTodosLosUsuarios(bool DeshabilitarDispositivo)
        {
            this.a = string.Empty;
            this.g = false;
            this.c = new List<UsuarioInformacion>();

            if (DeshabilitarDispositivo && !this.CambiarEstatusReloj(Estatus.Deshabilitar))
            {
                this.a = "(Método: UsuarioBuscarTodos) - Error al cambiar estatus de dispositivo. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                return this.c;
            }
            if (this.i.ReadAllUserID(1))
            {
                if (!this.i.ReadAllTemplate(1))
                {
                    this.a = "(Método: UsuarioBuscarTodos) - Error al obtener las huellas de los usuarios. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                    return this.c;
                }
                string dwEnrollNumber = string.Empty;
                string Name = string.Empty;
                string Password = string.Empty;
                int Privilege = 0;
                bool Enabled = false;
                string TmpData = string.Empty;
                int TmpLength = 0;
                int Flag = 0;
                if (!this.i.ReadAllUserID(1))
                {
                    this.a = "(Método: UsuarioBuscarTodos) - No se pudo obtener la información de la memoria. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                    return this.c;
                }
                if (!this.i.ReadAllTemplate(1))
                {
                    this.a = "(Método: UsuarioBuscarTodos) - No se pudieron obtener las huellas de la memoria. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                    return this.c;
                }
                this.c = new List<UsuarioInformacion>();
                while (this.i.SSR_GetAllUserInfo(1, out dwEnrollNumber, out Name, out Password, out Privilege, out Enabled))
                {
                    this.f = new UsuarioInformacion();
                    this.f.NumeroCredencial = int.Parse(dwEnrollNumber);
                    this.f.Nombre = Name;
                    this.f.Permiso = (Permiso)Privilege;
                    this.f.Contrasenia = Password;
                    this.f.Activo = Enabled;
                    this.f.Huellas = new List<UsuarioHuella>();
                    for (int dwFingerIndex = 0; dwFingerIndex < 10; ++dwFingerIndex)
                    {
                        if (this.i.GetUserTmpExStr(1, dwEnrollNumber, dwFingerIndex, out Flag, out TmpData, out TmpLength))
                            this.f.Huellas.Add(new UsuarioHuella()
                            {
                                IndexHuella = dwFingerIndex,
                                B64Huella = TmpData,
                                LongitudHuella = TmpLength
                            });
                    }
                    this.c.Add(this.f);
                }
                if (DeshabilitarDispositivo && !this.CambiarEstatusReloj(Estatus.Habilitar))
                {
                    this.a = "(Método: UsuarioBuscarTodos) - Error al habilitar el dispositivo. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
                    return this.c;
                }
                this.g = true;
                return this.c;
            }
            this.a = "(Método: UsuarioBuscarTodos) - Error al obtener la información de los usuarios. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            return this.c;
        }

        public bool BuscarUsuarioPorId(int NumeroCredencial)
        {
            this.a = string.Empty;
            this.g = false;
            string Name = string.Empty;
            string Password = string.Empty;
            string TmpData = string.Empty;
            int Privilege = 0;
            int TmpLength = 0;
            bool Enabled = false;
            if (this.i.SSR_GetUserInfo(1, NumeroCredencial.ToString(), out Name, out Password, out Privilege, out Enabled))
            {
                this.d = new UsuarioInformacion();
                this.d.NumeroCredencial = NumeroCredencial;
                this.d.Nombre = Name;
                this.d.Contrasenia = Password;
                this.d.Permiso = (Permiso)Privilege;
                this.d.Activo = Enabled;
                this.d.Huellas = new List<UsuarioHuella>();
                for (int dwFingerIndex = 0; dwFingerIndex < 10; ++dwFingerIndex)
                {
                    if (this.i.SSR_GetUserTmpStr(1, NumeroCredencial.ToString(), dwFingerIndex, out TmpData, out TmpLength))
                        this.d.Huellas.Add(new UsuarioHuella()
                        {
                            IndexHuella = dwFingerIndex,
                            B64Huella = TmpData,
                            LongitudHuella = TmpLength
                        });
                }
                this.g = true;
            }
            else
                this.a = "(Método: UsuarioBuscar) - Error al buscar el usuario. Código de error: " + this.h.ToString() + " - " + this.GenerarMensajeDeError(this.h);
            return this.g;
        }

        public List<UsuarioChecada> ObtenerChecadasDeUsuarios()
        {
            return e;
        }
        public UsuarioInformacion ObtenerInformacionDeUsuario()
        {
            return d;
        }

        public string ObtenerMensajeDeError()
        {
            return a;
        }

        public void LimpiarInformacionBajadaDeRelojes()
        {
            c.Clear();
            d = null;
            e.Clear();
            f = null;
            GC.Collect();
        }
        #endregion
    }
}
