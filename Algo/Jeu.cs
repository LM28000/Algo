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

        public Jeu(string langue)
        {
            this.langue = langue;
            //this.nbJoueur = 0;
            //this.tailleDeGrid = tailleGrid;
        }
        public int Score_Add(string mot, Plateau plateau1)
        {
            //take score du mot -> add au score joueur
            char[] tabChar = new char[mot.Length];
            tabChar = mot.ToCharArray(); ;
            int scoreTp = 0;
            foreach (char c in tabChar)
            {
                for (int i = 0; i < plateau1.lettres.Count; i++)
                {
                    if (plateau1.lettres[i].Caractere == c)
                        scoreTp+= plateau1.lettres[i].Points;
                }
            }
            return scoreTp;
        }

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
                if (Console.ReadLine() == "oui") {
                    string pseudoTempo;
                    joueurs[0] = new Joueur("bot");
                    joueurs[0].ia = true;
                    Console.WriteLine("Pseudo");
                    joueurs[1] = new Joueur(Console.ReadLine());
                    joueurs[1].indice = 0;
                }
                else {
                    
                    Console.WriteLine("Entrez les pseudos des joueurs : ");
                    
                    string pseudoTempo;
                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        pseudoTempo = Console.ReadLine();
                        joueurs[i] = new Joueur(pseudoTempo);
                    }
                }
                int timer = 6;


                #region TotalGameTimer
                Console.WriteLine("La temps de jeu pour la partie est de 6 minutes");
                DateTime startTime2 = DateTime.Now;
                TimeSpan duration2 = new TimeSpan(0, timer, 0); // 6 minutes

                while (DateTime.Now - startTime2 < duration2)
                {
                    Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                                        //TimeSpan elapsed2 = DateTime.Now - startTime2;
                                        //Console.WriteLine($"Elapsed time: {elapsed2.Seconds} seconds");



                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        Plateau plateau = new Plateau(this.tailleGrid, langue);
                        plateau.toString();
                        #region TimerDe1Minute
                        DateTime startTime = DateTime.Now;
                        TimeSpan duration = new TimeSpan(0, 0, 15);

                        if (joueurs[i].ia == true)
                        {
                            while ((DateTime.Now - startTime < duration) && joueurs[i].indice < plateau.dictionnaire.contenu.Count()) //boucle 1 minute pour chaque tour de joueur
                            {
                                Console.WriteLine(joueurs[i].indice);
                                Console.WriteLine("nouveau mot");

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
                            while (DateTime.Now - startTime < duration) //boucle 1 minute pour chaque tour de joueur
                            {
                                Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
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
                //Compter le score for each player

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
                #endregion
            
            
        }
    }
}
