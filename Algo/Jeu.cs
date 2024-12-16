using Algo;
using ClasseJoueur;

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

        public void LancerJeu(string langue)
        {
            if (langue == "Francais")
            {
                Console.WriteLine("Choix de la taille de la grille, minimum 4 par 4 : ");
                this.tailleGrid = Convert.ToInt32(Console.ReadLine());
                while (tailleGrid < 4)
                {
                    Console.WriteLine("Entrez une taille de grille correcte, minimum 4x4 (ex : 5) : ");
                    tailleGrid = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Combien de joueurs : ");
                this.nbJoueurs = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entrez les pseudos des joueurs : ");
                Joueur[] joueurs = new Joueur[nbJoueurs];
                string pseudoTempo;
                for (int i = 0; i < nbJoueurs; i++)
                {
                    pseudoTempo = Console.ReadLine();
                    joueurs[i] = new Joueur(pseudoTempo);
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

                    
                    
                    for (int i = 0;i < nbJoueurs; i++)
                    {
                        #region TimerDe1Minute
                        //Console.WriteLine("Timer started for 1 minute");
                        DateTime startTime = DateTime.Now;
                        TimeSpan duration = new TimeSpan(0, 0, 10); // 1 minute
                        while (DateTime.Now - startTime < duration) //boucle 1 minute pour chaque tour de joueur
                        {
                            Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                            //TimeSpan elapsed = DateTime.Now - startTime;
                            //Console.WriteLine($"Elapsed time: {elapsed.Seconds} seconds");

                            Console.WriteLine("C'est au tour de " + joueurs[i].name + " de jouer. Vous avez 1 minute pour jouer.");
                            Plateau plateau = new Plateau(4);
                            plateau.toString();

                            Console.WriteLine("Entrez un mot : ");
                            string? mot = Console.ReadLine();

                            if (mot != null)
                            {
                                bool estPresent = plateau.Test_Plateau(mot);
                                Console.WriteLine($"Le mot '{mot}' est {(estPresent ? "présent" : "absent")} sur le plateau.");
                                if (estPresent == true)
                                {
                                    joueurs[i].Add_Mot(mot);
                                    joueurs[i].Score_Add(mot);
                                    Console.WriteLine("Le score de " + joueurs[i].name + " est de " + joueurs[i].score + " grâce aux mots cités suivants");
                                    joueurs[i].AfficherMotsJoueurs();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Aucun mot n'a été saisi.");
                            }
                        }
                        Console.WriteLine("Fin du joueur");
                    }
                    #endregion
                }
                Console.WriteLine("Fin de la Partie");
                #endregion
            }
            else //English
            {
                Console.WriteLine("Choose the grid, minimum 4x4 : ");
                tailleGrid = Convert.ToInt32(Console.ReadLine());
                while (tailleGrid < 4)
                {
                    Console.WriteLine("Please enter a correct grid size, minimum 4x4 (e.g. 5) : ");
                    tailleGrid = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("How many players? ");
                nbJoueurs = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter player names : ");
                Joueur[] joueurs = new Joueur[nbJoueurs];
                string pseudoTempo;
                for (int i = 0; i < nbJoueurs; i++)
                {
                    pseudoTempo = Console.ReadLine();
                    joueurs[i] = new Joueur(pseudoTempo);
                }

                int timer = 3;
                
                #region TotalGameTimer
                Console.WriteLine("Game timer started for 6 minute");
                DateTime startTime2 = DateTime.Now;
                TimeSpan duration2 = new TimeSpan(0, timer, 0); // 6 minutes

                while (DateTime.Now - startTime2 < duration2)
                {
                    Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                    //TimeSpan elapsed2 = DateTime.Now - startTime2;
                    //Console.WriteLine($"Elapsed time: {elapsed2.Seconds} seconds");

                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        #region TimerDe1Minute
                        //Console.WriteLine("Timer started for 1 minute");
                        DateTime startTime3 = DateTime.Now;
                        TimeSpan duration3 = new TimeSpan(0, 0, 10); // 1 minute
                        while (DateTime.Now - startTime3 < duration3) //boucle 1 minute pour chaque tour de joueur
                        {
                            Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                            //TimeSpan elapsed = DateTime.Now - startTime;
                            //Console.WriteLine($"Elapsed time: {elapsed.Seconds} seconds");

                            Console.WriteLine("It's " + joueurs[i].name + "'s turn to play. You have 1 minute : ");
                            Plateau plateau = new Plateau(4);
                            plateau.toString();

                            Console.WriteLine("Enter a word : ");
                            string? mot = Console.ReadLine();

                            if (mot != null)
                            {
                                bool estPresent = plateau.Test_Plateau(mot);
                                Console.WriteLine($"The word '{mot}' is {(estPresent ? "present" : "absent")} on the board.");
                                if (estPresent == true)
                                {
                                    joueurs[i].Add_Mot(mot);
                                    joueurs[i].Score_Add(mot);
                                    Console.WriteLine(joueurs[i].name + "'s score is " + joueurs[i].score + ", they used these words : ");
                                    joueurs[i].AfficherMotsJoueurs();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No word was entered.");
                            }
                        }
                        Console.WriteLine("Next Player");
                    }

                }
                Console.WriteLine("Game Over");
                //for each player
                for (int i = 0; i < nbJoueurs; i++)
                {
                    Console.WriteLine(joueurs[i].name + "'s score is " + joueurs[i].score + ".");
                }

                //if (J1.score > J2.score /*&& J1.score>J3.score*/)
                //{
                //    Console.WriteLine(J1.name + " wins!!!");
                //}//else if (J3.score>J2.score) Console.WriteLine(J3.name + " wins!!!");
                //else Console.WriteLine(J2.name + " wins!!!");

                #endregion
                #endregion
            }
        }
    }
}
