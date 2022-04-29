using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conversorDivisas
{
    public class Moneda {
        public string signo;
        public string nombre;
        public Monedas clave;

        public Moneda(string signo, string nombre, Monedas avr)
        {
            this.signo = signo;
            this.nombre = nombre;
            this.clave = avr;
        }
        public override string ToString()
        {
            return $"{clave}-{nombre}";
        }
    }
}
