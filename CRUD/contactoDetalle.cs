using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CRUD
{
    public partial class contactoDetalle : Form
    {
        private CapaNegocios _capaNegocios;
        private Contacto _contacto;
        public contactoDetalle()
        {
            InitializeComponent();
            _capaNegocios = new CapaNegocios();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarContacto();
            this.Close();
            ((main)this.Owner).CargarContactos();
        }

        private void GuardarContacto()
        {
            Contacto contacto = new Contacto();
            contacto.Nombre = txtNombre.Text;
            contacto.Apellido = txtApellido.Text;
            contacto.Telefono = txtTelefono.Text;
            contacto.Direccion = txtDireccion.Text;

      // si concato tienen tiene algo quiere decir que CargarContacto se a ejecutado y utilizamos su id si no ponemos 0
            contacto.Id = _contacto != null ? _contacto.Id : 0;
            _capaNegocios.GuardarContacto(contacto);
        }

        public void CargarContacto(Contacto contacto)
        {
            _contacto = contacto;
            if(contacto != null)
            {
                LimpearFormulario();

                txtNombre.Text = contacto.Nombre;
                txtApellido.Text = contacto.Apellido;
                txtTelefono.Text = contacto.Telefono;
                txtDireccion.Text = contacto.Direccion;
            }
        }

        private void LimpearFormulario()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }
    }
}
