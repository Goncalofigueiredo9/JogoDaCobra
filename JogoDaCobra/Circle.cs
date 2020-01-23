using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaCobra
{
    class Circle
    {
        public int X { get; set; } // chama-se x
        public int Y { get; set; } // chama-se y

        public Circle()
        {
            // esta função reseta o x e o y para 0
            X = 0;
            Y = 0;
        }
    }
}