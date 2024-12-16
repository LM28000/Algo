using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dico;

namespace Algo
{
    /// <summary>
    /// Représente une lettre avec son caractère, sa fréquence et ses points.
    /// </summary>
    internal class Lettre
    {
        /// <summary>
        /// Obtient ou définit le caractère de la lettre.
        /// </summary>
        public char Caractere { get; set; }

        /// <summary>
        /// Obtient ou définit la fréquence de la lettre.
        /// </summary>
        public int Frequence { get; set; }

        /// <summary>
        /// Obtient ou définit les points de la lettre.
        /// </summary>
        public int Points { get; set; }
    }

    /// <summary>
    /// Représente le plateau de jeu.
    /// </summary>
    internal class Plateau
    {
        private char[,] grille;
        private static readonly Random random = new Random();
        public List<Lettre> lettres;
        private int taille_grille;
        public Dictionnaire dictionnaire = new Dictionnaire();
        private string langue;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Plateau"/>.
        /// </summary>
        /// <param name="taille">La taille du plateau.</param>
        /// <param name="langue">La langue utilisée pour le dictionnaire.</param>
        public Plateau(int taille, string langue)
        {
            this.langue = langue;
            grille = new char[taille, taille];
            taille_grille = taille;
            lettres = LireFichierLettres("Lettres.txt");

            if (langue == "Francais")
                dictionnaire.contenu = dictionnaire.LireDictionnaire("MotsPossiblesFR.txt");
            else
                dictionnaire.contenu = dictionnaire.LireDictionnaire("MotsPossiblesEN.txt");

            dictionnaire.contenu = dictionnaire.TriDictionnairefusion(dictionnaire.contenu);
            InitialiserGrille();
        }

        /// <summary>
        /// Lit le fichier des lettres et retourne une liste de <see cref="Lettre"/>.
        /// </summary>
        /// <param name="chemin">Le chemin du fichier des lettres.</param>
        /// <returns>Une liste de <see cref="Lettre"/>.</returns>
        private List<Lettre> LireFichierLettres(string chemin)
        {
            var lettres = new List<Lettre>();
            var lignes = File.ReadAllLines(chemin);

            foreach (var ligne in lignes)
            {
                var parties = ligne.Split(';');
                lettres.Add(new Lettre
                {
                    Caractere = parties[0][0],
                    Points = int.Parse(parties[1]),
                    Frequence = int.Parse(parties[2])
                });
            }

            return lettres;
        }

        /// <summary>
        /// Initialise la grille avec des lettres aléatoires basées sur leur fréquence.
        /// </summary>
        private void InitialiserGrille()
        {
            var poolLettres = new List<char>();

            foreach (var lettre in lettres)
            {
                for (int i = 0; i < lettre.Frequence; i++)
                {
                    poolLettres.Add(lettre.Caractere);
                }
            }

            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    int index = random.Next(poolLettres.Count);
                    grille[i, j] = poolLettres[index];
                    poolLettres.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Affiche la grille sur la console.
        /// </summary>
        public void toString()
        {
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    Console.Write(grille[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Teste si un mot peut être formé sur le plateau.
        /// </summary>
        /// <param name="mot">Le mot à tester.</param>
        /// <returns><c>true</c> si le mot peut être formé; sinon, <c>false</c>.</returns>
        public bool Test_Plateau(string mot)
        {
            Console.WriteLine($"Test du mot: {mot}");

            if (dictionnaire.estpresentdansdico(mot))
            {
                Console.WriteLine($"Le mot {mot} est dans le dictionnaire.");
            }
            else
            {
                Console.WriteLine($"Le mot {mot} n'est pas dans le dictionnaire.");
                return false;
            }

            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] == mot[0])
                    {
                        Console.WriteLine($"Départ possible trouvé pour {mot[0]} à ({i}, {j})");

                        if (Test_PlateauRec(mot, 1, i, j, new bool[taille_grille, taille_grille]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Méthode récursive pour tester si un mot peut être formé sur le plateau.
        /// </summary>
        /// <param name="mot">Le mot à tester.</param>
        /// <param name="index">L'index actuel dans le mot.</param>
        /// <param name="x">La position x actuelle sur la grille.</param>
        /// <param name="y">La position y actuelle sur la grille.</param>
        /// <param name="visite">Tableau des positions déjà visitées.</param>
        /// <returns><c>true</c> si le mot peut être formé; sinon, <c>false</c>.</returns>
        private bool Test_PlateauRec(string mot, int index, int x, int y, bool[,] visite)
        {
            if (index == mot.Length)
            {
                return true;
            }

            visite[x, y] = true;

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];

                if (nx >= 0 && ny >= 0 && nx < taille_grille && ny < taille_grille &&
                    !visite[nx, ny] && grille[nx, ny] == mot[index])
                {
                    Console.WriteLine($"Trouvé {mot[index]} à ({nx}, {ny})");

                    if (Test_PlateauRec(mot, index + 1, nx, ny, visite))
                    {
                        return true;
                    }
                }
            }

            visite[x, y] = false;
            return false;
        }
    }
}
