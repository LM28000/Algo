using Algo;
using ClasseJoueur;
using System;
using System.Drawing;

using System.IO;
using System.Linq;


namespace GameJeu
{
    internal class Jeu
    {
        public string langue;
        public int nbJoueurs;
        public int tailleGrid;
        public Joueur[] joueurs;

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
            while (!int.TryParse(Console.ReadLine(), out this.tailleGrid) || this.tailleGrid < 4)
            {
                Console.WriteLine("Entrez une taille de grille correcte, minimum 4x4 (ex : 5) : ");
            }

            Console.WriteLine("Combien de joueurs : ");
            while (!int.TryParse(Console.ReadLine(), out this.nbJoueurs) || this.nbJoueurs <= 0)
            {
                Console.WriteLine("Entrez un nombre de joueurs valide : ");
            }

            Console.WriteLine("IA (oui/non) ?");
            this.joueurs = new Joueur[nbJoueurs];
            string iaResponse = Console.ReadLine()?.Trim().ToLower();
            if (iaResponse == "oui")
            {
                joueurs[0] = new Joueur("bot") { ia = true };
                Console.WriteLine("Pseudo");
                joueurs[1] = new Joueur(Console.ReadLine());
                joueurs[1].indice = 0;
            }
            else
            {
                Console.WriteLine("Entrez les pseudos des joueurs : ");
                for (int i = 0; i < nbJoueurs; i++)
                {
                    string pseudoTempo;
                    while (string.IsNullOrWhiteSpace(pseudoTempo = Console.ReadLine()))
                    {
                        Console.WriteLine("Entrez un pseudo valide : ");
                    }
                    joueurs[i] = new Joueur(pseudoTempo);
                }
            }

            int timer = 20;
            Console.WriteLine("La temps de jeu pour la partie est de 6 minutes");
            DateTime startTime2 = DateTime.Now;
            TimeSpan duration2 = new TimeSpan(0, 0, timer);

            while (DateTime.Now - startTime2 < duration2)
            {
                Console.Clear();
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

                            if (!string.IsNullOrWhiteSpace(mot))
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


        public Dictionary<string, int> CreerDictionnaire(string[] ListeDeMot, int[] OccurenceMot)
        {
            // Vérifier que les deux tableaux ont la même longueur
            if (ListeDeMot.Length != OccurenceMot.Length)
            {
                throw new ArgumentException("Les tableaux ListeDeMot et OccurenceMot doivent avoir la même longueur.");
            }

            // Initialiser le dictionnaire
            Dictionary<string, int> dictionnaire = new Dictionary<string, int>();

            // Associer chaque mot avec son occurrence
            for (int i = 0; i < ListeDeMot.Length; i++)
            {
                if (ListeDeMot[i] == null || OccurenceMot[i]==0 || (ListeDeMot[i] == ""))
                {
                    continue;
                }
                else
                {
                    if (dictionnaire.ContainsKey(ListeDeMot[i]))
                    {
                        dictionnaire[ListeDeMot[i]] += OccurenceMot[i];
                    }
                    else
                    {
                        dictionnaire[ListeDeMot[i]] = OccurenceMot[i];
                    }
                    
                }

            }
            return dictionnaire;
        }


        public void GenererNuageDeMots(Dictionary<string, int> mots, int largeur, int hauteur, string cheminFichier)
        {
            // Créer une nouvelle image
            using Bitmap image = new Bitmap(largeur, hauteur);
            using Graphics graphics = Graphics.FromImage(image);

            // Fond blanc
            graphics.Clear(Color.White);

            // Déterminer les tailles de police basées sur les occurrences
            int tailleMin = 10;
            int tailleMax = 50;
            if (mots.Count == 0) return;
            int occurenceMax = mots.Values.Max();
            int occurenceMin = mots.Values.Min();
            if (occurenceMax < 2) occurenceMax = 2;

            // Mapper chaque mot à une taille de police
            var motsAvecTaille = mots.Select(mot => new
            {
                Mot = mot.Key,
                Taille = Map(mot.Value, occurenceMin, occurenceMax, tailleMin, tailleMax)
            }).OrderByDescending(m => m.Taille).ToList();

            // Placer les mots aléatoirement sans chevauchement
            Random random = new Random();
            List<Rectangle> positions = new List<Rectangle>();

            foreach (var mot in motsAvecTaille)
            {
                Font font = new Font("Arial", mot.Taille);
                SizeF tailleTexte = graphics.MeasureString(mot.Mot, font);

                Rectangle emplacement;
                bool emplacementTrouve;

                do
                {
                    // Position aléatoire
                    int x = random.Next(0, largeur - (int)tailleTexte.Width);
                    int y = random.Next(0, hauteur - (int)tailleTexte.Height);

                    emplacement = new Rectangle(x, y, (int)tailleTexte.Width, (int)tailleTexte.Height);

                    // Vérifier s'il y a chevauchement
                    emplacementTrouve = !positions.Any(pos => pos.IntersectsWith(emplacement));
                }
                while (!emplacementTrouve);

                // Ajouter le mot sur l'image
                graphics.DrawString(mot.Mot, font, Brushes.Black, emplacement.Location);
                positions.Add(emplacement);
            }

            // Sauvegarder l'image
            image.Save(cheminFichier);
        }

        static int Map(int valeur, int minOrigine, int maxOrigine, int minCible, int maxCible)
        {
            return minCible + (valeur - minOrigine) * (maxCible - minCible) / (maxOrigine - minOrigine);
        }
    }
}

