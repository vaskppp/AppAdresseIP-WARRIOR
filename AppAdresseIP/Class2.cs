namespace AppAdresseIP
{
    class Class2
    {
        //CalculMasque
        public static int[,] CalculMasque(int CIDR)
        {
            int[,] masque = new int[4, 8];

            for (int octet = 0; octet < 4; octet++)
            {
                for (int bit = 7; bit >= 0; bit--)
                {
                    if (CIDR > 0)
                    {
                        masque[octet, bit] = 1;
                        CIDR--;
                    }
                    else
                    {
                        masque[octet, bit] = 0;
                    }
                }
            }

            return masque;
        }
    }
}



