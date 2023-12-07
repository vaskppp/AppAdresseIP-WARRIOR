using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdresseIP
{
    class Class2
    {
        //CalculMasque
        public static int[,] CalculMasque(int CIDR)
        {
            int[,] masque = new int[ 4, 8 ];
            int nombreDe1 = CIDR;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (nombreDe1 > 0)
                    {
                        masque[i, j] = 1;
                        nombreDe1--;
                    }
                    else
                    {
                        masque[i, j] = 0;
                    }
                }
            }

            return masque;
        }
    }
}



