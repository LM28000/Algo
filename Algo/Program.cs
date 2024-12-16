using Algo;
using ClasseJoueur;
using System;
using GameJeu;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Déclaration des variables
            string langue;
            int nbJoueur = 2;
            int tailleGrid = 4;

            // Demande à l'utilisateur de choisir une langue
            Console.WriteLine("Choose your language / Choix de langue : ");
            Console.WriteLine("- Francais");
            Console.WriteLine("- English");
            langue = Convert.ToString(Console.ReadLine());

            // Boucle pour s'assurer que l'utilisateur entre une langue correcte
            while (langue != "Francais" && langue != "English")
            {
                Console.WriteLine("Please enter a correct language / Veuillez entrer une langue correct");
                langue = Convert.ToString(Console.ReadLine());
            }

            // Création d'un nouvel objet Jeu avec la langue choisie
            Jeu Game1 = new Jeu(langue);
            // Lancement du jeu
            Game1.LancerJeu(langue);
        }
    }
}
