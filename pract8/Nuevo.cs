using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Nuevo : Form
    {
        public Nuevo()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre=txbNombre.Text;
            string descripcion = rtbDescripcion.Text;
            string sql = "INSERT INTO productos (nombre,descripcion,cantidad,costo) VALUES ('"+nombre+"','"+descripcion+"',0,0)" ;
            BaseDatos bd = new BaseDatos();
            if (bd.insertar(sql))
            {
                MessageBox.Show("Se ha agregado correctamente");
                Form1 f1 = new Form1();
                f1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se ha registrado");
            }


        }

        private void btnRegresar_Click(object sender, System.EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
