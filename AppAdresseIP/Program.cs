using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdresseIP
{
    class Program
    {
 


 
        /// <summary>
        /// Fonction de vérification de la saisie entre 2 bornes
        /// </summary>
        /// <param name="nbr"></param>
        /// <param name="borneMin"></param>
        /// <param name="borneMax"></param>
        /// <returns>Retourne vrai si le nombre passé en paramètre est entre les bornes</returns>
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

        /// <summary>
        /// Vérifie que la saisie soit bien un chiffre et entre les bornes définies.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Le nombre saisi</returns>
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


        public static int SaisieIp()
        {
            string strOctetUn;
            string strOctetDeux;
            string strOctetTrois;
            string strOctetQuatre;
            int octetUn;
            int octetDeux;
            int octetTrois;
            int octetQuatre;

            Console.WriteLine("Saisir une adresse IP");

            Console.WriteLine("1er octet : ");
            strOctetUn = Console.ReadLine();
            octetUn = int.Parse(strOctetUn);

            Console.WriteLine("2eme octet : ");
            strOctetDeux = Console.ReadLine();
            octetDeux = int.Parse(strOctetDeux);

            Console.WriteLine("3eme octet : ");
            strOctetTrois = Console.ReadLine();
            octetTrois = int.Parse(strOctetTrois);

            Console.WriteLine("4eme octet : ");
            strOctetQuatre = Console.ReadLine();
            octetQuatre = int.Parse(strOctetQuatre);

        }

  



        /// <summary>
        /// Programme principal
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] ip = new int[4];
            int[,] ipBin = new int[4, 8];
            int[,] ipReseauBin = new int[4, 8];
            int[] ipReseau = new int[4];
            int[,] ipBroadcastBin = new int[4, 8];
            int[] ipBroadcast = new int[4];
            int[,] masqueBin = new int[4, 8];
            int[] masque = new int[4];
            int cidr;
            ip = SaisieIP();
            ipBin = DecimalBinaire(ip);
            AfficheIPdeci("Adresse IP : ", ip);
            do
            {
                cidr = SaisieNombre("CIDR ? ");
            } while (VerifSaisie(cidr, 1, 30));
            masqueBin = CalculMasque(cidr);
            ipReseauBin = CalculReseau(ipBin, masqueBin);
            ipReseau = BinaireDecimal(ipReseauBin);
            masque = BinaireDecimal(masqueBin);
            ipBroadcastBin = CalculBroadcast(ipReseauBin, masqueBin);
            ipBroadcast = BinaireDecimal(ipBroadcastBin);
            AfficheIPdeci("Adresse réseau : ", ipReseau);
            AfficheIPdeci("Masque : ", masque);
            AfficheIPdeci("IP Broadcast : ", ipBroadcast);
            AfficheIPbin("Adresse IP en Binaire : \t", ipBin);
            AfficheIPbin("Masque en binaire : \t\t", masqueBin);
            AfficheIPbin("Adresse réseau en binaire :\t", ipReseauBin);
            AfficheIPbin("Adresse broadcast en binaire :\t", ipBroadcastBin);

            Console.ReadKey();
        }
    }
}
