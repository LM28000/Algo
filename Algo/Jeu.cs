using Algo;
using ClasseJoueur;
using System;

namespace GameJeu
{
    internal class Jeu
    {
        public string langue;
        public int nbJoueurs;
        public int tailleGrid;

        /// <summary>
        /// Constructeur de la classe Jeu.
        /// </summary>
        /// <param name="langue">Langue du jeu.</param>
        public Jeu(string langue)
        {
            this.langue = langue;
        }

        /// <summary>
        /// Méthode pour ajouter le score d'un mot.
        /// </summary>
        /// <param name="mot">Le mot à évaluer.</param>
        /// <param name="plateau1">Le plateau de jeu.</param>
        /// <returns>Le score du mot.</returns>
        public int Score_Add(string mot, Plateau plateau1)
        {
            char[] tabChar = new char[mot.Length];
            tabChar = mot.ToCharArray();
            int scoreTp = 0;

            foreach (char c in tabChar)
            {
                for (int i = 0; i < plateau1.lettres.Count; i++)
                {
                    if (plateau1.lettres[i].Caractere == c)
                        scoreTp += plateau1.lettres[i].Points;
                }
            }
            return scoreTp;
        }

        /// <summary>
        /// Méthode pour lancer le jeu.
        /// </summary>
        /// <param name="langue">Langue du jeu.</param>
        public void LancerJeu(string langue)
        {
            Console.WriteLine("Choix de la taille de la grille, minimum 4 par 4 : ");
            this.tailleGrid = Convert.ToInt32(Console.ReadLine());
            while (tailleGrid < 4)
            {
                Console.WriteLine("Entrez une taille de grille correcte, minimum 4x4 (ex : 5) : ");
                this.tailleGrid = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Combien de joueurs : ");
            this.nbJoueurs = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("IA (oui/non) ?");
            Joueur[] joueurs = new Joueur[nbJoueurs];
            if (Console.ReadLine() == "oui")
            {
                string pseudoTempo;
                joueurs[0] = new Joueur("bot");
                joueurs[0].ia = true;
                Console.WriteLine("Pseudo");
                joueurs[1] = new Joueur(Console.ReadLine());
                joueurs[1].indice = 0;
            }
            else
            {
                Console.WriteLine("Entrez les pseudos des joueurs : ");
                string pseudoTempo;
                for (int i = 0; i < nbJoueurs; i++)
                {
                    pseudoTempo = Console.ReadLine();
                    joueurs[i] = new Joueur(pseudoTempo);
                }
            }
            int timer = 1;

            Console.WriteLine("La temps de jeu pour la partie est de 6 minutes");
            DateTime startTime2 = DateTime.Now;
            TimeSpan duration2 = new TimeSpan(0, timer, 0);

            while (DateTime.Now - startTime2 < duration2)
            {
                for (int i = 0; i < nbJoueurs; i++)
                {
                    Plateau plateau = new Plateau(this.tailleGrid, langue);
                    plateau.toString();

                    DateTime startTime = DateTime.Now;
                    TimeSpan duration = new TimeSpan(0, 0, 15);

                    if (joueurs[i].ia == true)
                    {
                        joueurs[i].indice = 0;
                        while ((DateTime.Now - startTime < duration) && joueurs[i].indice < plateau.dictionnaire.contenu.Count())
                        {
                            if (plateau.dictionnaire.contenu[joueurs[i].indice] == null || plateau.dictionnaire.contenu[joueurs[i].indice] == "")
                            {
                                joueurs[i].indice++;
                            }
                            else
                            {
                                if (plateau.Test_Plateau(plateau.dictionnaire.contenu[joueurs[i].indice]))
                                {
                                    joueurs[i].Add_Mot(plateau.dictionnaire.contenu[joueurs[i].indice]);
                                    joueurs[i].score += Score_Add(plateau.dictionnaire.contenu[joueurs[i].indice], plateau);
                                    Console.WriteLine("Le score de " + joueurs[i].name + " est de " + joueurs[i].score + " grâce aux mots cités suivants");
                                    joueurs[i].AfficherMotsJoueurs();
                                }
                                joueurs[i].indice++;
                            }
                        }
                    }
                    else
                    {
                        while (DateTime.Now - startTime < duration)
                        {
                            Console.WriteLine("C'est au tour de " + joueurs[i].name + " de jouer. Vous avez 1 minute pour jouer.");
                            Console.WriteLine("Entrez un mot : ");
                            string? mot = Console.ReadLine();

                            if (mot != null)
                            {
                                bool estPresent = plateau.Test_Plateau(mot);
                                Console.WriteLine($"Le mot '{mot}' est {(estPresent ? "présent" : "absent")} sur le plateau.");
                                if (estPresent == true)
                                {
                                    joueurs[i].Add_Mot(mot);
                                    joueurs[i].score += Score_Add(mot, plateau);
                                    Console.WriteLine("Le score de " + joueurs[i].name + " est de " + joueurs[i].score + " grâce aux mots cités suivants");
                                    joueurs[i].AfficherMotsJoueurs();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Aucun mot n'a été saisi.");
                            }
                        }
                    }

                    Console.WriteLine("Fin du joueur");
                }
            }
            Console.WriteLine("Fin de la Partie");

            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.WriteLine("Le score de " + joueurs[i].name + " est de " + joueurs[i].score + ".");
            }

            int[] scores = new int[nbJoueurs];
            for (int i = 0; i < nbJoueurs; i++)
            {
                scores[i] = joueurs[i].score;
            }
            int maxScore = scores.Max();
            for (int i = 0; i < nbJoueurs; i++)
            {
                if (maxScore == joueurs[i].score)
                    Console.WriteLine(joueurs[i].name + " est le vainqueur avec un score de " + joueurs[i].score);
            }
        }
    }
}

