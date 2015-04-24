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
    public partial class Compras : Form
    {
        public Compras()
        {
            InitializeComponent();
        }
        BaseDatos lista = new BaseDatos();
        private void Compras_Load(object sender, EventArgs e)
        {
            BaseDatos compras = new BaseDatos();
            string sql = "SELECT c.id, p.nombre AS Producto, c.cantidad AS Cantidad, c.costo AS 'Costo Unitario', c.costo_total AS 'Costo Total',c.fecha AS Fecha FROM compras c INNER JOIN productos p ON c.producto_id = p.id";
            compras.buscar(sql);
            dgvCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCompras.RowHeadersVisible = false;
            dgvCompras.DataSource = compras.ds.Tables[0];
            dgvCompras.Columns[0].Visible = false;
            string dt = Convert.ToBase64String(compras.ds.Tables[0].Rows[0][5] as byte[]);

            MessageBox.Show(dt);
            string sql2 = "SELECT * FROM productos";
            lista.buscar(sql2);
            cmbProducto.DataSource = lista.ds.Tables[0].DefaultView;
            cmbProducto.DisplayMember = "nombre";
            cmbProducto.ValueMember = "id";
           
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            string producto = Convert.ToString(cmbProducto.SelectedValue);
            int cantidad = Convert.ToInt16(txbCantidad.Text);
            double costo = Convert.ToDouble(txbCosto.Text);
            BaseDatos hj = new BaseDatos();
            string sql3 = "SELECT * FROM productos WHERE id = '"+producto+"'";
            hj.buscar(sql3);
            string cantidadAct = Convert.ToString(Convert.ToInt16(hj.ds.Tables[0].Rows[0][3]) + cantidad); 
            string costoTotal = Convert.ToString(cantidad * costo);
            string sql = "INSERT INTO compras (producto_id,cantidad,costo,costo_total) VALUES ('" +
                producto + "','" + cantidad + "','" + costo + "','" + costoTotal + "')";
            string sql2 = "UPDATE productos SET cantidad = " + cantidadAct + " WHERE id = " + producto + "";
            BaseDatos insertar = new BaseDatos();
            if (insertar.insertar(sql))
            {
                if (insertar.modificar(sql2))
                {
                    MessageBox.Show("Se ha agregado correctamente");
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Hide();

                }
               
            }
            else
            {
                MessageBox.Show("No se ha agregado correctamente");
            }
        }

        private void btnRegresa_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}

