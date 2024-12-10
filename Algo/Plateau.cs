using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algo
{
    // Classe représentant une lettre avec ses caractéristiques
    internal class Lettre
    {
        public char Caractere { get; set; }
        public int Frequence { get; set; }
        public int Points { get; set; }
    }

    // Classe représentant le plateau de Boggle
    internal class Plateau
    {
        private char[,] grille;
        private static readonly Random random = new Random();
        private List<Lettre> lettres;
        private int taille_grille;
        private int taille_points;
        private List<string> dictionnaire;

        // Constructeur de la classe Plateau
        public Plateau(int taille)
        {
            grille = new char[taille, taille];
            taille_grille = taille;
            // Lecture des fichiers de lettres et de dictionnaire
            lettres = LireFichierLettres("Lettres.txt");
            dictionnaire = LireDictionnaire("MotsPossiblesFR.txt");
            // Initialisation de la grille avec des lettres aléatoires
            InitialiserGrille();
        }

        // Méthode pour lire le dictionnaire à partir d'un fichier
        private List<string> LireDictionnaire(string chemin)
        {
            // Lecture des lignes, puis séparation des mots par espace
            var contenu = File.ReadAllText(chemin);
            return contenu.Split(new[] { ' ', '\n', '\r' })
                          .ToList();
        }


        // Méthode pour lire les lettres à partir d'un fichier
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


        // Méthode pour initialiser la grille avec des lettres aléatoires
        private void InitialiserGrille()
        {
            var poolLettres = new List<char>();

            // Ajout des lettres dans une liste en fonction de leur fréquence
            foreach (var lettre in lettres)
            {
                for (int i = 0; i < lettre.Frequence; i++)
                {
                    poolLettres.Add(lettre.Caractere);
                }
            }

            // Remplissage de la grille avec des lettres aléatoires
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

        // Méthode pour afficher la grille
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

        // Méthode pour tester si un mot est éligible sur le plateau
        public bool Test_Plateau(string mot)
        {
            Console.WriteLine($"Test du mot: {mot}");

            if (!dictionnaire.Contains(mot))
            {
                Console.WriteLine($"Le mot {mot} n'est pas dans le dictionnaire.");
                return false;
            }
            else
            {
                Console.WriteLine($"Le mot {mot} est dans le dictionnaire.");
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


        // Méthode récursive pour vérifier les contraintes d'adjacence
        private bool Test_PlateauRec(string mot, int index, int x, int y, bool[,] visite)
        {
            // Si toutes les lettres ont été trouvées
            if (index == mot.Length)
            {
                return true;
            }

            // Marquer la case courante comme visitée
            visite[x, y] = true;

            // Déplacements possibles (8 directions)
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];

                // Vérifie si la position est valide et si la lettre correspond
                if (nx >= 0 && ny >= 0 && nx < taille_grille && ny < taille_grille &&
                    !visite[nx, ny] && grille[nx, ny] == mot[index])
                {
                    Console.WriteLine($"Trouvé {mot[index]} à ({nx}, {ny})");

                    // Appel récursif pour la prochaine lettre
                    if (Test_PlateauRec(mot, index + 1, nx, ny, visite))
                    {
                        return true;
                    }
                }
            }

            // Réinitialise la case courante avant de revenir
            visite[x, y] = false;
            return false;
        }

    }
}