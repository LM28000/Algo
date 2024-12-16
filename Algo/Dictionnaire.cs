using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dico
{
    class Dictionnaire
    {
        public List<string> contenu;

        // Lit le contenu d'un fichier et le divise en une liste de mots
        public List<string> LireDictionnaire(string chemin)
        {
            var contenu = File.ReadAllText(chemin);

            return contenu.Split(new[] { ' ', '\n', '\r' })
                          .ToList();
        }

        // Trie une liste de mots en utilisant l'algorithme de tri fusion
        public List<string> TriDictionnairefusion(List<string> dictionnaire)
        {
            if (dictionnaire.Count <= 1)
                return dictionnaire;

            List<string> gauche = new List<string>();
            List<string> droite = new List<string>();
            int milieu = dictionnaire.Count / 2;

            // Divise la liste en deux sous-listes
            for (int i = 0; i < milieu; i++)
            {
                gauche.Add(dictionnaire[i]);
            }
            for (int i = milieu; i < dictionnaire.Count; i++)
            {
                droite.Add(dictionnaire[i]);
            }

            // Trie récursivement les sous-listes
            gauche = TriDictionnairefusion(gauche);
            droite = TriDictionnairefusion(droite);

            // Fusionne les sous-listes triées
            return Fusionner(gauche, droite);
        }

        // Fusionne deux listes triées en une seule liste triée
        public List<string> Fusionner(List<string> gauche, List<string> droite)
        {
            List<string> resultat = new List<string>();

            // Fusionne les listes jusqu'à ce que l'une d'elles soit vide
            while (gauche.Count > 0 || droite.Count > 0)
            {
                if (gauche.Count > 0 && droite.Count > 0)
                {
                    if (gauche[0].CompareTo(droite[0]) < 0)
                    {
                        resultat.Add(gauche[0]);
                        gauche.Remove(gauche[0]);
                    }
                    else
                    {
                        resultat.Add(droite[0]);
                        droite.Remove(droite[0]);
                    }
                }
                else if (gauche.Count > 0)
                {
                    resultat.Add(gauche[0]);
                    gauche.Remove(gauche[0]);
                }
                else if (droite.Count > 0)
                {
                    resultat.Add(droite[0]);
                    droite.Remove(droite[0]);
                }
            }

            return resultat;
        }

        // Vérifie si un mot est présent dans le dictionnaire
        public bool estpresentdansdico(string mot)
        {
            return RechercheDichotomique(contenu, mot);
        }

        // Effectue une recherche dichotomique pour trouver un mot dans une liste triée
        public bool RechercheDichotomique(List<string> liste, string mot)
        {
            int gauche = 0;
            int droite = liste.Count - 1;

            while (gauche <= droite)
            {
                int milieu = (gauche + droite) / 2;
                int comparaison = string.Compare(liste[milieu], mot, StringComparison.OrdinalIgnoreCase);

                if (comparaison == 0)
                {
                    return true;
                }
                else if (comparaison < 0)
                {
                    gauche = milieu + 1;
                }
                else
                {
                    droite = milieu - 1;
                }
            }

            return false;
        }
    }
}
