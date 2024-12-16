using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dico
{
    class Dictionnaire
    {
        public List<string> contenu;

        /// <summary>
        /// Lit le contenu d'un fichier dictionnaire et le divise en une liste de mots.
        /// </summary>
        /// <param name="chemin">Le chemin du fichier dictionnaire.</param>
        /// <returns>Une liste de mots contenus dans le fichier dictionnaire.</returns>
        public List<string> LireDictionnaire(string chemin)
        {
            var contenu = File.ReadAllText(chemin);
            return contenu.Split(new[] { ' ', '\n', '\r' }).ToList();
        }

        /// <summary>
        /// Trie une liste de mots en utilisant l'algorithme de tri fusion.
        /// </summary>
        /// <param name="dictionnaire">La liste de mots à trier.</param>
        /// <returns>Une liste de mots triés.</returns>
        public List<string> TriDictionnairefusion(List<string> dictionnaire)
        {
            if (dictionnaire.Count <= 1)
                return dictionnaire;

            List<string> gauche = new List<string>();
            List<string> droite = new List<string>();
            int milieu = dictionnaire.Count / 2;

            for (int i = 0; i < milieu; i++)
            {
                gauche.Add(dictionnaire[i]);
            }
            for (int i = milieu; i < dictionnaire.Count; i++)
            {
                droite.Add(dictionnaire[i]);
            }

            gauche = TriDictionnairefusion(gauche);
            droite = TriDictionnairefusion(droite);

            return Fusionner(gauche, droite);
        }

        /// <summary>
        /// Fusionne deux listes de mots triées en une seule liste triée.
        /// </summary>
        /// <param name="gauche">La première liste de mots triée.</param>
        /// <param name="droite">La deuxième liste de mots triée.</param>
        /// <returns>Une liste fusionnée et triée de mots.</returns>
        public List<string> Fusionner(List<string> gauche, List<string> droite)
        {
            List<string> resultat = new List<string>();

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

        /// <summary>
        /// Vérifie si un mot est présent dans le dictionnaire.
        /// </summary>
        /// <param name="mot">Le mot à rechercher.</param>
        /// <returns>True si le mot est présent, sinon False.</returns>
        public bool estpresentdansdico(string mot)
        {
            return RechercheDichotomique(contenu, mot);
        }

        /// <summary>
        /// Effectue une recherche dichotomique pour trouver un mot dans une liste triée.
        /// </summary>
        /// <param name="liste">La liste triée de mots.</param>
        /// <param name="mot">Le mot à rechercher.</param>
        /// <returns>True si le mot est trouvé, sinon False.</returns>
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
