using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdresseIP
{
    class Program
    {
        public static int[,] CalculReseau(int[,] IPbin, int[,] MasqueBin)
        {
            /* Fonction Calcul réseau :
             * Paramètres :  
             * - un tableau à 2 dimensions d'un adresse IP sous format binaire [octet de l'adresse IP, bit de l'octet]
             * - un tableau à 2 dimensions d'un masque sous format binaire [octet de l'adresse IP, bit de l'octet]
             * Retour : un tableau d'une adresse IP sous bormat binaire [octet de l'adresse IP, bit de l'octet]
             */

            int[,] ipReseauBin = new int[4, 8];
            // Parcours de tous les bits du tableau à 2 dimensions
             for (int octet = 0; octet <= 3; octet++)
            {
                for (int bit = 7; bit >=0; bit--)
                {
                    if ( MasqueBin[octet, bit] == 1)
                        ipReseauBin[octet, bit] = IPbin[octet, bit];
                    else
                        ipReseauBin[octet, bit] = 0;
                }
            }
            // retour de la valeur de l'IP réseau en binaire
            return ipReseauBin;
        }

        public static int[] BinDeci(int[,] binIP)
        {
            /* Méthode conversion Binaire vers Décimal
             */
            int[] deciIP = new int[4];

            for (int octet = 0; octet < 4; octet++)
            {
                for (int bit = 7; bit >=0; bit--)
                {
                    if (binIP[octet, bit] == 1)
                    {
                        deciIP[octet] = deciIP[octet] + (int)Math.Pow(2,bit);
                    }
                }
            }
            return deciIP;

        }

        public static bool VerifSaisie(int nbr, int borneMin, int borneMax)
        {
        //Fonction Verif entre bonemin et bornemax
            bool ok = false;
            //Verif de la valeur entrée
            if (nbr < borneMin || nbr > borneMax)
            {
                ok = true;
                Console.WriteLine(nbr + " n'est pas entre " + borneMin + " et " + borneMax);
            }
            return ok;
        }

        public static int SaisieNombre(string message)
        {
            string strvaleur;
            int unentier;
            Boolean saisieinvalide = true;
            do
            {
                Console.Write(message);
                strvaleur = Console.ReadLine();
                if (int.TryParse(strvaleur, out unentier) == true)
                {
                    saisieinvalide = false;
                }
                else
                {
                    Console.WriteLine("La valeur n'est pas un nombre");
                }
            }
            while (saisieinvalide);
            return unentier;
        }

        public static int[] SaisieIP()
        {
            int[] tabIP = new int[4];
            int saisie;
            Console.WriteLine("Saisir une adresse IP : ");
            for (int octet=0; octet<4;octet++)
            {
                do
                {
                    saisie = SaisieNombre("Saisir l'octet n°" + (octet + 1) + ": ");
                }
                while (VerifSaisie(saisie, 0, 255));
                tabIP[octet] = saisie;
            }
            return tabIP;
        }

        public static int[,] CalculMasque(int CIDR)
        {
            int[,] adresseMasque = new int[4, 8];
            for (int octet = 0; octet < 4; octet++)
            {
                for (int bit = 7; bit >=0; bit--)
                {
                    if (CIDR > 0)
                    {
                        CIDR--;
                        adresseMasque[octet, bit] = 1;
                    }
                    else
                    {
                        adresseMasque[octet, bit] = 0;
                    }
                }
            }
            return adresseMasque;
        }

        public static int[,] DecimalBinaire(int[] ipDecimal)
        {
            int valOctet; // Délcaration d'une variable, car le tableau est passé par référence et les valeurs peuvent être modifiées.
            int[,] ipBin = new int[4, 8];
            for (int octet = 0; octet < 4; octet++)
            {
               valOctet = ipDecimal[octet];
                for (int bit = 7; bit >= 0; bit--)
                {
                    if (valOctet - (int)Math.Pow(2,bit)>=0)
                    {
                        ipBin[octet, bit] = 1;
                        valOctet = valOctet - (int)Math.Pow(2, bit);
                    }
                    else
                        ipBin[octet, bit] = 0;
                }
            }
            return ipBin;
        }
        public static void AfficheIPbin(string message, int[,] ipBin)
            /* Méthode qui Affiche une adresse IP sous format binaire précédée d'un message  */
        {
            Console.Write(message);
            for (int octet = 0; octet < 4; octet++)
            {
                for (int bit = 7; bit >= 0; bit--)
                {
                    Console.Write(ipBin[octet, bit]);
                }
                Console.Write(".");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] ip = new int[4];
            int[,] ipBin = new int[4, 8];
            int[,] ipReseauBin = new int[4,8];
            int[] ipReseau = new int[4];
            int[,] masqueBin = new int[4, 8];
            int[] masque = new int[4];
            int cidr;
            ip = SaisieIP();
            ipBin = DecimalBinaire(ip);
            Console.WriteLine("Adresse IP : " + ip[0] + "." + ip[1] + "." + ip[2] + "." + ip[3]);
            do
            {
                cidr = SaisieNombre("CIDR ? ");
            } while (VerifSaisie(cidr, 1, 30));
            masqueBin = CalculMasque(cidr);
            ipReseauBin=CalculReseau(ipBin, masqueBin);
            ipReseau = BinDeci(ipReseauBin);
            masque = BinDeci(masqueBin);
            Console.WriteLine("Adresse réseau : " + ipReseau[0] + "." + ipReseau[1] + "." + ipReseau[2] + "." + ipReseau[3]);
            Console.WriteLine ("Masque : " + masque[0] + "." + masque[1] + "." + masque[2] + "." + masque[3]);
            AfficheIPbin("Adresse IP en Binaire : \t", ipBin);
            AfficheIPbin("Masque en binaire : \t\t", masqueBin);
            AfficheIPbin("Adresse réseau en binaire :\t", ipReseauBin);

            Console.ReadKey(); 
        }
    }
}
