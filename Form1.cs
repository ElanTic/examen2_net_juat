namespace conversorDivisas
{
    public partial class Form1 : Form
    {
        private Label? lblMoneda;
        private ComboBox? listaDivisas;
        private Label[] lblsDivisa;
        private TextBox[] txtConversiones;
        private Label? lblMonto;
        private TextBox? campoMonto;
        private Button? btnCalcular;
        private double[] equivalencias;
        private Moneda[] datos;
        private int moneda;//moneda actual, indice en la lista de equivalencias que servira de base para convertir.
        public Form1()
        {
            InitializeComponent();
            inicializar();
        }
        private void inicializar()
        {
            //valores de cada moneda vasados en el USD.
            equivalencias = new double[] { 1, 21.23, 1.28, 0.89, 113.05 };

            //monedas que seran utilizadas
            datos = new Moneda[] {
                new Moneda("$","Dolar", Monedas.USD),
                new Moneda("$","Peso", Monedas.MX ),
                new Moneda("$","Dolar", Monedas.CAD ),
                new Moneda("Є","Euro", Monedas.EUR),
                new Moneda("¥","Yen japones", Monedas.JPY)
            };

            lblMonto = new Label();
            lblMonto.Text = "Monto";
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(200, 10);

            campoMonto = new TextBox();
            campoMonto.Size = new Size(100, 20);
            campoMonto.Location = new Point(200, 40);
            lblsDivisa = new Label[datos.Length];
            txtConversiones = new TextBox[datos.Length];
            
            //lista de campos con resultados de la conversion
            for (int i = 0; i < datos.Length; i++)
            {
                Label lblDivisa = new Label();
                lblDivisa.Text = "";
                lblDivisa.AutoSize = true;
                lblDivisa.Location = new Point(10, 15 + (30 * i));
                lblDivisa.Visible = false;

                TextBox textoResul = new TextBox();
                textoResul.Size = new Size(100, 20);
                textoResul.Location = new Point(200, 10 + (30 * i));
                textoResul.Visible = false;

                groupBox1.Controls.Add(lblDivisa);
                groupBox1.Controls.Add(textoResul);

                lblsDivisa[i] = lblDivisa;
                txtConversiones[i] = textoResul;
            }
            btnCalcular = new Button();
            btnCalcular.Text = "Calcular";
            btnCalcular.AutoSize = true;
            btnCalcular.Location = new Point(200, 70);
            btnCalcular.Click += new EventHandler(btnCalcular_Click);

            lblMoneda = new Label();
            lblMoneda.Text = "Moneda";
            lblMoneda.AutoSize = true;
            lblMoneda.Location = new Point(50, 10);

            //el selector de moneda
            listaDivisas = new ComboBox();
            //listaDivisas.Items.Add("");
            listaDivisas.Items.Add("USD - Dólar estadounidense");
            listaDivisas.Items.Add("MXN - Peso mexicano");
            listaDivisas.Items.Add("CAD - Dólar canadiense");
            listaDivisas.Items.Add("EUR - Euro");
            listaDivisas.Items.Add("JPY - Yen japonés");
            listaDivisas.SelectedIndex = 0;
            listaDivisas.Location = new Point(50, 40);
            listaDivisas.SelectedValueChanged += new EventHandler(cmb_ValueChanged);

            this.Controls.Add(campoMonto);
            this.Controls.Add(lblMonto);
            this.Controls.Add(btnCalcular);
            this.Controls.Add(lblMoneda);
            this.Controls.Add(listaDivisas);
            //this.Controls.Add(this.groupBox1);

        }
        private void cmb_ValueChanged(object sender, EventArgs e)
        {
            moneda = listaDivisas.SelectedIndex; //cambia la base 
            desaparece(); //borra las conversiones
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            desaparece(); //borra conversiones anteriores
            double monto = campoMontoDouble();
            if (monto == 0) return; //no hay nada en el campo o no es una cifra numerica
            int[] listaConverison = new int[datos.Length]; //lista para recuperar informacion del siguiente formulario

            Form2 subVentana = new Form2(datos, moneda, listaConverison);
            subVentana.ShowDialog();
            int renglon = 0; //indice del renglon en el grupo de conversiones
            for (int i = 0; i < listaConverison.Length; i++)
            {
                if(listaConverison[i]!= 0) { //entonces se dio cilick en el formulario pasado.
                    muestraConversion(renglon, datos[i], convierte(i, monto));
                    renglon++;
                }
            }
        }
        private double convierte(int indice, double monto)
        {
            //calcula la equivalencia en terminos de la moneda actual y la multiplica por el monto.
            return (equivalencias[indice] / equivalencias[moneda]) * monto;
        }

        /**
         * indice es el indice de los Label y Textbox en las listas.
         * dato es el contenido de la Label.
         * monto es contenido del canmpo de texto.
         */
        private void muestraConversion(int indice, Moneda dato, double monto)
        {
            lblsDivisa[indice].Text = dato.ToString();
            txtConversiones[indice].Text = $"{dato.signo} {monto.ToString("0.0000")}";
            lblsDivisa[indice].Visible=true;
            //lblsDivisa[indice].BringToFront();
            txtConversiones[indice].Visible = true;
            //txtConversiones[indice].BringToFront();
        }

        //obtiene info del campo para el monto y lo convierte en un numero.
        private double campoMontoDouble()
        {
            double input = 0;
            try
            {
                input = Double.Parse(campoMonto.Text);
            }catch(Exception ex)
            {
                
            }
            return input;
        }

        //para borrar los campos con las conversiones
        private void desaparece()
        {
            foreach(Label label in lblsDivisa)
            {
                label.Visible=false;
            }
            foreach(TextBox textBox in txtConversiones)
            {
                textBox.Visible=false;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}