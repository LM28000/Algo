using Algo;
using ClasseJoueur;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Open();

            string langue;
            Console.WriteLine("Choose your language / Choix de langue : ");
            Console.WriteLine("Francais"); //Mettre en forme pour une liste *
            Console.WriteLine("English");
            langue = Convert.ToString(Console.ReadLine());
            if (langue != "Francais" && langue != "English") //Pour éviter toutes autres langues
                if (langue == null)
                    Console.WriteLine("Please enter a correct language / Veuillez entrer une langue correct");
            Console.WriteLine(langue);

            //Déclaration de toutes les variables communes aux 2 langues ou systèmes separés?
            int tailleGrid = 4;
            int nbJoueurs = 2;
            if (langue == "Francais")
            {
                Console.WriteLine("Choix de la taille de la grille, minimum 4 par 4 : ");
                tailleGrid = Convert.ToInt32(Console.ReadLine());
                while (tailleGrid < 4)
                {
                    Console.WriteLine("Entrez une taille de grille correcte, minimum 4x4 (ex : 5) : ");
                    tailleGrid = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("Combien de joueurs : ");
                nbJoueurs = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entrez les pseudos des joueurs : ");
                string[] joueurs = new string[nbJoueurs];
                for (int i = 0; i < nbJoueurs; i++)
                {
                    joueurs[i] = Console.ReadLine();
                    Joueur Jp = new Joueur(joueurs[i]);
                }
                int timer = 3;
                Joueur J1 = new Joueur("Johnny");
                Joueur J2 = new Joueur("Autre");

                #region TotalGameTimer
                Console.WriteLine("La temps de jeu pour la partie est de 6 minutes");
                DateTime startTime2 = DateTime.Now;
                TimeSpan duration2 = new TimeSpan(0, timer, 0); // 6 minutes

                while (DateTime.Now - startTime2 < duration2)
                {
                    //Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                    //TimeSpan elapsed2 = DateTime.Now - startTime2;
                    //Console.WriteLine($"Elapsed time: {elapsed2.Seconds} seconds");

                    #region TimerDe1Minute
                    //Console.WriteLine("Timer started for 1 minute");
                    DateTime startTime = DateTime.Now;
                    TimeSpan duration = new TimeSpan(0, 1, 0); // 1 minute

                    while (DateTime.Now - startTime < duration) //boucle 1 minute pour chaque tour de joueur
                    {
                        //Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                        //TimeSpan elapsed = DateTime.Now - startTime;
                        //Console.WriteLine($"Elapsed time: {elapsed.Seconds} seconds");



                        Console.WriteLine("C'est au tour de " + J1.name + " de jouer. Vous avez 1 minute pour jouer.");
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
                                J1.Add_Mot(mot);
                                J1.Score_Add(mot);
                                Console.WriteLine("Le score de " + J1.name + " est de " + J1.score + " grâce aux mots cités suivants");
                                J1.AfficherMotsJoueurs();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Aucun mot n'a été saisi.");
                        }
                    }
                    Console.WriteLine("Fin du joueur");
                    #endregion
                }
                Console.WriteLine("Fin de la Partie");
                #endregion
            }
            else
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
                string[] joueurs = new string[nbJoueurs];
                for (int i = 0; i < nbJoueurs; i++)
                {
                    joueurs[i] = Console.ReadLine();
                    Joueur Jp = new Joueur(joueurs[i]);
                }

                int timer = 3;
                Joueur J1 = new Joueur("Johnny");
                Joueur J2 = new Joueur("Autre");

                #region TotalGameTimer
                Console.WriteLine("Game timer started for 6 minute");
                DateTime startTime2 = DateTime.Now;
                TimeSpan duration2 = new TimeSpan(0, timer, 0); // 6 minutes

                while (DateTime.Now - startTime2 < duration2)
                {
                    //Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                    //TimeSpan elapsed2 = DateTime.Now - startTime2;
                    //Console.WriteLine($"Elapsed time: {elapsed2.Seconds} seconds");

                    #region TimerDe1Minute
                    //Console.WriteLine("Timer started for 1 minute");
                    DateTime startTime = DateTime.Now;
                    TimeSpan duration = new TimeSpan(0, 1, 0); // 1 minute

                    while (DateTime.Now - startTime < duration) //boucle 1 minute pour chaque tour de joueur
                    {
                        //Thread.Sleep(1000); // Pause de 1 seconde pour "compter" une seconde
                        //TimeSpan elapsed = DateTime.Now - startTime;
                        //Console.WriteLine($"Elapsed time: {elapsed.Seconds} seconds");



                        Console.WriteLine("It's " + J1.name + "'s turn to play. You have 1 minute : ");
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
                                J1.Add_Mot(mot);
                                J1.Score_Add(mot);
                                Console.WriteLine(J1.name + "'s score is " + J1.score + ", they used these words : ");
                                J1.AfficherMotsJoueurs();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No word was entered");
                        }
                    }
                    Console.WriteLine("Next Player");
                    #endregion
                }
                Console.WriteLine("Game Over");
                //for each player
                Console.WriteLine(J1.name+"'s score is "+J1.score+".");
                Console.WriteLine(J2.name+"'s score is "+J2.score+".");
                if (J1.score > J2.score /*&& J1.score>J3.score*/)
                {
                    Console.WriteLine(J1.name + " wins!!!");
                }//else if (J3.score>J2.score) Console.WriteLine(J3.name + " wins!!!");
                else Console.WriteLine(J2.name + " wins!!!");

                #endregion

            }
            /*J1.Add_Mot("France");
            J1.Add_Mot("Espagne");
            J2.Add_Mot("Francais");
            J2.Add_Mot("Espagnol");
            J2.Add_Mot("Allemand");

            Console.WriteLine(J1.toString());
            Console.WriteLine(J2.toString());
            Console.WriteLine();*/
        }
    }
}
