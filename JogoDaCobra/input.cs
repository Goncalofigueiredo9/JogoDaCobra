using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Windows.Forms;
namespace JogoDaCobra
{
    class input
    {
        private static Hashtable keyTable = new Hashtable();
        // esta classe é usada para optimizar as teclas quando clicamos

        public static bool KeyPress(Keys key)
        {
            //esta classe vai fazer voltar a chave para a classe 

            if (keyTable[key] == null)
            {
                // se a hastable for empty então volta para falso;
                return false;
            }
            return (bool)keyTable[key];
        }
        public static void changeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }

    }
}
