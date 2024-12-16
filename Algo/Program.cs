using Algo;
using ClasseJoueur;
using System;
using GameJeu;
using ClasseJoueur;

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
            langue = Console.ReadLine();

            while (string.IsNullOrEmpty(langue) || (langue != "Francais" && langue != "English"))
            {
                Console.WriteLine("Please enter a correct language / Veuillez entrer une langue correct");
                langue = Console.ReadLine();
            }

            Jeu Game1 = new Jeu(langue);
            Game1.LancerJeu(langue);
            // Dimensions de l'image
            int largeur = 800;
            int hauteur = 600;
            for (int i = 0; i < nbJoueur; i++)
            {
                // Générer et sauvegarder le nuage de mots
                string cheminFichier = "nuage_de_mots" + i + ".png";
                Console.WriteLine("Génération du nuage de mots pour le joueur " + i + "...");
                Game1.GenererNuageDeMots(Game1.CreerDictionnaire(Game1.joueurs[i].ListeDeMot, Game1.joueurs[i].OccurenceMot), largeur, hauteur, cheminFichier);
            }
            Console.WriteLine("Nuages de mots générés avec succès !");
        }
    }
}
