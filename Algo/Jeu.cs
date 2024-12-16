using Algo;
using ClasseJoueur;
using System;

namespace GameJeu
{
    internal class Jeu
    {
        public string langue; // Langue du jeu
        public int nbJoueurs; // Nombre de joueurs
        public int tailleGrid; // Taille de la grille

        // Constructeur de la classe Jeu
        public Jeu(string langue)
        {
            this.langue = langue;
            //this.nbJoueur = 0;
            //this.tailleDeGrid = tailleGrid;
        }

        // Méthode pour ajouter le score d'un mot
        public int Score_Add(string mot, Plateau plateau1)
        {
            // Convertir le mot en tableau de caractères
            char[] tabChar = new char[mot.Length];
            tabChar = mot.ToCharArray();
            int scoreTp = 0;

            // Calculer le score du mot en fonction des lettres présentes sur le plateau
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

        // Méthode pour lancer le jeu
        public void LancerJeu(string langue)
        {
            // Demander la taille de la grille
            Console.WriteLine("Choix de la taille de la grille, minimum 4 par 4 : ");
            this.tailleGrid = Convert.ToInt32(Console.ReadLine());
            while (tailleGrid < 4)
            {
                Console.WriteLine("Entrez une taille de grille correcte, minimum 4x4 (ex : 5) : ");
                this.tailleGrid = Convert.ToInt32(Console.ReadLine());
            }

            // Demander le nombre de joueurs
            Console.WriteLine("Combien de joueurs : ");
            this.nbJoueurs = Convert.ToInt32(Console.ReadLine());

            // Demander si une IA est présente
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
                // Demander les pseudos des joueurs
                Console.WriteLine("Entrez les pseudos des joueurs : ");
                string pseudoTempo;
                for (int i = 0; i < nbJoueurs; i++)
                {
                    pseudoTempo = Console.ReadLine();
                    joueurs[i] = new Joueur(pseudoTempo);
                }
            }
            int timer = 1;

            #region TotalGameTimer
            // Définir le temps total de jeu
            Console.WriteLine("La temps de jeu pour la partie est de 6 minutes");
            DateTime startTime2 = DateTime.Now;
            TimeSpan duration2 = new TimeSpan(0, timer, 0); // 6 minutes

            // Boucle principale du jeu
            while (DateTime.Now - startTime2 < duration2)
            {
                for (int i = 0; i < nbJoueurs; i++)
                {
                    Plateau plateau = new Plateau(this.tailleGrid, langue);
                    plateau.toString();

                    #region TimerDe1Minute
                    // Définir le temps de jeu pour chaque joueur
                    DateTime startTime = DateTime.Now;
                    TimeSpan duration = new TimeSpan(0, 0, 15);

                    if (joueurs[i].ia == true)
                    {
                        joueurs[i].indice = 0;
                        // Boucle pour le tour de l'IA
                        while ((DateTime.Now - startTime < duration) && joueurs[i].indice < plateau.dictionnaire.contenu.Count()) //boucle 1 minute pour chaque tour de joueur
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
                        // Boucle pour le tour d'un joueur humain
                        while (DateTime.Now - startTime < duration) //boucle 1 minute pour chaque tour de joueur
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
                #endregion
            }
            Console.WriteLine("Fin de la Partie");

            // Afficher les scores de chaque joueur
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.WriteLine("Le score de " + joueurs[i].name + " est de " + joueurs[i].score + ".");
            }

            // Déterminer le vainqueur
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
            #endregion
        }
    }
}
