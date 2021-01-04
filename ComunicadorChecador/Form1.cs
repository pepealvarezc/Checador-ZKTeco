using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComunicadorChecador.models;


namespace ComunicadorChecador
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ZKBio reloj = new ZKBio();
        private bool checadorConectado = false;
        public Form1()
        {
            InitializeComponent();
            //gridControl.DataSource = GetDataSource();
            //BindingList<Customer> dataSource = GetDataSource();
            //gridControl.DataSource = dataSource;
            //bsiRecordsCount.Caption = "RECORDS : " + dataSource.Count;

        }

        private void resetGrid()
        {
            //BindingList<object> dataSource = new BindingList<object>();

            //gridControl.DataSource = dataSource;
            //bsiRecordsCount.Caption = "RECORDS : " + dataSource.Count;
        }
        void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.ShowRibbonPrintPreview();
        }
        public BindingList<Customer> GetDataSource()
        {
            BindingList<Customer> result = new BindingList<Customer>();
            result.Add(new Customer()
            {
                ID = 1,
                Name = "ACME",
                Address = "2525 E El Segundo Blvd",
                City = "El Segundo",
                State = "CA",
                ZipCode = "90245",
                Phone = "(310) 536-0611"
            });
            result.Add(new Customer()
            {
                ID = 2,
                Name = "Electronics Depot",
                Address = "2455 Paces Ferry Road NW",
                City = "Atlanta",
                State = "GA",
                ZipCode = "30339",
                Phone = "(800) 595-3232"
            });
            return result;
        }
        public class Customer
        {
            [Key, Display(AutoGenerateField = false)]
            public int ID { get; set; }
            [Required]
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            [Display(Name = "Zip Code")]
            public string ZipCode { get; set; }
            public string Phone { get; set; }
        }

        private void gridControl_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (this.reloj.ConectarReloj("192.168.0.6", 4370, 0, false))
            {
                checadorStatus.Caption = "Checador Conectado";
                this.checadorConectado = true;
            }


        }

        private void desconectarChecador_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.reloj.DesconectarReloj())
            {
                checadorStatus.Caption = "No Conectado";
                this.checadorConectado = false;

            }

        }

        private bool checarEstatusChecador()
        {
            if (!this.checadorConectado)
            {
                MessageBox.Show("Checador no conectado, necesita primero conectarse a el para realizar esta accion");
            }

            return this.checadorConectado;
        }

        private void usuarios_btn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.checarEstatusChecador())
            {
                this.resetGrid();

                List<UsuarioInformacion> usuarios = new List<UsuarioInformacion>();

                usuarios = this.reloj.BuscarTodosLosUsuarios(false);

                BindingList<UsuarioInformacion> dataSource = new BindingList<UsuarioInformacion>();

                foreach (UsuarioInformacion usuario in usuarios)
                {
                    dataSource.Add(usuario);
                }

                gridControl.DataSource = dataSource;
                bsiRecordsCount.Caption = "RECORDS : " + dataSource.Count;

            }



        }

        private void checadas_btn_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (this.checarEstatusChecador())
            {
                this.resetGrid();

                List<UsuarioChecada> checadas = new List<UsuarioChecada>();

                checadas = this.reloj.ObtenerChecadaAsistenciaReloj();

                BindingList<UsuarioChecada> dataSource = new BindingList<UsuarioChecada>();

                foreach (UsuarioChecada checada in checadas)
                {
                    dataSource.Add(checada);
                }

                gridControl.DataSource = dataSource;
                bsiRecordsCount.Caption = "RECORDS : " + dataSource.Count;


            }
        }

        private void movimientos_btn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.checarEstatusChecador())
            {
                this.resetGrid();

                List<UsuarioChecada> movimientos = new List<UsuarioChecada>();

                movimientos = this.reloj.ObtenerChecadaHistorialDeMovimientosDelReloj();

                BindingList<UsuarioChecada> dataSource = new BindingList<UsuarioChecada>();

                foreach (UsuarioChecada movimiento in movimientos)
                {
                    dataSource.Add(movimiento);
                }

                gridControl.DataSource = dataSource;
                bsiRecordsCount.Caption = "RECORDS : " + dataSource.Count;

            }
        }
    }

}