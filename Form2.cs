using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conversorDivisas
{
    public partial class Form2 : Form
    {
        private int[] listaSeleccionados { get; set; }
        private CheckBox[] checBoxes;

        /**
         * lista es la lista de datos para las checbox
         * actual es un indice de la checbox que hay que omitir
         * retorno es una lista que sera llenada con los datos obtenidos en este formulario
         */
        public Form2(Moneda[] lista, int actual, int[] retorno)
        {
            listaSeleccionados =retorno;
            InitializeComponent();
            inicializachecs(lista, actual);
        }
        private void inicializachecs(Moneda[] lista, int actual)
        {
            checBoxes = new CheckBox[lista.Length];
            Point  point = new Point(40 , 20);
            for (int i = 0; i < lista.Length; i++)
            {
                Moneda moneda = lista[i];
                CheckBox box = new CheckBox();
                box.Checked = false;  
                box.Name = $"checbox{i}";
                box.Text = moneda.ToString();
                box.TabIndex = (int)moneda.clave;
                checBoxes[i] = box;
                if (i != actual)
                {
                    box.Location = point;
                    point = new Point(point.X, point.Y+20);//solo se baja de renglon cuando se agrega una checbox.
                    this.Controls.Add(box);
                }
            }
            return;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //llena la lista de datos que seran usados por el formulario anterior
            for(int i = 0;i < checBoxes.Length; i++)
            {
                listaSeleccionados[i] = checBoxes[i].Checked?1:0;
            }
            this.Close();
        }

  
    }
}
