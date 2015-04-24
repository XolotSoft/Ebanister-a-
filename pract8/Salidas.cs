using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pract8
{
    public partial class Salidas : Form
    {
        public Salidas()
        {
            InitializeComponent();
        }

        BaseDatos lista = new BaseDatos();
        BaseDatos hj = new BaseDatos();
        BaseDatos salidas = new BaseDatos();
        BaseDatos bd = new BaseDatos();

        private void Salidas_Load(object sender, EventArgs e)
        {
            string sql = "SELECT s.id,p.nombre AS Producto,s.cantidad AS Cantidad FROM salidas s INNER JOIN productos p ON s.producto_id = p.id";
            string sql2 = "SELECT * FROM productos";
            
            salidas.buscar(sql);
            dgvSalidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSalidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSalidas.RowHeadersVisible = false;
            dgvSalidas.DataSource = salidas.ds.Tables[0];
            dgvSalidas.Columns[0].Visible = false;

            lista.buscar(sql2);
            cmbProducto.DataSource = lista.ds.Tables[0].DefaultView;
            cmbProducto.DisplayMember = "nombre";
            cmbProducto.ValueMember = "id";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string producto = Convert.ToString(cmbProducto.SelectedValue);
            int cantidad = Convert.ToInt16(txbCantidad.Text);
            string sql3 = "SELECT * FROM productos WHERE id = '" + producto + "'";
            hj.buscar(sql3);
            string cantidadAct = Convert.ToString(Convert.ToInt16(hj.ds.Tables[0].Rows[0][3]) - cantidad);
            string sql4 = "INSERT INTO salidas (producto_id,cantidad) VALUES ('" + producto + "','" + cantidad + "') ";
            string sql5 = "UPDATE productos SET cantidad = " + cantidadAct + " WHERE id = " + producto + "";
          
            if (Convert.ToInt16(hj.ds.Tables[0].Rows[0][3]) >= cantidad)
            {
                if(bd.insertar(sql4))
                {
                    if(bd.modificar(sql5))
                    {
                        MessageBox.Show("Se ha modificado correctamente");
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No se reaizo");
                    }
                }
                else
                {
                    MessageBox.Show("No se realizo");
                }
            }
            else
            {
                MessageBox.Show("No tienes productos suficientes");
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
