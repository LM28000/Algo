using Algo;
using ClasseJoueur;
using System;
using GameJeu;

namespace Test
{
    class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// <param name="args">Arguments de ligne de commande.</param>
        static void Main(string[] args)
        {
            string langue;
            int nbJoueur = 2;
            int tailleGrid = 4;

            Console.WriteLine("Choose your language / Choix de langue : ");
            Console.WriteLine("- Francais");
            Console.WriteLine("- English");
            langue = Convert.ToString(Console.ReadLine());

            while (langue != "Francais" && langue != "English")
            {
                Console.WriteLine("Please enter a correct language / Veuillez entrer une langue correct");
                langue = Convert.ToString(Console.ReadLine());
            }

            Jeu Game1 = new Jeu(langue);
            Game1.LancerJeu(langue);
        }
    }
}
