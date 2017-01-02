using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenorCaminho.Classes
{
    class Aresta
    {
        public int id_ar { get; set; }
        public int peso { get; set; }
        public int ponto1 { get; set; }
        public int ponto2 { get; set; }
        public bool visitado { get; set; }
        public int acumulado { get; set; }
    }
}
