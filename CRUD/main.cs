using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class main : Form
    {
        private CapaNegocios _capaNegocios;
        public main()
        {
            InitializeComponent();
            _capaNegocios = new CapaNegocios();
        }
        #region Eventos
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirContactoDetalle();
        }
        #endregion

        #region Metodos Privados
        private void AbrirContactoDetalle()
        {
            contactoDetalle contactoDetalle = new contactoDetalle();
            contactoDetalle.ShowDialog(this);
        }

        #endregion

        private void main_Load(object sender, EventArgs e)
        {
            CargarContactos();
        }

        public void CargarContactos( string TextoBuscar = null)
        {
            List<Contacto> contactos = _capaNegocios.GetContactos(TextoBuscar);
            gridContacts.DataSource = contactos;
        }

        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if(cell.Value.ToString() == "Editar")
            {
                contactoDetalle contactoDetalle = new contactoDetalle();

                contactoDetalle.CargarContacto(new Contacto
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString()),
                    Nombre = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Apellido = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    Telefono = gridContacts.Rows[e.RowIndex].Cells[5].Value.ToString(),
                    Direccion = gridContacts.Rows[e.RowIndex].Cells[6].Value.ToString(),
                });

                contactoDetalle.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Eliminar")
            {
                EliminarContacto(int.Parse(gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString()));
                CargarContactos();
            }
        }

        private void EliminarContacto(int id)
        {
            _capaNegocios.EliminarContacto(id);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarContactos(txtBuscar.Text);
            txtBuscar.Text = string.Empty;
        }
    }
}
