using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Formatif03B
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Calculatrice
    {
        public int ajouter(string nombres)
        {
            string messageErreur = "";
            string[] chiffres = separerChiffre(nombres, out messageErreur);
            int resultat = 0;
            List<int> nombreNegatif = new List<int> { };
            resultat = faireCalcul(chiffres, resultat, nombreNegatif);
            messageErreur = verifierSiNombreNegatif(nombreNegatif, messageErreur);
            envoyerMessageErreur(messageErreur);
            return resultat;
        }
        private static int determinerSepatateurSiString(string nombres, List<string> separateur, int i)
        {
            for (int j = i + 1; j < nombres.Length - 1; j++)
            {
                if (!char.IsDigit(nombres, j) && nombres.ToCharArray()[j] != '-')
                {
                    separateur[1] += (nombres.ToCharArray()[j].ToString());
                    i++;
                }
                else { break; }
            }

            return i;
        }
        private static void envoyerMessageErreur(string messageErreur)
        {
            if (messageErreur != "")
            {
                throw new ArgumentException(messageErreur);
            }
        }
        private static int faireCalcul(string[] chiffres, int resultat, List<int> nombreNegatif)
        {
            foreach (var item in chiffres)
            {
                int chiffre;
                int.TryParse(item, out chiffre);
                if (chiffre >= 0 && chiffre < 1000)
                {
                    resultat += chiffre;
                }
                else if (chiffre < 1000)
                {
                    nombreNegatif.Add(chiffre);
                }
            }
            return resultat;
        }

        private static string separerMessageErreur(string messageErreur)
        {
            if (messageErreur != "")
            {
                messageErreur += "\n";
            }

            return messageErreur;
        }

        private string[] separerChiffre(string nombres, out string messageErreur)
        {
            List<string> separateur = new List<string> { "\n" };
            char charactereAvant = ' ';
            messageErreur = "";
            for (int i = 0; i < nombres.Length - 1; i++)
            {
                if (char.IsDigit(charactereAvant) && !char.IsDigit(nombres, i) && nombres.ToCharArray()[i] != '\n')
                {
                    separateur.Add(nombres.ToCharArray()[i].ToString());
                    messageErreur = verifierSiSepareteurDifferent(nombres, separateur, i, messageErreur);
                    i = determinerSepatateurSiString(nombres, separateur, i);
                }
                charactereAvant = nombres.ToCharArray()[i];
            }
            messageErreur = verifierSiSeparateurFin(nombres, messageErreur);
            string[] chiffres = nombres.Split(separateur.ToArray(), StringSplitOptions.None);
            return chiffres;

        }

        private static string verifierSiSepareteurDifferent(string nombres, List<string> separateur, int i, string messageErreur)
        {
            if (nombres.ToCharArray()[i].ToString() != separateur[1])
            {
                return (separerMessageErreur(messageErreur) + "\'" + separateur[1] + "\' attendu mais \'" + nombres.ToCharArray()[i].ToString() + "\' trouvé");
            }
            return messageErreur;
        }

        private static string verifierSiSeparateurFin(string nombres, string messageErreur)
        {
            if (!char.IsNumber(nombres.ToCharArray()[nombres.Length - 1]))
            {
                return (separerMessageErreur(messageErreur) + "La chaine de caractère ne peut pas finir avec " + nombres[nombres.Length - 1]);
            }
            return messageErreur;
        }
        private static string verifierSiNombreNegatif(List<int> nombreNegatif, string messageErreur)
        {
            if (nombreNegatif.Count() >= 1)
            {
                string messageErreurNegatif = separerMessageErreur(messageErreur) + "Nombre(s) négatif(s) non autorisé(s) : ";
                foreach (var item in nombreNegatif)
                {
                    messageErreurNegatif += item + ", ";
                }
                return messageErreurNegatif;
            }
            return messageErreur;
        }
    }
    
}